using System.Windows.Forms;
using System.Collections.Generic;
using SAP.Middleware.Connector;
using CashJournalModel;
using CashJournalPrinting.view;
using System;
using System.Threading;

namespace SAPEntity {

    public class SAPReader
    {
        private static SAPReader instance;
        private bool connectionSuccess;
        private string companyCode;
        private string cajoNumber;
        private string atDate;
        private IDictionary<string, string> connection;
        private RfcConfigParameters parameters;
        private RfcDestination destination;
        
        // Data containers
        private IList<CashDoc> heads;
        private IRfcFunction rfcCashDocs;
        private ReceiptHead receiptHeader;
        private IList<ReceiptItem> receiptItems;

        // Asynchronous methods


        private SAPReader(Dictionary<string, string> connection)
        {
            this.connection = connection;
            parameters = new RfcConfigParameters();
            parameters[RfcConfigParameters.Name] = connection["name"];
            parameters[RfcConfigParameters.AppServerHost] = connection["ashost"];
            parameters[RfcConfigParameters.SystemNumber] = connection["sysnr"];
            parameters[RfcConfigParameters.SystemID] = connection["r3name"];
            parameters[RfcConfigParameters.Client] = connection["client"];
            parameters[RfcConfigParameters.User] = connection["user"];
            parameters[RfcConfigParameters.Password] = connection["password"];
            parameters[RfcConfigParameters.Language] = connection["lang"];
            heads = new List<CashDoc>();
            receiptHeader = new ReceiptHead();
            receiptItems = new List<ReceiptItem>();
        }
        
        // Get a singleton
        public static SAPReader GetInstance(Dictionary<string, string> connection)
        {
            if (instance == null)
            {
                instance = new SAPReader(connection);
            }
            return instance;
        }

        // Properties (setters and getters in the C# style)
        public bool ConnectionSuccess { get => connectionSuccess; }
        public string CompanyCode { set => companyCode = value; }
        public string CajoNumber { set => cajoNumber = value; }
        public string AtDate { set => atDate = value; } 
        public IList<CashDoc> Heads { get => heads; }
        public ReceiptHead ReceiptHeader { get => receiptHeader; }
        public IList<ReceiptItem> ReceiptItems { get => receiptItems; }
       
        // Main methods of this class
        public void InitSAPDestionation()
        {
            const string MSG = "Нет связи с сервером SAP";
            bool flag = false;
           
            connection.Clear();

            if (parameters != null)
            {
                try
                {
                    destination = RfcDestinationManager.GetDestination(parameters);
                    destination.Ping();
                    connectionSuccess = true;
                } catch (RfcInvalidParameterException ex)
                {
                    connectionSuccess = false;
                    if (ex.Message.Equals(""))
                    {
                        flag = true;
                    } else
                    {
                        MessageBox.Show(ex.Message);
                    }
                } catch (RfcBaseException ex)
                {
                    connectionSuccess = false;
                    if (ex.Message.Equals(""))
                    {
                        flag = true;
                    } else
                    {
                        MessageBox.Show(ex.Message);
                    }
                    
                }

                if (flag && !connectionSuccess)
                {
                    MessageBox.Show(MSG);
                }  
                
            } // external if

        }

        // Upload all data
        public void UploadData()
        {
          
            try
            {
                ProgressTemplate progress = new ProgressTemplate();
                progress.Text = "Выборка данных SAP";
                progress.Show();
                progress.OutputProgress.Value = 30;
                progress.OutputProgress.Update();
                rfcCashDocs = destination.Repository.CreateFunction("Z_RFC_GET_CASHDOC");
                rfcCashDocs.SetValue("I_COMP_CODE", companyCode);
                rfcCashDocs.SetValue("I_CAJO_NUMBER", cajoNumber);
                rfcCashDocs.SetValue("FROM_DATE", atDate);
                rfcCashDocs.SetValue("TO_DATE", atDate);
                // Execute the remote function on server
                rfcCashDocs.Invoke(destination);
                progress.OutputProgress.Value = 50;
                progress.OutputProgress.Update();
                GetCashDocuments();
                //Thread tid1 = new Thread(new ThreadStart(GetCashDocuments));
               // Thread tid2 = new Thread(new ThreadStart(GetOutgoingDeliveries));
               // tid1.Start();
              //  tid2.Start();
                progress.OutputProgress.Value = 70;
                progress.OutputProgress.Update();
                progress.OutputProgress.Value = 100;
                progress.OutputProgress.Update();
                progress.Close();
            } catch (RfcAbapException e)
            {
                MessageBox.Show(e.Message);
            }

        }

        // Get all cash journal documents at date
        private void GetCashDocuments()
        {
            // If we have old values in the collection, we should to clear it.
            if (heads.Count > 0)
            {
                heads.Clear();
            }
           
            IRfcTable docs = rfcCashDocs.GetTable("T_CJ_DOCS");

            if (docs != null && docs.RowCount > 0)
            {
                string[] SPACE = new string[1] { " " }; // delimeter 
                // Run through internal table in SAP
                foreach (var row in docs)
                {
                    string posText = row.GetString("POSITION_TEXT");
                    // get only the outgoing deliveries
                    if (!posText.Equals("") && posText.StartsWith("8"))
                    {
                        CashDoc doc = new CashDoc();
                        doc.CajoNumber = row.GetString("CAJO_NUMBER");
                        doc.CompanyCode = row.GetInt("COMP_CODE");
                        doc.FiscalYear = row.GetInt("FISC_YEAR");
                        doc.PostingNumber = row.GetLong("POSTING_NUMBER");
                        doc.PostingDate = row.GetString("POSTING_DATE");
                        doc.PositionText = posText;
                        string[] temp = posText.Split(SPACE, StringSplitOptions.None);
                        doc.DeliveryId = Int64.Parse(temp[0]);
                        doc.Amount = row.GetDecimal("H_NET_AMOUNT");
                        heads.Add(doc);
                    }
                } // for each
            }

        } // Get cash journal documents

        public void GetOutgoingDeliveries(long deliveryNumber, decimal amount)
        {
            if (ReceiptItems.Count > 0)
            {
                receiptItems.Clear();
            }

            IRfcTable deliveryHeads = rfcCashDocs.GetTable("T_LIKP");
            IRfcTable deliveryItems = rfcCashDocs.GetTable("T_LIPS");
            
            if ( deliveryHeads != null && deliveryItems != null 
                && deliveryHeads.RowCount > 0 && deliveryItems.RowCount > 0)
            {
                for (int i = 0; i < deliveryHeads.RowCount; i++)
                {
                    deliveryHeads.CurrentIndex = i;
                    if (deliveryNumber == deliveryHeads.GetLong("VBELN"))
                    {
                        receiptHeader.ReceiptId = deliveryHeads.GetLong("VBELN");
                        // Get the price list by the delivery number
                        IDictionary<long, decimal> pr = FindPricesByDeliveryId(receiptHeader.ReceiptId);
                        receiptHeader.DeliveryDate = deliveryHeads.GetString("WADAT");
                        for (int j = 0; j < deliveryItems.RowCount; j++)
                        {
                            deliveryItems.CurrentIndex = j;
                            if (ReceiptHeader.ReceiptId == deliveryItems.GetLong("VBELN"))
                            {
                                ReceiptItem ri = new ReceiptItem();
                                ri.ReceiptId = deliveryItems.GetLong("VBELN");
                                ri.Position = deliveryItems.GetLong("POSNR");
                                ri.Material = deliveryItems.GetLong("MATNR");
                                ri.MaterialName = deliveryItems.GetString("ARKTX");
                                ri.Unit = deliveryItems.GetString("MEINS");
                                ri.Quantity = deliveryItems.GetDecimal("LFIMG");
                                if (pr.Count > 0)
                                {
                                    ri.AmountPerUnit = pr[ri.Material];
                                    ri.Amount = ri.Quantity * ri.AmountPerUnit;
                                }
                                receiptItems.Add(ri);
                            }
                            
                        }
                    } 
                }
               
            }

        } // end of method GetOutgoingDeliveries

        private IDictionary<long, decimal> FindPricesByDeliveryId(long id)
        {
            IDictionary<long, decimal> prices = new Dictionary<long, decimal>();

            // Get a sales order numbers by delivery Id
            IRfcFunction rfcSalesOrders = destination.Repository.CreateFunction("Z_RFC_GET_SDORD_BY_DELIVERY");
            // Get a sales order master data
            IRfcFunction rfcSalesInfo = destination.Repository.CreateFunction("Z_RFC_GET_SD_ORDERS_BY_KEY");

            IRfcTable keyParams = rfcSalesOrders.GetTable("T_KEYS");
            keyParams.Append();
            keyParams.SetValue("VBELN", convertToAlpha(id));

            return prices;
        }

        // Get sales orders by the delivery Id
        private void GetPriceList(long key)
        {
            // Get a sales order numbers by delivery Id
            IRfcFunction rfcSalesOrders = destination.Repository.CreateFunction("Z_RFC_GET_SDORD_BY_DELIVERY");
            // Get a sales order master data
            IRfcFunction rfcSalesInfo = destination.Repository.CreateFunction("Z_RFC_GET_SD_ORDERS_BY_KEY");

            IRfcTable keyParams = rfcSalesOrders.GetTable("T_KEYS");
            keyParams.Append();
            keyParams.SetValue("VBELN", convertToAlpha(key));
            try
            {
                rfcSalesOrders.Invoke(destination);
                IRfcTable resultSet = rfcSalesOrders.GetTable("T_VBAK_KEYS");
                if (resultSet.RowCount > 0)
                {
                    for (int i = 0; i < resultSet.RowCount; i++)
                    {
                        resultSet.CurrentIndex = i;
                        IRfcTable sdKeys = rfcSalesInfo.GetTable("T_KEYS");
                        sdKeys.Append();
                        sdKeys.SetValue("VBELN", resultSet.GetString("VBELN"));
                        rfcSalesInfo.Invoke(destination);
                        IDictionary<long, decimal> pItems = new Dictionary<long, decimal>();
                        IRfcTable vbap = rfcSalesInfo.GetTable("T_VBAP");
                        for (int k = 0; k < vbap.RowCount; k++)
                        {
                            vbap.CurrentIndex = k;
                           
                        }
                     
                    }
                }
            } catch (RfcAbapException e)
            {
                MessageBox.Show(e.Message);
            }
           
        }  // end of method GetPriceList

        // Add initial zeroes if we need it (delivery Id has a length equals 10 symbols)
        private string convertToAlpha(long val)
        {
            const int LENGTH = 10;
            string strValue = val.ToString();

            if (strValue.Length < LENGTH)
            {
                for (int i = 1; i <= (LENGTH - strValue.Length); i++)
                {
                    strValue = "0" + strValue;
                }
            }

            return strValue;
        }

    } // end of class

  

}  // end of namespace