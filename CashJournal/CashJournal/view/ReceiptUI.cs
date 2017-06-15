using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CashJournalModel;
using SAPEntity;
using CashJournalPrinting;

namespace CashJournal.view
{
    public partial class ReceiptUI : Form
    {

        private long delivery;
        private IList<ResultView> outputView;
        private decimal receiptAmount;
        private IDictionary<string, decimal> sums;
        private DataTable viewResult;
        private FPrinterEngine printer;
        private bool distrComplete;
      
        public ReceiptUI()
        {
            InitializeComponent();
            sums = new Dictionary<string, decimal>();
            viewResult = new DataTable();
            printer = FPrinterEngine.GetInstance();
            distrComplete = false;
        }

        public long Delivery { set => delivery = value; }
        public IList<ResultView> OutputView { set => outputView = value; }
        public decimal ReceiptAmount { set => receiptAmount = value; }


        private void ReceiptUI_Load(object sender, EventArgs e)
        {
            tbxDelivery.Text = delivery.ToString();
            tbxTotalAmount.Text = receiptAmount.ToString();
            dataGridViewReceipt.DataSource = viewResult;
            BuildHeader(ref viewResult);
            BuildBody(ref viewResult);
            viewResult.AcceptChanges();
            int lastIndex = viewResult.Rows.Count - 1;
            AddColors(lastIndex);
        }

        // Create a header of table
        private void BuildHeader(ref DataTable tab)
        {
            string[] header = CreateColumns();
            int index = 0;
            foreach (string name in header)
            {
                tab.Columns.Add(name);
                DataColumn col = tab.Columns[index];
                index++;
            }
        }

        // Create a body of table
        private void BuildBody(ref DataTable tab)
        {
            int position = 1;
            decimal price = 0M;
            decimal vat = 0M;
            decimal sum = 0M;
            string txtUnit = "";
            string txtQuantity = "";

            if (tab.Rows.Count > 0)
            {
                tab.Rows.Clear();
            }

            if (outputView.Count > 0)
            {
               
                foreach (ResultView rv in outputView)
                {
                    price += rv.AmountPerUnit;
                    vat += rv.TaxRate;
                    sum += rv.Amount;
                    txtQuantity = Math.Round(rv.Quantity, 3).ToString();
                    switch (rv.Unit)
                    {
                        case "KG":
                            txtUnit = "КГ";
                            break;
                        case "ST":
                            txtUnit = "ШТ";
                            break;
                    }
                    object[] row = new object[]
                    {
                        position, rv.MaterialName, txtUnit, txtQuantity, rv.AmountPerUnit, rv.TaxRate, rv.Amount
                    };
                    tab.Rows.Add(row);
                    position++;
                }

                if (sums.Count == 0)
                {
                    sums.Add("PRICE", price);
                    sums.Add("VAT", vat);
                    sums.Add("SUM", sum);
                }
              
                object[] finalRow = new object[] { "", "", "", "", "", "Итого", sum };
                tab.Rows.Add(finalRow);
                int lastIndex = position - 1;
                AddColors(lastIndex);
                // Refresh the receipt sum
                //tbxTotalAmount.Text = receiptAmount.ToString();

            }

        }

        // Build an array with column names
        private string[] CreateColumns()
        {
            string[] columns = new string[7];

            columns[0] = "Номер";
            columns[1] = "Наименование артикула";
            columns[2] = "ЕИ";
            columns[3] = "Количество";
            columns[4] = "Цена";
            columns[5] = "НДС";
            columns[6] = "Сумма";

            return columns;

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            decimal total = CalculateSum(ref outputView);
            printer.PrintReceipt(outputView, total, delivery);
        }

        private void btnDistribution_Click(object sender, EventArgs e)
        {
            if (!distrComplete)
            {
                RunDistribution(sums["SUM"]);
                BuildBody(ref viewResult);
            }        
        }

        // Initial quantity
        private decimal SetInitialDistribution()
        {
            decimal unitSum = 0M;

            foreach (ResultView rv in outputView)
            {
                rv.Quantity = 1.000M;
                rv.Amount = Math.Round((rv.Quantity * (rv.AmountPerUnit + rv.TaxRate)), 2);
                unitSum += Math.Round(rv.Amount, 2);
            }

            return unitSum;
        }

        // Run distribution
        private void RunDistribution(decimal currentAmount)
        {

            IDictionary<int, decimal> mainPosition = new Dictionary<int, decimal>();
            decimal sum = 0M;
            //decimal difference = 0M;
            decimal coefficent = Math.Round((receiptAmount / currentAmount), 4);
            
            for (int j = 0; j < outputView.Count; j++)
            {
                outputView[j].Quantity = outputView[j].Quantity * coefficent;
                outputView[j].Amount =
                    Math.Round((outputView[j].Quantity * (outputView[j].AmountPerUnit + outputView[j].TaxRate)), 2);
                sum += Math.Round(outputView[j].Amount, 2);
            }

            distrComplete = true;
        }

        // Choose a maximum
        private decimal ChooseMaximum(decimal v1, decimal v2)
        {
            if (v1 < v2)
            {
                return v2;
            } else
            {
                return v1;
            }
        }

        private decimal CalculateSum(ref IList<ResultView> items)
        {
            decimal sum = 0M;
        
            foreach (ResultView rv in items)
            {
                sum += rv.Amount;
            }

            sum = Math.Round(sum, 2);

            return sum;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddColors(int index)
        {
            dataGridViewReceipt.Rows[index].DefaultCellStyle.BackColor = Color.Aquamarine;
            dataGridViewReceipt.Rows[index].DefaultCellStyle.ForeColor = Color.Black;
            dataGridViewReceipt.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

     
    } // ReceiptUI
}
