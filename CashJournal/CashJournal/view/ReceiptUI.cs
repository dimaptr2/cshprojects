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

            if (outputView.Count > 0)
            {
                foreach (ResultView rv in outputView)
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
            IDictionary<int, decimal>  changedPosition = RunDistribution();
        }

        // amount correction
        private void CorrectTheAmount()
        {

            ShowDistribution();
        }

        // Run distribution
        private IDictionary<int, decimal> RunDistribution()
        {

            IDictionary<int, decimal> mainPosition = new Dictionary<int, decimal>();
            decimal sum;
            decimal difference = 0M;
            decimal coefficent = Math.Round((sums["SUM"] / receiptAmount), 6);

            while (true)
            {
                sum = 0M;
                for (int j = 0; j < outputView.Count; j++)
                {
                    coefficent = outputView[j].Amount / receiptAmount;
                    coefficent = Math.Round(coefficent, 3);
                    outputView[j].Quantity = outputView[j].Quantity * (1 - coefficent);
                    outputView[j].Amount =
                        Math.Round((outputView[j].Quantity * (outputView[j].AmountPerUnit + outputView[j].TaxRate)), 2);
                    sum += Math.Round(outputView[j].Amount, 2);
                }
                difference = sum - receiptAmount;
                if (difference > 0M)
                {
                    continue;
                }
                else
                {
                    break;
                }
            }

            // Add difference to the position with the greatest price
            int index = 0;
            decimal price = 0M;

            foreach (ResultView rv in outputView)
            {
                int current = outputView.IndexOf(rv);
                int next = current + 1;
                if (next >= outputView.Count)
                {
                    break;
                }
                decimal maxValue = ChooseMaximum(outputView[current].AmountPerUnit, outputView[next].AmountPerUnit);
                if (maxValue.Equals(outputView[current].AmountPerUnit))
                {
                    index = current;
                    price = outputView[current].AmountPerUnit;
                } else
                {
                    index = next;
                    price = outputView[next].AmountPerUnit;
                }
            }

            mainPosition.Add(index, price);

            return mainPosition;

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

        // quantity distribution
        private void ShowDistribution()
        {
            decimal sum = 0M;
            viewResult.Clear();
            int counter = 1;

            decimal difference = (-1) * (receiptAmount - sums["SUM"]);
            decimal coefficent = Math.Round((difference / receiptAmount), 3);

            for (int j = 0; j < outputView.Count; j++)
            {
                outputView[j].Quantity = Math.Round((coefficent * outputView[j].Quantity), 3);
                outputView[j].Amount =
                    Math.Round((outputView[j].Quantity * (outputView[j].AmountPerUnit + outputView[j].TaxRate)), 2);
                sum += outputView[j].Amount;
                object[] row = new object[]
                {
                        counter, outputView[j].MaterialName,  outputView[j].Unit,  outputView[j].Quantity,
                         outputView[j].AmountPerUnit,  outputView[j].TaxRate, outputView[j].Amount
                };
                viewResult.Rows.Add(row);
                counter++;
            }
            // Add the latest row with the total sum.
            object[] finalRow = new object[] { "", "", "", "", "", "Итого", sum };
            viewResult.Rows.Add(finalRow);
            viewResult.AcceptChanges();
            int lastIndex = viewResult.Rows.Count - 1;
            AddColors(lastIndex);
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
