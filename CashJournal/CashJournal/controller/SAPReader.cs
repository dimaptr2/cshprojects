using System.Windows.Forms;
using System.Collections.Generic;
using SAP.Middleware.Connector;
using CashJournalModel;
using CashJournal.view;
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
        private IDictionary<long, Material> materials;
        private IList<Receipt> receipts;

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
            materials = new Dictionary<long, Material>();
            receipts = new List<Receipt>();
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
        public IDictionary<long, Material> Materials { get => materials; }
     
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

        // Get all cash journal documents at date
        public void GetCashDocuments()
        {
            // If we have old values in the collection, we should to clear it.
            heads.Clear();
            ProgressTemplate progress = new ProgressTemplate();
            progress.Text = "Выборка данных SAP";
            progress.Show();
            progress.OutputProgress.Value = 1;
            progress.OutputProgress.Update();
           IRfcFunction rfcCashDocs = destination.Repository.CreateFunction("Z_RFC_GET_CASHDOC");
            rfcCashDocs.SetValue("I_COMP_CODE", companyCode);
            rfcCashDocs.SetValue("I_CAJO_NUMBER", cajoNumber);
            rfcCashDocs.SetValue("FROM_DATE", atDate);
            rfcCashDocs.SetValue("TO_DATE", atDate);
            // Execute the remote function on server
            rfcCashDocs.Invoke(destination);
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

            // deliveries headers
            IRfcTable likp = rfcCashDocs.GetTable("T_LIKP");
            // deliveries items
            IRfcTable lips = rfcCashDocs.GetTable("T_LIPS");

            GetOutgoingDeliveries(likp, lips);

        } // Get cash journal documents

        private void GetOutgoingDeliveries(IRfcTable hd, IRfcTable it)
        {
            receipts.Clear();

            if ( hd != null && it != null && hd.RowCount > 0 && it.RowCount > 0)
            {
                for (int i = 0; i < hd.RowCount; i++)
                {
                    hd.CurrentIndex = i;
                    Receipt receipt = new Receipt();
                    long deliveryId = hd.GetLong("VBELN");
                }
            }
        }

    } // end of class

  

}  // end of namespace