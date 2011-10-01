using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iloire_Facturacion.Controllers
{
    [Authorize]
    public class InvoiceDetailsController : Controller
    {
        private DBContext db = new DBContext();

        //
        // GET: /InvoiceDetails/

        public ViewResult Index()
        {
            var invoicedetails = db.InvoiceDetails.Include(i => i.Invoice);
            return View(invoicedetails.ToList());
        }

        //
        // GET: /InvoiceDetails/Details/5

        public ViewResult Details(int id)
        {
            InvoiceDetails invoicedetails = db.InvoiceDetails.Find(id);
            return View(invoicedetails);
        }

        //
        // GET: /InvoiceDetails/Create

        public ActionResult Create()
        {
            ViewBag.InvoiceID = new SelectList(db.Invoices, "InvoiceID", "Notes");
            return View();
        } 

        //
        // POST: /InvoiceDetails/Create

        [HttpPost]
        public ActionResult Create(InvoiceDetails invoicedetails)
        {
            if (ModelState.IsValid)
            {
                db.InvoiceDetails.Add(invoicedetails);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.InvoiceID = new SelectList(db.Invoices, "InvoiceID", "Notes", invoicedetails.InvoiceID);
            return View(invoicedetails);
        }
        
        //
        // GET: /InvoiceDetails/Edit/5
 
        public ActionResult Edit(int id)
        {
            InvoiceDetails invoicedetails = db.InvoiceDetails.Find(id);
            ViewBag.InvoiceID = new SelectList(db.Invoices, "InvoiceID", "Notes", invoicedetails.InvoiceID);
            return View(invoicedetails);
        }

        //
        // POST: /InvoiceDetails/Edit/5

        [HttpPost]
        public ActionResult Edit(InvoiceDetails invoicedetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoicedetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InvoiceID = new SelectList(db.Invoices, "InvoiceID", "Notes", invoicedetails.InvoiceID);
            return View(invoicedetails);
        }

        //
        // GET: /InvoiceDetails/Delete/5
 
        public ActionResult Delete(int id)
        {
            InvoiceDetails invoicedetails = db.InvoiceDetails.Find(id);
            return View(invoicedetails);
        }

        //
        // POST: /InvoiceDetails/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            InvoiceDetails invoicedetails = db.InvoiceDetails.Find(id);
            db.InvoiceDetails.Remove(invoicedetails);
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