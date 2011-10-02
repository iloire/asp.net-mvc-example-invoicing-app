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

            if (id.HasValue) {
                var invoice = (from i in db.Invoices
                               where i.InvoiceID == id
                               select i).FirstOrDefault();
                
                if (invoice != null) {
                    InvoiceDetails i = new InvoiceDetails();
                    i.InvoiceID = id.Value;
                    i.Invoice = invoice;

                    i.TimeStamp = DateTime.Now;

                    ViewBag.InvoiceID = new SelectList(db.Invoices, "InvoiceID", "Notes", id.Value);
                    return View(i);
                }
                else
                    return View();
            }
            else
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
                return RedirectToAction("Details", "Invoice", new { id = invoicedetails.InvoiceID });
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
                return RedirectToAction("Details", "Invoice", new  { id = invoicedetails.InvoiceID });
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
            return RedirectToAction("Details", "Invoice", new { id = invoicedetails.InvoiceID });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}