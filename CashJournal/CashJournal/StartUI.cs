using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SAPEntity;
using CashJournalPrinting.view;
using CashJournalPrinting.controller;
using CashJournalModel;

namespace CashJournalPrinting
{
    public partial class StartUI : Form
    {
        // SAP connection parameters
        private Dictionary<string, string> connection;
        private SAPReader sapReader;
        private FPrinterEngine printEngine;
        private CashBoxEmulator cashBox;
        private DataTable dataTable;
        private string[] columnsCollection;
        private ReceiptHead header;
        private IList<ResultView> items;

        // Default constructor
        public StartUI()
        {
            InitializeComponent();
            connection = new Dictionary<string, string>();
            header = new ReceiptHead();
            items = new List<ResultView>();
        }

        // Form initialization
        private void StartUI_Load(object sender, EventArgs e)
        {
            
            ConnectProgress progressBar = new ConnectProgress();
            progressBar.Show(this);
            DateTime currentDate = DateTime.Now;
            progressBar.GetProgressBar().Value = 10;
            progressBar.GetProgressBar().Update();
            atDate.Text = currentDate.ToString();
            ReadLogonFile("logon.txt");
            progressBar.GetProgressBar().Value = 70;
            progressBar.GetProgressBar().Update();
            sapReader = SAPReader.GetInstance(connection);
            sapReader.InitSAPDestionation();
            progressBar.GetProgressBar().Value = 100;
            progressBar.GetProgressBar().Update();
            progressBar.Close();
            if (!sapReader.ConnectionSuccess)
            {
                Close();
            } else
            {
                tbxCompany.Text = "1000";
                tbxCashBox.Text = "1000";
            }
            printEngine = FPrinterEngine.GetInstance();
            printEngine.InitFiscalDevice();
            cashBox = CashBoxEmulator.GetInstance();
            cashBox.Device = printEngine;
            dataTable = new DataTable();
            columnsCollection = CreateTableHeader();
            BuildTableHead(ref dataTable);
            dataGridViewOutput.DataSource = dataTable;
            // Add the double clicking event for the grid
            dataGridViewOutput.DoubleClick += new EventHandler(tableRow_DoubleClick);
            
        }

        // Read the special file with SAP parameters for the connection
        private void ReadLogonFile(string fileName)
        {
            string line;
            StreamReader file = new StreamReader(fileName);

            while ((line = file.ReadLine()) != null)
            {
                string[] separator = new string[1] { "=" };
                string[] result = line.Split(separator, StringSplitOptions.None);
                if (result.Length == 2)
                {
                    connection[result[0]] = result[1];
                }
            }

            file.Close();
        }

        // Build the header of the table grid.
        private string[] CreateTableHeader()
        {
            string[] dataColumns = new string[7];
            dataColumns[0] = "№ п/п";
            dataColumns[1] = "Клиент";
            dataColumns[2] = "Приходный ордер";
            dataColumns[3] = "Дата проводки";
            dataColumns[4] = "Пояснительный текст";
            dataColumns[5] = "Исходящая поставка";
            dataColumns[6] = "Сумма";

            return dataColumns;
        }

        // Draw a head of table
        private void BuildTableHead(ref DataTable tab)
        {
            int index = 0;
            foreach (string name in columnsCollection)
            {
                tab.Columns.Add(name);
                DataColumn col = tab.Columns[index];
                index++;
            }

        }
    

        // X-Report from the gadget
        private void btnX_Click(object sender, EventArgs e)
        {
            if (printEngine.IsReadyForPrinting())
            {
                printEngine.PrintXReport();
            } else
            {
                MessageBox.Show("ФМ не открыт");
            }   
        }

        // Get Z-Report from the gadget
        private void btnZ_Click(object sender, EventArgs e)
        {
            if (printEngine.IsReadyForPrinting())
            {
                printEngine.PrintZReport();
            } 
        }

        // Read the receipts
        private void btnRead_Click(object sender, EventArgs e)
        {
            sapReader.CompanyCode = tbxCompany.Text;
            sapReader.CajoNumber = tbxCashBox.Text;
            sapReader.AtDate = ConvertDateToSAPFormat(atDate.Text);
            sapReader.UploadData();
            // Build the grid and show it
            BuildMainGrid();
        }

        // Convert the date to the internal SAP format
        private string ConvertDateToSAPFormat(string value)
        {
            string result = "";
            string[] separator = new string[1] { " " };
            string[] temp  = value.Split(separator, StringSplitOptions.None);
            // Get a year
            result = temp[2];
            // Get a month
            switch (temp[1])
            {
                case "января":
                    result += "01";
                    break;
                case "февраля":
                    result += "02";
                    break;
                case "марта":
                    result += "03";
                    break;
                case "апреля":
                    result += "04";
                    break;
                case "мая":
                    result += "05";
                    break;
                case "июня":
                    result += "06";
                    break;
                case "июля":
                    result += "07";
                    break;
                case "августа":
                    result += "08";
                    break;
                case "сентября":
                    result += "09";
                    break;
                case "октября":
                    result += "10";
                    break;
                case "ноября":
                    result += "11";
                    break;
                case "декабря":
                    result += "12";
                    break;
            }
            // Get a day
            int day = Int32.Parse(temp[0]);
            if (day >= 1 && day <= 9)
            {
                result = result + "0" + day;
            } else
            {
                result = result + day;
            }

            return result;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }


        // Build a grid
        private void BuildMainGrid()
        {
            dataTable.Clear();
            if (sapReader.Heads.Count > 0)
            {
                decimal totalAmount = 0M;
                int counter = 0;

                foreach (CashDoc doc in sapReader.Heads)
                {
                    counter++;
                    totalAmount += doc.Amount;
                    object[] sapRow = new object[] {
                        counter, sapReader.Customers[doc.DeliveryId].Name,
                        doc.PostingNumber, doc.PostingDate,
                        doc.PositionText, doc.DeliveryId, doc.Amount
                    };
                    dataTable.Rows.Add(sapRow); 
                }
                // add a final sum
                counter++;
                object[] finalRow = new object[] {
                    counter, "", 0, "", "Итого: ", 0, totalAmount
                };
                
                dataTable.Rows.Add(finalRow);
                dataTable.AcceptChanges();
                int lastIndex = counter - 1;
                dataGridViewOutput.Columns["Сумма"].DefaultCellStyle.Format = "c";
                dataGridViewOutput.Rows[lastIndex].DefaultCellStyle.BackColor = Color.Aquamarine;
                dataGridViewOutput.Rows[lastIndex].DefaultCellStyle.ForeColor = Color.Black;
                dataGridViewOutput.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }

        } // build the main grid view

        // Double on the table row
        private void tableRow_DoubleClick(object sender, EventArgs e)
        {
            DataGridViewRow currentRow = dataGridViewOutput.CurrentRow;
            DataRow row = ((DataRowView)currentRow.DataBoundItem).Row;
            long valDelivery = Int64.Parse(row.ItemArray[5].ToString());
            decimal amount = decimal.Parse(row.ItemArray[6].ToString());
            IList<ResultView> output = sapReader.GetOutgoingDelivery(valDelivery, amount);
        }

      
    } // end of StartUI class

} // end of namespace Cash Journal
