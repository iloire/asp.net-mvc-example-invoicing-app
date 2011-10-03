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
    public class ProviderController : Controller
    {
        private DBContext db = new DBContext();
        private const int defaultPageSize = 10;

        //
        // GET: /Provider/

        public ViewResult Index(int? page)
        {
            var providers = db.Providers.ToList();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var providersListPaged = providers.OrderBy(i => i.Name).ToPagedList(currentPageIndex, defaultPageSize);
            return View(providersListPaged);
        }

        //
        // GET: /Provider/Details/5

        public ViewResult Details(int id)
        {
            Provider provider = db.Providers.Find(id);
            return View(provider);
        }

        //
        // GET: /Provider/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Provider/Create

        [HttpPost]
        public ActionResult Create(Provider provider)
        {
            if (ModelState.IsValid)
            {
                db.Providers.Add(provider);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(provider);
        }
        
        //
        // GET: /Provider/Edit/5
 
        public ActionResult Edit(int id)
        {
            Provider provider = db.Providers.Find(id);
            return View(provider);
        }

        //
        // POST: /Provider/Edit/5

        [HttpPost]
        public ActionResult Edit(Provider provider)
        {
            if (ModelState.IsValid)
            {
                db.Entry(provider).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(provider);
        }

        //
        // GET: /Provider/Delete/5
 
        public ActionResult Delete(int id)
        {
            Provider provider = db.Providers.Find(id);
            return View(provider);
        }

        //
        // POST: /Provider/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Provider provider = db.Providers.Find(id);
            db.Providers.Remove(provider);
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