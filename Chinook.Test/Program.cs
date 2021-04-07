using System;
using Chinook.Dal;
using System.Collections.Generic;

namespace Chinook.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomerAdapter adapter = new CustomerAdapter();
            List<Customer> customers = adapter.GetAll();

            foreach (Customer customer in customers) 
            {
                Console.WriteLine("CustomerID: "+customer.CustomerId + ", First Name: " + customer.FirstName + ", Last Name: " + customer.LastName + ", Country: " + customer.Country + ", Email: " + customer.Email);
            }

            /*InvoiceAdapter adapter2 = new InvoiceAdapter();
            List<Invoice> invoices = adapter2.GetAll();

            foreach (Invoice invoice in invoices)
            {
                Console.WriteLine("InvoiceID: " +invoice.InvoiceId + ", CustomerID: " + invoice.CustomerId + ", Invoice Date: " + invoice.InvoiceDate + ", Invoice Total: " + invoice.Total);
            }*/

            Console.WriteLine("Please enter a new customer into the Database...");
            Customer newcustomer = new Customer();
            Console.WriteLine("First Name: ");
            newcustomer.FirstName = Console.ReadLine();
            Console.WriteLine("Last Name:");
            newcustomer.LastName = Console.ReadLine();
            Console.WriteLine("Country: ");
            newcustomer.Country = Console.ReadLine();
            Console.WriteLine("Email: ");
            newcustomer.Email = Console.ReadLine();

            CustomerAdapter addcustomer = new CustomerAdapter();
            addcustomer.InsertCustomer(newcustomer);
            Console.WriteLine("Customer added...");

            Console.ReadLine();
        }
    }
}
