using System;
using System.Collections.Generic;
using System.Linq;

namespace EduPartner.MvcApp.Data.Models
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public List<InvoiceItem> Items { get; set; }
        public InvoiceStatus Status { get; set; }

        public decimal TotalAmount()
        {
            return Items.Sum(i => i.Amount);
        }
    }
}
