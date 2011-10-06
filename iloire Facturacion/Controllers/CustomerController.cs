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
    public class CustomerController : Controller
    {
        private const int defaultPageSize = 10;
        private DBContext db = new DBContext();

        /*CUSTOM*/
        public ViewResultBase Search(string q, int? page)
        {
            IQueryable<Customer> customers;

            if (q.Length == 0) {
                customers = db.Customers;
            }
            else if (q.Length == 1)
            {
                ViewBag.LetraAlfabetica = q;
                //alfabet, first letter
                customers = (from c in db.Customers
                                 where c.Name.StartsWith(q)
                                 select c);
            }
            else { 
                //normal search
                 customers = (from c in db.Customers
                                 where c.Name.IndexOf(q) > -1
                                 select c);
                
            }

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var customersListPaged = customers.OrderBy(i => i.Name).ToPagedList(currentPageIndex, defaultPageSize);
            
            if (Request.IsAjaxRequest())
                return PartialView("Index", customersListPaged);
            else
                return View("Index", customersListPaged);
        }

        /*END CUSTOM*/
        
        
        //
        // GET: /Customer/

        public ViewResult Index(int? page)
        {
            //throw new Exception("ops, error");
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return View(db.Customers.ToList().ToPagedList(currentPageIndex, defaultPageSize));
        }

        //
        // GET: /Customer/Details/5

        public ViewResult Details(int id)
        {
            Customer customer = db.Customers.Find(id);
            return View(customer);
        }

        //
        // GET: /Customer/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Customer/Create

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(customer);
        }
        
        //
        // GET: /Customer/Edit/5
 
        public ActionResult Edit(int id)
        {
            Customer customer = db.Customers.Find(id);
            return View(customer);
        }

        //
        // POST: /Customer/Edit/5

        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        //
        // GET: /Customer/Delete/5
 
        public ActionResult Delete(int id)
        {
            Customer customer = db.Customers.Find(id);
            return View(customer);
        }

        //
        // POST: /Customer/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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