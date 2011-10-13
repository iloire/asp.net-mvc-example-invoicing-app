/*
	Iván Loire - www.iloire.com
	Please readme README file for license terms.

	ASP.NET MVC3 ACME Invocing app (demo app for training purposes)
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using System.Globalization;

namespace iloire_Facturacion.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {

        private InvoiceDB db = new InvoiceDB();
        private const int defaultPageSize=10;

        /*CUSTOM*/

        public ViewResultBase Search(string text, string from, string to, int? page)
        {
            IQueryable<Invoice> invoices = db.Invoices;

            if (!string.IsNullOrWhiteSpace(from))
            {
                DateTime fromDate = DateTime.Parse(from, CultureInfo.CurrentUICulture);
                invoices = invoices.Where(t => t.TimeStamp >= fromDate);
            }
            if (!string.IsNullOrWhiteSpace(to)) 
            {
                DateTime toDate = DateTime.Parse(to, CultureInfo.CurrentUICulture);
                invoices = invoices.Where(t => t.TimeStamp <= toDate);
            }

            if (!string.IsNullOrWhiteSpace(text))
            {
                invoices = invoices.Where(t => t.Notes.ToLower().IndexOf(text.ToLower())>-1);
            }

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var invoicesListPaged = invoices.OrderByDescending(i => i.TimeStamp).ToPagedList(currentPageIndex, defaultPageSize);

            if (Request.IsAjaxRequest())
                return PartialView("Index", invoicesListPaged);
            else
                return View("Index", invoicesListPaged);
        }

        public PartialViewResult UnPaidInvoices()
        {
            var invoices = db.Invoices.Include(i => i.Customer).Where(i => i.Paid == false && i.DueDate >= DateTime.Now).OrderBy(i => i.DueDate);
            return PartialView("InvoicesListPartial", invoices.ToList());
        }

        public PartialViewResult OverDueInvoices()
        {
            var invoices = db.Invoices.Include(i => i.Customer).Where(i => i.Paid == false && i.DueDate<DateTime.Now).OrderBy(i => i.DueDate);
            return PartialView("InvoicesListPartial", invoices.ToList());
        }

        public PartialViewResult LastInvoicesByCustomer(int id)
        {
            var invoices = db.Invoices.Include(i => i.Customer).Where(i => i.CustomerID == id).OrderByDescending(i=>i.TimeStamp);
            return PartialView("InvoicesListPartial", invoices.ToList());  
        }

        /*END CUSTOM*/

        //
        // GET: /Invoice/

        public ViewResult Index(int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var invoices = db.Invoices.Include(i => i.Customer).OrderByDescending(i=>i.TimeStamp);
            return View(invoices.ToList().ToPagedList(currentPageIndex, defaultPageSize));
        }

        //
        // GET: /Invoice/Details/5

        public ViewResult Print(int id)
        {
            ViewBag.Print = true;
            ViewBag.MyCompany = System.Configuration.ConfigurationManager.AppSettings["MyCompanyName"];
            ViewBag.MyCompanyID = System.Configuration.ConfigurationManager.AppSettings["MyCompanyID"];
            ViewBag.MyCompanyAddress = System.Configuration.ConfigurationManager.AppSettings["MyCompanyAddress"];
            ViewBag.MyCompanyPhone = System.Configuration.ConfigurationManager.AppSettings["MyCompanyPhone"];
 
            Invoice invoice = db.Invoices.Find(id);
            return View(invoice);
        }

        //
        // GET: /Invoice/Create

        public ActionResult Create()
        {
            Invoice i = new Invoice();
            i.TimeStamp = DateTime.Now;
            i.DueDate = DateTime.Now.AddDays(30); //30 days after today
            i.AdvancePaymentTax = Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings["DefaultAdvancePaymentTax"]); 
            
            //generate next invoice number
            var next_invoice = (from inv in db.Invoices 
                                orderby inv.InvoiceNumber descending
                                select inv).FirstOrDefault();
            if (next_invoice!=null)
                i.InvoiceNumber = next_invoice.InvoiceNumber + 1;

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name");
            return View(i);
        } 

        //
        // POST: /Invoice/Create

        [HttpPost]
        public ActionResult Create(Invoice invoice)
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name", invoice.CustomerID);
            if (ModelState.IsValid)
            {
                //make sure invoice number doesn't exist
                var invoice_exists = (from inv in db.Invoices where inv.InvoiceNumber==invoice.InvoiceNumber select inv).FirstOrDefault();
                if (invoice_exists != null) {
                    ModelState.AddModelError("InvoiceNumber", "Invoice with that number already exists");
                    return View(invoice);
                }
                db.Invoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Edit", "Invoice", new { id = invoice.InvoiceID });  
            }
            return View(invoice);
        }
        
        //
        // GET: /Invoice/Edit/5
 
        public ActionResult Edit(int id)
        {
            Invoice invoice = db.Invoices.Find(id);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name", invoice.CustomerID);
            return View(invoice);
        }

        //
        // POST: /Invoice/Edit/5

        [HttpPost]
        public ActionResult Edit(Invoice invoice)
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name", invoice.CustomerID);
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(invoice);
        }

        //
        // GET: /Invoice/Delete/5
 
        public ActionResult Delete(int id)
        {
            Invoice invoice = db.Invoices.Find(id);
            return View(invoice);
        }

        //
        // POST: /Invoice/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Invoice invoice = db.Invoices.Find(id);
            db.Invoices.Remove(invoice);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}