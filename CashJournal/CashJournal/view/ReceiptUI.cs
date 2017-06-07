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
       
        public ReceiptUI()
        {
            InitializeComponent();
            sums = new Dictionary<string, decimal>();
            viewResult = new DataTable();
            printer = FPrinterEngine.GetInstance();
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
            dataGridViewReceipt.Rows[lastIndex].DefaultCellStyle.BackColor = Color.Aquamarine;
            dataGridViewReceipt.Rows[lastIndex].DefaultCellStyle.ForeColor = Color.Black;
            dataGridViewReceipt.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
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

            if (outputView.Count > 0)
            {
                foreach(ResultView rv in outputView)
                {
                    price += rv.AmountPerUnit;
                    vat += rv.TaxRate;
                    sum += rv.Amount;
                    object[] row = new object[]
                    {
                        position, rv.MaterialName, rv.Unit, rv.Quantity, rv.AmountPerUnit, rv.TaxRate, rv.Amount
                    };
                    tab.Rows.Add(row);
                    position++;
                }
                sums.Add("PRICE", price);
                sums.Add("VAT", vat);
                sums.Add("SUM", sum);
                object[] finalRow = new object[] { "", "", "", "", "", "Итого", sum };
                tab.Rows.Add(finalRow);
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
            printer.PrintReceipt(outputView, receiptAmount);
        }

        private void btnDistribution_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

       
    } // ReceiptUI
}
