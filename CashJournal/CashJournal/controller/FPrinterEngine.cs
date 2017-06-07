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

        private FPrinterEngine()
        {
            driver = new DrvFR();
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

        public bool IsReadyForPrinting()
        {
            if (driver.GetECRStatus() == 0)
            {
                return true;
            } else
            {
                return false;
            }
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

        public void GetBeep()
        {
            driver.Beep();
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

        // Documents for printing creation
        public void PrintReceipt(IList<ResultView> outList, decimal actualAmount)
        {
            int status = driver.GetECRStatus();
            if (status == 4)
            {
                StartPrintActivity();
            }
            CreateReceipt(outList, actualAmount);
        }

        // PRIVATE SECTION

        private void StartPrintActivity()
        {
            driver.FNBeginOpenSession();
        }

        private void FinishPrintActivity()
        {
            driver.FNCloseSession();
        }

        // Main method for the printing of receipts
        private void CreateReceipt(IList<ResultView> items, decimal sum1)
        {

        }

    } // FPrinterEngine

} // CashJournal.controller
