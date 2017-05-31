using DrvFRLib;

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

    } // FPrinterEngine

} // CashJournal.controller
