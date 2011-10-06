using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;

namespace iloire_Facturacion.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        private DBContext db = new DBContext();
        private const int defaultPageSize=10;

        /*CUSTOM*/
        public PartialViewResult UnPaidInvoices()
        {
            var invoices = db.Invoices.Include(i => i.Customer).Where(i=>i.Paid==false);
            return PartialView("InvoicesListPartial", invoices.ToList());
        }

        public PartialViewResult LastInvoicesByCustomer(int id)
        {
            var invoices = db.Invoices.Include(i => i.Customer).Where(i => i.CustomerID == id);
            return PartialView("InvoicesListPartial", invoices.ToList());  
        }

        /*END CUSTOM*/

        //
        // GET: /Invoice/

        public ViewResult Index(int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var invoices = db.Invoices.Include(i => i.Customer);
            return View(invoices.ToList().ToPagedList(currentPageIndex, defaultPageSize));
        }

        //
        // GET: /Invoice/Details/5

        public ViewResult Print(int id)
        {
            ViewBag.Print = true;
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
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name");
            return View(i);
        } 

        //
        // POST: /Invoice/Create

        [HttpPost]
        public ActionResult Create(Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Invoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Edit", "Invoice", new { id = invoice.InvoiceID });  
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name", invoice.CustomerID);
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
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name", invoice.CustomerID);
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