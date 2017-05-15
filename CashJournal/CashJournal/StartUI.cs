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

namespace CashJournal
{
    public partial class StartUI : Form
    {
        // SAP connection parameters
        private Dictionary<string, string> connection;
        private SAPReader sapReader;

        // Default constructor
        public StartUI()
        {
            InitializeComponent();
            connection = new Dictionary<string, string>();
        }

        // Form initialization
        private void StartUI_Load(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            atDate.Text = currentDate.ToString();
            readLogonFile("C:\\Users\\DPETROV\\CJLogon\\logon.txt");
            sapReader = SAPReader.getInstance(connection);
            sapReader.initSAPDestionation();
        }

        private void readLogonFile(string fileName)
        {
            string line;
            StreamReader file =
                            new StreamReader(fileName);

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

        // Get Z-Report from the gadget
        private void btnZ_Click(object sender, EventArgs e)
        {

        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            MessageBox.Show(convertDateToSAPFormat(atDate.Text));
        }

        // Convert the date to the internal SAP format
        private string convertDateToSAPFormat(string value)
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

    } // end of StartUI class

} // end of namespace Cash Journal
