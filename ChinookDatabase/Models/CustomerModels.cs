using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using Chinook.Dal;
using System.ComponentModel.DataAnnotations;

namespace ChinookDatabase.Models
{
    public class CustomerModels
    {
        public int CustomerId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
    public class AllCustomersModel
    {
        public List<CustomerModels> Customers { get; set; }
    }
}
