using System.Windows.Forms;
using System.Collections.Generic;
using SAP.Middleware.Connector;
using CashJournalModel;
using CashJournalPrinting.view;
using System;

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
        private IDictionary<long, Customer> customers;
        private IList<ReceiptHead> receiptHeaders;
        private IList<ReceiptItem> receiptItems;

        private SAPReader(IDictionary<string, string> connection)
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
            customers = new Dictionary<long, Customer>();
            receiptHeaders = new List<ReceiptHead>();
            receiptItems = new List<ReceiptItem>();
        }
        
        // Get a singleton
        public static SAPReader GetInstance(IDictionary<string, string> connection)
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
        public IDictionary<long, Customer> Customers { get => customers; }
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
                receiptHeaders.Clear();
                receiptItems.Clear();
                customers.Clear();
            }
           
            IRfcTable docs = rfcCashDocs.GetTable("T_CJ_DOCS");

            if (docs != null && docs.RowCount > 0)
            {
                string[] SPACE = new string[1] { " " }; // delimeter 
                // Run through internal table in SAP
                foreach (IRfcStructure row in docs)
                {
                    string posText = row.GetString("POSITION_TEXT");
                    // get only the outgoing deliveries
                    if (!posText.Equals("") && (posText.StartsWith("0") || posText.StartsWith("8"))) 
                    {
                        string[] temp = posText.Split(SPACE, StringSplitOptions.None);
                        CashDoc doc = new CashDoc();
                        long dNumber = Int64.Parse(temp[0]);
                        // All deliveries in our ERP have the numbers between 800000000 and 899999999.
                        if (dNumber >= 800000000L && dNumber <= 899999999L)
                        {
                            doc.CajoNumber = row.GetString("CAJO_NUMBER");
                            doc.CompanyCode = row.GetInt("COMP_CODE");
                            doc.FiscalYear = row.GetInt("FISC_YEAR");
                            doc.PostingNumber = row.GetLong("POSTING_NUMBER");
                            doc.PostingDate = row.GetString("POSTING_DATE");
                            doc.PositionText = posText;
                            doc.DeliveryId = dNumber;
                            doc.Amount = row.GetDecimal("H_NET_AMOUNT");
                            heads.Add(doc);
                        }
                    }
                } // for each
                // Read all customers by deliveries
                IRfcTable likp = rfcCashDocs.GetTable("T_LIKP");
                IRfcTable lips = rfcCashDocs.GetTable("T_LIPS");
                if (likp.RowCount > 0)
                {
                    IRfcFunction bapiCustomer = destination.Repository.CreateFunction("BAPI_CUSTOMER_GETLIST");
                    foreach (IRfcStructure row in likp)
                    {
                        ReceiptHead rh = new ReceiptHead();
                        rh.ReceiptId = row.GetLong("VBELN");
                        rh.DeliveryDate = row.GetString("WADAT");
                        receiptHeaders.Add(rh);
                        Customer customer = new Customer();
                        customer.CustomerId = row.GetString("KUNNR");
                        IRfcTable range = bapiCustomer.GetTable("IDRANGE");
                        range.Append();
                        range.SetValue("SIGN", "I");
                        range.SetValue("OPTION", "EQ");
                        range.SetValue("LOW", customer.CustomerId);
                        range.SetValue("HIGH", customer.CustomerId);
                        bapiCustomer.Invoke(destination);
                        IRfcTable addr = bapiCustomer.GetTable("ADDRESSDATA");
                        if (addr.RowCount > 0)
                        {
                            foreach(IRfcStructure data in addr)
                            {
                                customer.Name = data.GetString("NAME");
                            }
                        }
                        customers.Add(row.GetLong("VBELN"), customer);
                    }

                    // Read all positions from the deliveries
                    if (lips.RowCount > 0)
                    {
                        foreach (IRfcStructure item in lips)
                        {
                            ReceiptItem ri = new ReceiptItem();
                            ri.ReceiptId = lips.GetLong("VBELN");
                            ri.Position = lips.GetLong("POSNR");
                            // Positions with the splitting
                            if (ri.Position >= 900000L && ri.Position <= 999999L)
                            {
                                continue;
                            }
                            ri.Material = lips.GetLong("MATNR");
                            ri.MaterialName = lips.GetString("ARKTX");
                            ri.Unit = lips.GetString("MEINS");
                            ri.Quantity = lips.GetDecimal("LFIMG");
                            IDictionary<long, decimal> pr = FindPricesByDeliveryId(ri.ReceiptId);
                            if (pr != null && pr.ContainsKey(ri.Material))
                            {
                                ri.AmountPerUnit = pr[ri.Material];
                                ri.Amount = ri.AmountPerUnit * ri.Quantity;
                            }
                            receiptItems.Add(ri);
                        }
                    }
                }
            }

        } // Get cash journal documents

        public IList<ResultView> GetOutgoingDelivery(long deliveryNumber, decimal amount)
        {
            if (ReceiptItems.Count > 0)
            {
                receiptItems.Clear();
            }

            IList<ResultView> result = new List<ResultView>();

            foreach (ReceiptHead rh in receiptHeaders)
            {
                if (deliveryNumber == rh.ReceiptId)
                {
                    foreach (ReceiptItem ri in receiptItems)
                    {
                        if (ri.ReceiptId == rh.ReceiptId)
                        {
                            ResultView rv = new ResultView();
                            rv.MaterialName = ri.MaterialName;
                            rv.Quantity = ri.Quantity;
                            rv.AmountPerUnit = ri.AmountPerUnit;
                            rv.Amount = ri.Amount;
                            result.Add(rv);
                        }
                    }
                }
            }

            return result;

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
            rfcSalesOrders.Invoke(destination);
            IRfcTable vbakKeys = rfcSalesOrders.GetTable("T_VBAK_KEYS");
            if (vbakKeys.RowCount > 0)
            {
                foreach (IRfcStructure keys in vbakKeys)
                {
                    IRfcTable sdKeys = rfcSalesInfo.GetTable("T_KEYS");
                    sdKeys.Append();
                    sdKeys.SetValue("VBELN", keys.GetString("VBELN"));
                    rfcSalesInfo.Invoke(destination);
                    IRfcTable vbap = rfcSalesInfo.GetTable("T_VBAP"); 
                    if (vbap.RowCount > 0)
                    {
                        foreach (IRfcStructure row in vbap)
                        {
                            prices.Add(row.GetLong("MATNR"), row.GetDecimal("NETPR"));
                        }
                    }
                }
            }

            return prices;
        }

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