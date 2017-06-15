using DrvFRLib;
using System;
using System.Collections.Generic;
using CashJournalModel;

namespace CashJournalPrinting
{
   
    class FPrinterEngine
    {
        private static FPrinterEngine instance;
        // Driver of the fiscal device
        private DrvFR driver;
        private PrintForm printForm;
        private const string DASH = "------------------------------";

        private FPrinterEngine()
        {
            driver = new DrvFR();
            printForm = new PrintForm();
        }

        // Create the instance as a singleton
        public static FPrinterEngine GetInstance()
        {
            if (instance == null)
            {
                instance = new FPrinterEngine();
            }
            return instance;
        }

        public void InitFiscalDevice()
        {
            driver.ConnectionTimeout = 2000;
            driver.WaitConnection();
        }

        // Open session
        public void OpenFSession()
        {
            if (GetDeviceStatus() == 4)
            {
                driver.FNOpenSession();
            }
        }

        // Close a session
        public void CloseFSession()
        {
            if (GetDeviceStatus() != 4)
            {
                driver.FNCloseSession();
            }
        }

        public int GetDeviceStatus()
        {
            driver.GetECRStatus();
            return driver.ECRMode;
        }

        // X-Report
        public void PrintXReport()
        {
            driver.PrintReportWithoutCleaning();
        }

        public void TellMeAbout()
        {
            driver.ShowProperties();
        }

        // Z-Report
        public void PrintZReport()
        {
            driver.PrintReportWithCleaning();
        }

        public void ClearPrinterResult()
        {
            driver.ClearResult();
        }

        public void ResetDevice()
        {
            driver.ResetECR();
        }

        // Build a document for printing
        public void ExecuteSale(ref PrintForm pf)
        {
            int deviceMode = GetDeviceStatus();
            if (deviceMode != 4)
            {
                try
                {
                    driver.OpenCheck();
                    for (int k = 0; k < pf.Positions.Count; k++)
                    {
                        driver.Password = 30;
                        driver.Price = pf.Positions[k].AmountPerUnit;
                        driver.Quantity = Convert.ToDouble(Math.Round(pf.Positions[k].Quantity, 3));
                        driver.Department = 1;
                        driver.Tax1 = 1;
                        driver.Tax2 = 0;
                        driver.Tax3 = 0;
                        driver.Tax4 = 0;
                        driver.StringForPrinting = pf.Positions[k].MaterialName;
                        driver.Sale();
                    }
                } finally
                {
                    driver.CloseCheck();
                    driver.OutputReceipt();
                }
            }
        }


        // Documents for printing creation
        public void PrintReceipt(IList<ResultView> outList, decimal actualAmount, long delivery)
        {
            int status = driver.GetECRStatus();
            printForm.InitPrintForm();
            printForm.Positions = outList;
            printForm.CalculateAmounts();
            ExecuteSale(ref printForm);
            //CreateReceipt(actualAmount, delivery);
        }

      
        // PRIVATE SECTION

     
        // Main method for the printing of receipts
        private void CreateReceipt(decimal sum1, long delivery)
        {
            // Print a receipt
            CreateHeader();
            BuildItemsDocument(ref printForm, sum1, delivery);

        }

        private void CreateHeader()
        {
            // Print a receipt
            driver.StringForPrinting = "ООО МК Павловская Слобода";
            driver.PrintString();
            driver.StringForPrinting = "Номер ФР " + driver.GetCashReg();
            driver.PrintString();
            driver.StringForPrinting = "ИНН 5017041244";
            driver.PrintString();
            driver.StringForPrinting = "Код ОКПО 53942794";
            driver.PrintString();
            driver.StringForPrinting = "Дата: " + driver.Date + " " + driver.Time;
            driver.PrintString();
            driver.StringForPrinting = DASH;
            driver.PrintString();
        }

        private void BuildItemsDocument(ref PrintForm pf, decimal sum1, long delivery)
        {
            IList<ResultView> it = pf.Positions;

            driver.OpenCheck();
            int counter = 0;
            for (int i = 0; i < it.Count; i++)
            {
                driver.StringForPrinting = DASH;
                driver.PrintString();
                counter = i + 1;
                driver.StringForPrinting = "Секция # " + counter;
                driver.PrintString();
                driver.StringForPrinting = "Арт: " + it[i].MaterialName;
                driver.PrintString();
                driver.StringForPrinting = "Кол-во | Цена | НДС | Сумма";
                driver.PrintString();
                driver.StringForPrinting = "  " + Math.Round(it[i].Quantity, 3).ToString() + "  " + it[i].AmountPerUnit.ToString() +
                    "  " + it[i].TaxRate.ToString() + "  " + it[i].Amount;
                driver.PrintString();
            }
            driver.StringForPrinting = DASH;
            driver.PrintString();
            driver.StringForPrinting = "  ";
            driver.PrintString();
            decimal totalVat = pf.Vat10 + pf.Vat18;
            driver.StringForPrinting = "Итого: " + sum1.ToString() + " НДС: " + totalVat.ToString(); ;
            driver.PrintString();
            driver.StringForPrinting = DASH;
            driver.PrintString();
            driver.StringForPrinting = "Спасибо!";
            driver.PrintWideString();
            driver.CloseCheck();
            driver.FeedDocument();
            driver.FinishDocument();

        }

        private void CreateFooter()
        {

        }

    } // FPrinterEngine

   


} // CashJournal.controller
