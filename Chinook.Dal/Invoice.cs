using System;
using System.Collections.Generic;
using System.Text;

namespace Chinook.Dal
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public decimal Total { get; set; }
        public DateTime InvoiceDate { get; set; }
    }
}
