using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using iloire_Facturacion.Controllers;
using MvcPaging;

namespace iloire_Facturacion_Tests
{
    [TestFixture]
    public class CustomerTest
    {
        [TestFixtureSetUp]
        public void TestSetup() {
            System.Data.Entity.Database.SetInitializer(new EntitiesContextInitializer());
        }

        [Test]
        public void TestListCustomers()
        {
            CustomerController c = new CustomerController();
            System.Web.Mvc.ViewResult result = c.Index(null);
            Assert.IsNotNull(result.ViewData.Model);
            Assert.IsInstanceOf(typeof(IPagedList<Customer>), result.ViewData.Model);
        }


        [Test]
        public void TestAddCustomer()
        {
            CustomerController cc = new CustomerController();

            Customer c = new Customer();
            c.Address = "Address dummy";
            c.City = "City dummy";
            c.CompanyNumber = "23423423424";
            c.CP = "508000";
            c.Email = "email@email.com";
            c.Fax = "342343434";
            c.Name = "Company name dummy";
            c.Phone1 = "3423423423";
            c.Phone2 = "234234232";
            c.Notes = "A customer!";
            c.ContactPerson = "Mr customer";       

            System.Web.Mvc.ActionResult result = cc.Create(c);

            Assert.IsInstanceOf(typeof(System.Web.Mvc.PartialViewResult), result);
        }

        [Test]
        public void TestEditCustomer()
        {
            CustomerController cc = new CustomerController();
            System.Web.Mvc.ViewResult result = cc.Index(null);

            Customer c = ((IPagedList<Customer>)result.ViewData.Model).First();
            Assert.NotNull(c);
            System.Web.Mvc.ActionResult customerEdition = cc.Edit(c.CustomerID);

            //post edited
            c.Address = "Address dummy";
            c.City = "City dummy";
            c.CompanyNumber = "23423424";
            c.CP = "508000";
            c.Email = "email@domainemail.com";
            c.Fax = "342343434";
            c.Name = "Company name dummy";
            c.Phone1 = "3423423423";
            c.Phone2 = "234234232";

            //get
            System.Web.Mvc.ActionResult resultEditionView = cc.Edit(c.CustomerID);
            Assert.IsInstanceOf(typeof(System.Web.Mvc.PartialViewResult), resultEditionView);

            //post
            System.Web.Mvc.ActionResult resultEdition = cc.Edit(c);
            Assert.IsInstanceOf(typeof(System.Web.Mvc.PartialViewResult), resultEdition);
        }

        [Test]
        public void TestDeleteCustomer()
        {
            CustomerController cc = new CustomerController();
            System.Web.Mvc.ViewResult result = cc.Index(null);

            Customer c = ((IPagedList<Customer>)result.ViewData.Model).First();
            Assert.NotNull(c);

            //ask deletion action
            System.Web.Mvc.ActionResult customerAskDeletion = cc.Delete(c.CustomerID);
            Assert.IsInstanceOf(typeof(System.Web.Mvc.ViewResult), customerAskDeletion);

            //delete action
            System.Web.Mvc.ActionResult customerDeletion = cc.DeleteConfirmed(c.CustomerID);
            Assert.IsInstanceOf(typeof(System.Web.Mvc.RedirectToRouteResult), customerDeletion);
        }
    }
}

