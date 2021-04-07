using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chinook.Dal;
using ChinookDatabase.Models;

namespace ChinookDatabase.Controllers
{
    public class InvoiceController : Controller
    {
        public IActionResult Index(int customerId = 0)
        {
            InvoiceAdapter adapter = new InvoiceAdapter();
            List<Invoice> invoices = new List<Invoice>();
            if (customerId == 0)
            {
                invoices = adapter.GetAll();
            }
            else
            {
                invoices = adapter.GetByCustomerId(customerId);
            }
            AllInvoicesModel model = new AllInvoicesModel();
            List<InvoiceModels> invoiceModels = new List<InvoiceModels>();
            foreach (Invoice invoice in invoices)
            {
                InvoiceModels invoiceModel = new InvoiceModels();
                invoiceModel.InvoiceId = invoice.InvoiceId;
                invoiceModel.CustomerId = invoice.CustomerId;
                invoiceModel.Total = invoice.Total;
                invoiceModel.InvoiceDate = invoice.InvoiceDate;
                invoiceModels.Add(invoiceModel);
            }
            model.Invoices = invoiceModels;
            return View(model);
        }
        public IActionResult Add(int customerId)
        {
            InvoiceModels model = new InvoiceModels();
            model.CustomerId = customerId;
            return View(model);
        }
        [HttpPost]
        public ActionResult Add(InvoiceModels model)
        {
            if (ModelState.IsValid)
            {
                InvoiceAdapter customerAdapter = new InvoiceAdapter();
                Invoice invoice = new Invoice();
                invoice.CustomerId = model.CustomerId;
                invoice.Total = model.Total;
                invoice.InvoiceDate = model.InvoiceDate;
                bool returnValue = customerAdapter.InsertInvoice(invoice);
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
        public IActionResult Edit(int invoiceId)
        {
            InvoiceAdapter invoiceAdapter = new InvoiceAdapter();
            Invoice invoice = invoiceAdapter.GetById(invoiceId);
            if (invoice == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                InvoiceModels model = new InvoiceModels();
                model.InvoiceId = invoice.InvoiceId;
                model.CustomerId = invoice.CustomerId;
                model.Total = invoice.Total;
                model.InvoiceDate = invoice.InvoiceDate;
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult Edit(InvoiceModels model)
        {
            if (ModelState.IsValid)
            {
                InvoiceAdapter invoiceAdapter = new InvoiceAdapter();
                Invoice invoice = new Invoice();
                invoice.InvoiceId = model.InvoiceId;
                invoice.CustomerId = model.CustomerId;
                invoice.Total = model.Total;
                invoice.InvoiceDate = model.InvoiceDate;
                bool returnValue = invoiceAdapter.UpdateInvoice(invoice);
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
        public IActionResult Delete(int invoiceId)
        {
            InvoiceAdapter invoiceAdapter = new InvoiceAdapter();
            bool returnValue = invoiceAdapter.DeleteInvoice(invoiceId);
            return RedirectToAction("Index");
        }

    }
}
