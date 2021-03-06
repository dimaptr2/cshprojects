﻿/*
 * Here are classes are representing the model of data 
 */

using System;
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
        private string unit;
        private decimal quantity;
        private decimal amountPerUnit;
        private string taxType;
        private decimal taxRate;
        private decimal amount;

        // Accessors to the properties
        public string MaterialName { get => materialName; set => materialName = value; }
        public string Unit { get => unit; set => unit = value; }
        public decimal Quantity { get => quantity; set => quantity = value; }
        public decimal AmountPerUnit { get => amountPerUnit; set => amountPerUnit = value; }
        public string TaxType { get => taxType; set => taxType = value; }
        public decimal TaxRate { get => taxRate; set => taxRate = value; }
        public decimal Amount { get => amount; set => amount = value; }

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

    public class Customer
    {

        private string customerId;
        private string name;

        public string CustomerId { get => customerId; set => customerId = value; }
        public string Name { get => name; set => name = value; }

    }

    public class ReceiptHead
    {
        private long receiptId;
        private string deliveryDate;

        public long ReceiptId { get => receiptId; set => receiptId = value; }
        public string DeliveryDate { get => deliveryDate; set => deliveryDate = value; }

    }

    // Outgoing receipt entity and her items
    public class ReceiptItem
    {
        private long receiptId;
        private long position;
        private long material;
        private string materialName;
        private string unit;
        private decimal quantity;
        private decimal amountPerUnit;
        private decimal taxRate;
        private decimal amount;

        public long ReceiptId { get => receiptId; set => receiptId = value; }
        public long Position { get => position; set => position = value; }
        public long Material { get => material; set => material = value; }
        public string MaterialName { get => materialName; set => materialName = value; }
        public string Unit { get => unit; set => unit = value; }
        public decimal Quantity { get => quantity; set => quantity = value; }
        public decimal AmountPerUnit { get => amountPerUnit; set => amountPerUnit = value; }
        public decimal TaxRate { get => taxRate; set => taxRate = value; }
        public decimal Amount { get => amount; set => amount = value; }

    }

    public class SalesDataCache
    {
        private long id;
        private IList<ReceiptItem> items;
        
        public SalesDataCache()
        {
            items = new List<ReceiptItem>();
        }

        public long Id { get => id; set => id = value; }
        public IList<ReceiptItem> Items { get => items; }

        public void AddItem(ReceiptItem item)
        {
            items.Add(item);
        }

        public void RemoveItem(ReceiptItem item)
        {
            items.Remove(item);
        }

        public void ClearItems()
        {
            items.Clear();
        }

    }

    public class PrintForm
    {
        private decimal amount;
        private decimal vat10, vat18;
        private IList<ResultView> positions;

        public IList<ResultView> Positions { get => positions; set => positions = value; }
        public decimal Amount { get => amount; }
        public decimal Vat10 { get => vat10; }
        public decimal Vat18 { get => vat18; }

        public void InitPrintForm()
        {
            amount = 0M;
            vat10 = 0M;
            vat18 = 0M;
        }

        public void CalculateAmounts()
        {
            foreach (ResultView view in positions)
            {
                amount += view.Amount;
                switch (view.TaxType)
                {
                    case "10%":
                        vat10 += view.TaxRate;
                        break;
                    case "18%":
                        vat18 += view.TaxRate;
                        break;
                }
            }
            amount = Math.Round(amount, 2);
            vat10 = Math.Round(vat10, 2);
            vat18 = Math.Round(vat18, 2);
        }

    }  // print form

} // end of namespace CashJournalModel