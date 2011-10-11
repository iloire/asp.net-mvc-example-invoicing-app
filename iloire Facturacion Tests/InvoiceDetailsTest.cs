using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using iloire_Facturacion.Controllers;
using MvcPaging;
using System.Data.Entity.Infrastructure;
using Moq;
using System.Web;
using System.Web.Mvc;

namespace iloire_Facturacion_Tests
{
    internal class MockHttpContext : System.Web.HttpContextBase
    {
        public override HttpRequestBase Request
        {
            get
            {
                var mockRequest = new Mock<HttpRequestBase>();
                return mockRequest.Object;
            }
        }
    }

    [TestFixture]
    public class InvoiceDetailsTest
    {
        [TestFixtureSetUp]
        public void TestSetup()
        {
            InvoiceDB db = new InvoiceDB();
            if (db.Invoices.Count() == 0) {
                EntitiesContextInitializer e = new EntitiesContextInitializer();
                e.InitializeDatabase(db);
            }
        }

        [Test]
        public void TestListInvoiceDetailsForInvoice()
        {
            InvoiceDB db = new InvoiceDB();
            Invoice i = db.Invoices.First();
            ((IObjectContextAdapter)db).ObjectContext.Detach(i); //http://stackoverflow.com/questions/4168073/entity-framework-code-first-no-detach-method-on-dbcontext
            Assert.NotNull(i);

            InvoiceDetailsController idc = new InvoiceDetailsController();
            System.Web.Mvc.PartialViewResult result = idc.IndexByInvoice(i.InvoiceID);

            Assert.IsInstanceOf(typeof(List<InvoiceDetails>), result.ViewData.Model);
        }


        [Test]
        public void TestAddInvoiceDetails()
        {
            InvoiceDB db = new InvoiceDB();
            Invoice i = db.Invoices.First();
            ((IObjectContextAdapter)db).ObjectContext.Detach(i); //http://stackoverflow.com/questions/4168073/entity-framework-code-first-no-detach-method-on-dbcontext
            Assert.NotNull(i);

            InvoiceDetailsController idc = new InvoiceDetailsController();
            idc.ControllerContext = new ControllerContext() { HttpContext=new MockHttpContext() };

            InvoiceDetails id = new InvoiceDetails();
            id.TimeStamp = DateTime.Now;
            id.Invoice = i;
            id.Qty = 1;
            id.Price = 100;
            id.VAT = 18;
            id.Article = "Invoice details test";
            
            //get 
            System.Web.Mvc.ActionResult resultAddView = idc.Create(i.InvoiceID);
            Assert.IsInstanceOf(typeof(System.Web.Mvc.ViewResult), resultAddView);

            //post
            System.Web.Mvc.ActionResult resultAdd = idc.Create(id);
            Assert.IsInstanceOf(typeof(System.Web.Mvc.ViewResult), resultAdd);

            Assert.AreEqual(((System.Web.Mvc.ViewResult)resultAdd).ViewName, "Index");
        }

        [Test]
        public void TestEditInvoiceDetails()
        {
            InvoiceDetailsController idc = new InvoiceDetailsController();
            System.Web.Mvc.ViewResult result = idc.Index();

            InvoiceDetails id = ((List<InvoiceDetails>)result.ViewData.Model).First();
            System.Web.Mvc.ActionResult invoiceDetailsEdition = idc.Edit(id.InvoiceDetailsID);

            //post edited
            id.Price= 9999;

            System.Web.Mvc.ActionResult resultEdition = idc.Edit(id);
            Assert.IsInstanceOf(typeof(System.Web.Mvc.RedirectToRouteResult), resultEdition);
        }


        [Test]
        public void TestDeleteInvoiceDetails()
        {
            InvoiceDetailsController idc = new InvoiceDetailsController();
            System.Web.Mvc.ViewResult result = idc.Index();

            InvoiceDetails id = ((List<InvoiceDetails>)result.ViewData.Model).First();
            Assert.NotNull(id);

            //ask deletion action
            System.Web.Mvc.ActionResult invoiceDetailsAskDeletion = idc.Delete(id.InvoiceDetailsID);
            Assert.IsInstanceOf(typeof(System.Web.Mvc.PartialViewResult), invoiceDetailsAskDeletion);

            //delete action
            System.Web.Mvc.ActionResult invoiceDetailDeletion = idc.DeleteConfirmed(id.InvoiceDetailsID);
            Assert.IsInstanceOf(typeof(System.Web.Mvc.RedirectToRouteResult), invoiceDetailDeletion);
        }
    }
}
