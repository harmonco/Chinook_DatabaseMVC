using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ChinookDatabase.Models
{
    public class InvoiceModels
    {
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        [Required]
        [Display(Name = "Total Due")]
        public decimal Total { get; set; }
        [Required]
        [Display(Name = "Invoice Date")]
        public DateTime InvoiceDate { get; set; }
    }
    public class AllInvoicesModel
    {
        public List<InvoiceModels> Invoices { get; set; }
    }
}

