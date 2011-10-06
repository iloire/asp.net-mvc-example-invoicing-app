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

        /*CUSTOM*/
        public PartialViewResult IndexByInvoice(int id)
        {
            ViewBag.InvoiceID = id;
            var invoicedetails = db.InvoiceDetails.Include(i => i.Invoice).Where(i=>i.InvoiceID==id);
            return PartialView("Index", invoicedetails.ToList());
        }

    
        /*END CUSTOM*/

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
        //Optional: Invoice ID
        public ActionResult Create(int? id)
        {
            ViewBag.InvoiceID = new SelectList(db.Invoices, "InvoiceID", "Notes");
            Invoice invoice = null;
            InvoiceDetails i = null;

            if (id.HasValue) {
                invoice = (from ii in db.Invoices
                               where ii.InvoiceID == id
                               select ii).FirstOrDefault();

                if (invoice != null)
                {
                    i = new InvoiceDetails();
                    i.InvoiceID = id.Value;
                    i.Invoice = invoice;
                    i.Qty = 1;
                    i.VAT = 18;

                    i.TimeStamp = DateTime.Now;

                    ViewBag.InvoiceID = new SelectList(db.Invoices, "InvoiceID", "Notes", id.Value);
                }
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("Create", i);
            }
            else {
                return View("Create", i);
            }
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
                return PartialView("Index", db.InvoiceDetails.Where(i => i.InvoiceID == invoicedetails.InvoiceID));
            }

            ViewBag.InvoiceID = new SelectList(db.Invoices, "InvoiceID", "Notes", invoicedetails.InvoiceID);
            this.Response.StatusCode = 400;
            return PartialView("Create", invoicedetails);
        }
        
        //
        // GET: /InvoiceDetails/Edit/5

        public ActionResult Edit(int id)
        {
            InvoiceDetails invoicedetails = db.InvoiceDetails.Find(id);
            ViewBag.InvoiceID = new SelectList(db.Invoices, "InvoiceID", "Notes", invoicedetails.InvoiceID);
            return PartialView(invoicedetails);
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
                return RedirectToAction("IndexByInvoice", "InvoiceDetails", new { id = invoicedetails.InvoiceID });
            }       
            ViewBag.InvoiceID = new SelectList(db.Invoices, "InvoiceID", "Notes", invoicedetails.InvoiceID);
            this.Response.StatusCode = 400;
            return PartialView("Edit", invoicedetails);
        }

        //
        // GET: /InvoiceDetails/Delete/5
 
        public ActionResult Delete(int id)
        {
            InvoiceDetails invoicedetails = db.InvoiceDetails.Find(id);
            return PartialView(invoicedetails);
        }

        //
        // POST: /InvoiceDetails/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            InvoiceDetails invoicedetails = db.InvoiceDetails.Find(id);
            if (invoicedetails != null)
            {
                db.InvoiceDetails.Remove(invoicedetails);
                db.SaveChanges();
                return RedirectToAction("IndexByInvoice", "InvoiceDetails", new { id = invoicedetails.InvoiceID });
            }
            else {
                this.Response.StatusCode = 400;
                return Content("Record not found");
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}