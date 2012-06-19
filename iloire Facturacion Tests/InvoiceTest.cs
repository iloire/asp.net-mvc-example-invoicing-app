using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using iloire_Facturacion.Controllers;
using MvcPaging;
using System.Data.Entity.Infrastructure;
using System.Web.Mvc;

namespace iloire_Facturacion_Tests
{
    [TestFixture]
    public class InvoiceTest
    {
        [TestFixtureSetUp]
        public void TestSetup()
        {

        }

        [Test]
        public void TestListInvoices()
        {
            InvoiceController ic = new InvoiceController();
            System.Web.Mvc.ViewResult result = ic.Index(null,null,null) as ViewResult;
            Assert.IsNotNull(result.ViewData.Model);
            Assert.IsInstanceOf(typeof(IPagedList<Invoice>), result.ViewData.Model);
        }


        [Test]
        public void TestAddInvoice()
        {
            InvoiceController ic = new InvoiceController();

            //load first customer by calling DBcontext
            InvoiceDB db = new InvoiceDB();
            Customer c = db.Customers.First();
            ((IObjectContextAdapter)db).ObjectContext.Detach(c); //http://stackoverflow.com/questions/4168073/entity-framework-code-first-no-detach-method-on-dbcontext
            Assert.NotNull(c);

            Invoice i = new Invoice();
            i.CustomerID = c.CustomerID;
            i.Customer = c;
            i.AdvancePaymentTax = 10;
            i.Notes = "Invoice notes";
            i.TimeStamp = DateTime.Now;
            i.DueDate = DateTime.Now.AddDays(90);
            i.Paid = false;
            i.Name = "Test invoice";
         
            System.Web.Mvc.ActionResult resultAdd = ic.Create(i);

            Assert.IsInstanceOf(typeof(System.Web.Mvc.ViewResult), resultAdd);
        }

        [Test]
        public void TestEditInvoice()
        {
            InvoiceController ic = new InvoiceController();
            ActionResult result = ic.Index(null,null,null);
            ViewResult view = result as ViewResult;
            Invoice i = ((IPagedList<Invoice>)view.ViewData.Model).First();
            System.Web.Mvc.ActionResult invoiceEdition = ic.Edit(i.InvoiceID);

            //post edited
            i.Name = "Change invoice name test";

            //get
            System.Web.Mvc.ActionResult resultEditionView = ic.Edit(i.InvoiceID);
            Assert.IsInstanceOf(typeof(System.Web.Mvc.ViewResult), resultEditionView);

            //post
            System.Web.Mvc.ActionResult resultEdition = ic.Edit(i);
            Assert.IsInstanceOf(typeof(System.Web.Mvc.RedirectToRouteResult), resultEdition);
        }

        [Test]
        public void TestDeleteInvoice()
        {
            InvoiceController ic = new InvoiceController();
            System.Web.Mvc.ViewResult result = ic.Index(null,null,null) as ViewResult;

            Invoice i = ((IPagedList<Invoice>)result.ViewData.Model).First();
            Assert.NotNull(i);

            //ask deletion action
            System.Web.Mvc.ActionResult invoiceAskDeletion = ic.Delete(i.InvoiceID);
            Assert.IsInstanceOf(typeof(System.Web.Mvc.ViewResult), invoiceAskDeletion);

            //delete action
            System.Web.Mvc.ActionResult invoiceDeletion = ic.DeleteConfirmed(i.InvoiceID);
            Assert.IsInstanceOf(typeof(System.Web.Mvc.RedirectToRouteResult), invoiceDeletion);
        }
    }
}

