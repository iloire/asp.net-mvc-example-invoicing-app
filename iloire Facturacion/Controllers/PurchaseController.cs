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
    public class PurchaseController : Controller
    {
        private DBContext db = new DBContext();

        /*CUSTOM*/
        public PartialViewResult RecentPurchases()
        {
            var invoices = db.Purchases.Include(i => i.Provider).OrderByDescending(t=>t.TimeStamp).Take(10);
            return PartialView("PurchasesListPartial", invoices.ToList());
        }

        /*END CUSTOM*/

        //
        // GET: /Purchase/

        public ViewResult Index()
        {
            var purchases = db.Purchases.Include(p => p.Provider);
            return View(purchases.ToList());
        }

        //
        // GET: /Purchase/Details/5

        public ViewResult Details(int id)
        {
            Purchase purchase = db.Purchases.Find(id);
            return View(purchase);
        }

        //
        // GET: /Purchase/Create

        public ActionResult Create()
        {
            ViewBag.ProviderID = new SelectList(db.Providers, "ProviderID", "Name");
            return View();
        } 

        //
        // POST: /Purchase/Create

        [HttpPost]
        public ActionResult Create(Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                db.Purchases.Add(purchase);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.ProviderID = new SelectList(db.Providers, "ProviderID", "Name", purchase.ProviderID);
            return View(purchase);
        }
        
        //
        // GET: /Purchase/Edit/5
 
        public ActionResult Edit(int id)
        {
            Purchase purchase = db.Purchases.Find(id);
            ViewBag.ProviderID = new SelectList(db.Providers, "ProviderID", "Name", purchase.ProviderID);
            return View(purchase);
        }

        //
        // POST: /Purchase/Edit/5

        [HttpPost]
        public ActionResult Edit(Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProviderID = new SelectList(db.Providers, "ProviderID", "Name", purchase.ProviderID);
            return View(purchase);
        }

        //
        // GET: /Purchase/Delete/5
 
        public ActionResult Delete(int id)
        {
            Purchase purchase = db.Purchases.Find(id);
            return View(purchase);
        }

        //
        // POST: /Purchase/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Purchase purchase = db.Purchases.Find(id);
            db.Purchases.Remove(purchase);
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