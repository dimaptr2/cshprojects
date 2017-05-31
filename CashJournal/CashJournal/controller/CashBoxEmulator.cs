using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashJournalModel;

namespace CashJournalPrinting.controller
{
    class CashBoxEmulator
    {
        private static CashBoxEmulator instance;
       
        private FPrinterEngine device;

        private CashBoxEmulator() { }

        public static CashBoxEmulator GetInstance()
        {
            if (instance == null)
            {
                instance = new CashBoxEmulator();
            }
            return instance;
        }

        public FPrinterEngine Device { set => device = value; }



    }  // CashBoxEmulator
}
