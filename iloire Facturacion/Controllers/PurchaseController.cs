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
    public class PurchaseController : Controller
    {
        private InvoiceDB db = new InvoiceDB();
        private int defaultPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultPaginationSize"]);

        /*CUSTOM*/

        public ViewResultBase Search(string text, string from, string to, int? page)
        {
            Session["purchaseText"] = text;
            Session["purchaseFrom"] = from;
            Session ["purchaseTo"] = to;

            IQueryable<Purchase> expenses = db.Purchases;

            if (!string.IsNullOrWhiteSpace(from))
            {
                DateTime fromDate = DateTime.Parse(from, CultureInfo.CurrentUICulture);
                expenses = expenses.Where(t => t.TimeStamp >= fromDate);
            }
            if (!string.IsNullOrWhiteSpace(to))
            {
                DateTime toDate = DateTime.Parse(to, CultureInfo.CurrentUICulture);
                expenses = expenses.Where(t => t.TimeStamp <= toDate);
            }

            if (!string.IsNullOrWhiteSpace(text))
            {
                expenses = expenses.Where(t => (t.Notes.ToLower().IndexOf(text.ToLower()) > -1) || (t.Article.ToLower().IndexOf(text.ToLower()) > -1));
            }

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var expensesListPaged = expenses.OrderByDescending(i => i.TimeStamp).ToPagedList(currentPageIndex, defaultPageSize);

            if (Request.IsAjaxRequest())
                return PartialView("Index", expensesListPaged);
            else
                return View("Index", expensesListPaged);
        }

        public PartialViewResult RecentPurchases(int? top)
        {
            if (!top.HasValue) top = 10;
            var invoices = db.Purchases.Include(i => i.Provider).OrderByDescending(t=>t.TimeStamp).Take(top.Value);
            return PartialView("PurchasesListPartial", invoices.ToList());
        }

        public PartialViewResult RecentPurchasesByCustomer(int? providerID)
        {
            var invoices = db.Purchases.Include(i => i.Provider).Where(p=>p.ProviderID==providerID).OrderByDescending(t => t.TimeStamp).Take(10);
            return PartialView("PurchasesListPartial", invoices.ToList());
        }
        /*END CUSTOM*/

        //
        // GET: /Purchase/
        
        public ActionResult Index(int? page, string filter)
        {
            #region remember filter stuff
            if (filter == "clear")
            {
                Session["purchaseText"] = null;
                Session["purchaseFrom"] = null;
                Session["purchaseTo"] = null;
            }
            else {
                if ((Session["purchaseText"] != null) || (Session["purchaseFrom"] != null) || (Session["purchaseTo"] != null)) {                    
                    return RedirectToAction("Search", new { text = Session["purchaseText"], from = Session["purchaseFrom"], to = Session["purchaseTo"] });
                }
            }
            #endregion

            var purchases = db.Purchases.Include(p => p.Provider).Include(p => p.PurchaseType);
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return View(purchases.OrderByDescending(p=>p.TimeStamp).ToPagedList(currentPageIndex, defaultPageSize));
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
            Purchase p = new Purchase();
            p.TimeStamp = DateTime.Now;
            p.VAT = Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings["DefaultVAT"]);
            ViewBag.ProviderID = new SelectList(db.Providers, "ProviderID", "Name");
            ViewBag.PurchaseTypeID = new SelectList(db.PurchaseTypes, "PurchaseTypeID", "Name");
            return View(p);
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
            ViewBag.PurchaseTypeID = new SelectList(db.PurchaseTypes, "PurchaseTypeID", "Name");
            return View(purchase);
        }
        
        //
        // GET: /Purchase/Edit/5
 
        public ActionResult Edit(int id)
        {
            Purchase purchase = db.Purchases.Find(id);
            ViewBag.ProviderID = new SelectList(db.Providers, "ProviderID", "Name", purchase.ProviderID);
            ViewBag.PurchaseTypeID = new SelectList(db.PurchaseTypes, "PurchaseTypeID", "Name", purchase.PurchaseTypeID);
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
            ViewBag.PurchaseTypeID = new SelectList(db.PurchaseTypes, "PurchaseTypeID", "Name", purchase.PurchaseTypeID);
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