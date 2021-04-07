using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chinook.Dal;
using ChinookDatabase.Models;

namespace ChinookDatabase.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            CustomerAdapter adapter = new CustomerAdapter();
            List<Customer> customers = adapter.GetAll();
            AllCustomersModel model = new AllCustomersModel();
            List<CustomerModels> customerModels = new List<CustomerModels>();
            foreach (Customer customer in customers)
            {
                CustomerModels customerModel = new CustomerModels();
                customerModel.CustomerId = customer.CustomerId;
                customerModel.FirstName = customer.FirstName;
                customerModel.LastName = customer.LastName;
                customerModel.Country = customer.Country;
                customerModel.Email = customer.Email;
                customerModels.Add(customerModel);
            }
            model.Customers = customerModels;
            return View(model);
        }
        public IActionResult Add()
        {
            CustomerModels model = new CustomerModels();
            return View(model);
        }
        public ActionResult Edit(int customerId)
        {
            CustomerAdapter customerAdapter = new CustomerAdapter();
            Customer customer = customerAdapter.GetById(customerId);
            if (customer == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                CustomerModels model = new CustomerModels();
                model.CustomerId = customer.CustomerId;
                model.FirstName = customer.FirstName;
                model.LastName = customer.LastName;
                return View(model);
            }
        }
        public ActionResult Delete(int customerId)
        {
            CustomerAdapter customerAdapter = new CustomerAdapter();
            bool returnValue = customerAdapter.DeleteCustomer(customerId);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Add(CustomerModels model)
        {
            if (ModelState.IsValid)
            {
                CustomerAdapter customerAdapter = new CustomerAdapter();
                Customer customer = new Customer();
                customer.FirstName = model.FirstName;
                customer.LastName = model.LastName;
                customer.Country = model.Country;
                customer.Email = model.Email;
                bool returnValue = customerAdapter.InsertCustomer(customer);
                if (returnValue)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(CustomerModels model)
        {
            if (ModelState.IsValid)
            {
                CustomerAdapter customerAdapter = new CustomerAdapter();
                Customer customer = new Customer();
                customer.FirstName = model.FirstName;
                customer.LastName = model.LastName;
                customer.CustomerId = model.CustomerId;
                customer.Country = model.Country;
                customer.Email = model.Email;
                bool returnValue = customerAdapter.UpdateCustomer(customer);
                if (returnValue)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            return View(model);
        }
    }
}
