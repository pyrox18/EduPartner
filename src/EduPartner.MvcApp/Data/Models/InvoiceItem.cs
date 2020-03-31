using System;

namespace EduPartner.MvcApp.Data.Models
{
    public class InvoiceItem
    {
        public Guid Id { get; set; }
        public string Details { get; set; }
        public decimal Amount { get; set; }
    }
}
