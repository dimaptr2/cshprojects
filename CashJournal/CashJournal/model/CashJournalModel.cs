/*
 * Here are classes are representing the model of data 
 */

using System.Collections.Generic;

namespace CashJournalModel
{
    public class CashDoc
    {
        private string cajoNumber;
        private int companyCode;
        private int fiscalYear;
        private long postingNumber;
        private string postingDate;
        private string positionText;
        private long deliveryId;
        private decimal amount;

        // Accessors to the properties
        public string CajoNumber { get => cajoNumber; set => cajoNumber = value; }
        public int CompanyCode { get => companyCode; set => companyCode = value; }
        public int FiscalYear { get => fiscalYear; set => fiscalYear = value; }
        public long PostingNumber { get => postingNumber; set => postingNumber = value; }
        public string PostingDate { get => postingDate; set => postingDate = value; }
        public string PositionText { get => positionText; set => positionText = value; }
        public long DeliveryId { get => deliveryId; set => deliveryId = value; }
        public decimal Amount { get => amount; set => amount = value; }


    } // CashDoc

    public class ResultView
    {
        private string materialName;
        private string quantity;
        private string amountPerUnit;
        private string amount;

        // Accessors to the properties
        public string MaterialName { get => materialName; set => materialName = value; }
        public string Quantity { get => quantity; set => quantity = value; }
        public string AmountPerUnit { get => amountPerUnit; set => amountPerUnit = value; }
        public string Amount { get => amount; set => amount = value; }

    } // ResultView

    public class Material
    {

        private long id;
        private string description;

        public long Id { get => id; set => id = value; }
        public string Description { get => description; set => description = value; }

    }
    
    public class Vendor
    {
        private string id;
        private string name;

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }

    }

    // Outgoing receipt entity and her items
    public class ReceiptItem
    {
        private long receiptId;
        private long position;
        private string material;
        private string materialName;
        private decimal quantity;
        private decimal amountPerUnit;
        private decimal amount;

        public long ReceiptId { get => receiptId; set => receiptId = value; }
        public long Position { get => position; set => position = value; }
        public string Material { get => material; set => material = value; }
        public string MaterialName { get => materialName; set => materialName = value; }
        public decimal Quantity { get => quantity; set => quantity = value; }
        public decimal AmountPerUnit { get => amountPerUnit; set => amountPerUnit = value; }
        public decimal Amount { get => amount; set => amount = value; }

    }

    public class Receipt
    {
        private long id;
        private long actualAmount;

        // Items
        private IList<ReceiptItem> items;

        public Receipt()
        {
            items = new List<ReceiptItem>();
        }

        public long Id { get => id; set => id = value; }
        public long ActualAmount { get => actualAmount; set => actualAmount = value; }
        public IList<ReceiptItem> Items { get => items; set => items = value; }

        public void ClearItems()
        {
            items.Clear();
        }

        public void RemoveItem(ReceiptItem ri)
        {
            items.Remove(ri);
        }

        public void AddItem(ReceiptItem ri)
        {
            items.Add(ri);
        }

    }

} // end of namespace CashJournalModel