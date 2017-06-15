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
        private IDictionary<long, SalesDataCache> receipts;

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
            receipts = new SortedDictionary<long, SalesDataCache>();
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
        public IDictionary<long, SalesDataCache> Receipts { get => receipts; }
       
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
                progress.StartPosition = FormStartPosition.CenterScreen;
                progress.AllowTransparency = true;
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
                foreach (KeyValuePair<long, SalesDataCache> pair in receipts)
                {
                    pair.Value.ClearItems();
                }
                receipts.Clear();
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
                        long idValue = row.GetLong("VBELN");
                        SalesDataCache cache = new SalesDataCache();
                        cache.Id = idValue;
                        foreach (IRfcStructure it in lips)
                        {
                            String deliveryId = convertToAlpha(idValue);
                            if (deliveryId.Equals(it.GetString("VBELN")))
                            {
                                ReceiptItem ri = new ReceiptItem();
                                ri.ReceiptId = it.GetLong("VBELN");
                                ri.Position = it.GetLong("POSNR");
                                // Skip the splitted positions
                                if (ri.Position >= 900000L && ri.Position <= 999999L)
                                {
                                    continue;
                                }
                                ri.Material = it.GetLong("MATNR");
                                ri.MaterialName = it.GetString("ARKTX");
                                ri.Unit = it.GetString("MEINS");
                                ri.Quantity = it.GetDecimal("LFIMG");
                                IDictionary<long, decimal[]> pr = FindPricesByDeliveryId(ri.ReceiptId);
                                if (pr != null && pr.ContainsKey(ri.Material))
                                {
                                    decimal[] prices = pr[ri.Material];
                                    ri.AmountPerUnit = prices[0];
                                    ri.TaxRate = prices[1];
                                    ri.Amount = Math.Round((ri.Quantity * (ri.AmountPerUnit + ri.TaxRate)), 2);
                                }
                                cache.AddItem(ri);
                            }
                        }
                        receipts.Add(idValue, cache);
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
                }
            }

        } // Get cash journal documents

        public IList<ResultView> GetOutgoingDelivery(long deliveryNumber, decimal amount)
        {
            IList<ResultView> result = new List<ResultView>();
            const decimal ZERO = 0M;

            if (receipts.Count > 0)
            {
                SalesDataCache data = receipts[deliveryNumber];
                for (int i = 0; i < data.Items.Count; i++)
                {
                    ResultView rv = new ResultView();
                    rv.MaterialName = data.Items[i].MaterialName;
                    rv.Unit = data.Items[i].Unit;
                    rv.Quantity = data.Items[i].Quantity;
                    if (rv.Quantity.Equals(ZERO))
                    {
                        continue;
                    }
                    rv.AmountPerUnit = data.Items[i].AmountPerUnit;
                    rv.TaxRate = data.Items[i].TaxRate;
                    rv.Amount = data.Items[i].Amount;
                    result.Add(rv);
                }
            }
            return result;
        } // end of method GetOutgoingDeliveries

        // Read sales orders and define prices and VAT rates. 
        private IDictionary<long, decimal[]> FindPricesByDeliveryId(long id)
        {
            IDictionary<long, decimal[]> prices = new Dictionary<long, decimal[]>();

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
                        // Get two values in the array. First value is a price, second is a tax rate. 
                        decimal tempValue = 0M; 
                        decimal tempVat = 0M;

                        foreach (IRfcStructure row in vbap)
                        {
                            decimal[] data = new decimal[2];
                            data[0] = row.GetDecimal("NETPR");
                            data[1] = row.GetDecimal("KZWI5");
                            tempValue += data[0];
                            tempVat += data[1];
                            long materialNumber = row.GetLong("MATNR");
                            if (prices.ContainsKey(materialNumber))
                            {
                                prices[materialNumber] = new decimal[] { tempValue, tempVat };
                            } else
                            {
                                prices.Add(materialNumber, data);
                            }           
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