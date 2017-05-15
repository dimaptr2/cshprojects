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

namespace CashJournal
{
    public partial class StartUI : Form
    {
        private Dictionary<string, string> sapParams;

        public StartUI()
        {
            InitializeComponent();
            sapParams = new Dictionary<string, string>();
        }

        private void StartUI_Load(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            atDate.Text = currentDate.ToString();
            readLogonFile("C:\\Users\\DPETROV\\CJLogon\\logon.txt");
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
                    sapParams[result[0]] = result[1];
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

        }
    }
}
