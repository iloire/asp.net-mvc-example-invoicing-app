using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iloire_Facturacion.Controllers
{ 
    public class PurchaseTypeController : Controller
    {
        private InvoiceDB db = new InvoiceDB();

        //
        // GET: /PurchaseType/

        public ViewResult Index()
        {
            return View(db.PurchaseTypes.OrderBy(p=>p.Name).ToList());
        }

        //
        // GET: /PurchaseType/Details/5

        public ViewResult Details(int id)
        {
            PurchaseType purchasetype = db.PurchaseTypes.Find(id);
            return View(purchasetype);
        }

        //
        // GET: /PurchaseType/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /PurchaseType/Create

        [HttpPost]
        public ActionResult Create(PurchaseType purchasetype)
        {
            if (ModelState.IsValid)
            {
                db.PurchaseTypes.Add(purchasetype);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(purchasetype);
        }
        
        //
        // GET: /PurchaseType/Edit/5
 
        public ActionResult Edit(int id)
        {
            PurchaseType purchasetype = db.PurchaseTypes.Find(id);
            return View(purchasetype);
        }

        //
        // POST: /PurchaseType/Edit/5

        [HttpPost]
        public ActionResult Edit(PurchaseType purchasetype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchasetype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(purchasetype);
        }

        //
        // GET: /PurchaseType/Delete/5
 
        public ActionResult Delete(int id)
        {
            PurchaseType purchasetype = db.PurchaseTypes.Find(id);
            return View(purchasetype);
        }

        //
        // POST: /PurchaseType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            PurchaseType purchasetype = db.PurchaseTypes.Find(id);
            db.PurchaseTypes.Remove(purchasetype);
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