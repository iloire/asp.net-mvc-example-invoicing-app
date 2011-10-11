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
    public class ProviderTest
    {
        [TestFixtureSetUp]
        public void TestSetup()
        {

        }

        [Test]
        public void TestListProvider()
        {
            ProviderController pc = new ProviderController();
            System.Web.Mvc.ViewResult result = pc.Index(null);
            Assert.IsNotNull(result.ViewData.Model);
            Assert.IsInstanceOf(typeof(IPagedList<Provider>), result.ViewData.Model);
        }


        [Test]
        public void TestAddProvider()
        {
            ProviderController pc = new ProviderController();

            Provider p = new Provider();
            p.Address = "Address dummy";
            p.City = "City dummy";
            p.CompanyNumber = "23423423424";
            p.CP = "508000";
            p.Email = "email@email.com";
            p.Fax = "342343434";
            p.Name = "Provider name dummy";
            p.Phone1 = "3423423423";
            p.Phone2 = "234234232";

            System.Web.Mvc.ActionResult result = pc.Create(p);

            Assert.IsInstanceOf(typeof(System.Web.Mvc.RedirectToRouteResult), result);
        }

        [Test]
        public void TestEditProvider()
        {
            ProviderController pc = new ProviderController();
            System.Web.Mvc.ViewResult result = pc.Index(null);

            Provider p = ((IPagedList<Provider>)result.ViewData.Model).First();
            System.Web.Mvc.ActionResult customerEdition = pc.Edit(p.ProviderID);

            //post edited
            p.Address = "Address dummy";
            p.City = "City dummy";
            p.CompanyNumber = "23423423424";
            p.CP = "508000";
            p.Email = "email@email.com";
            p.Fax = "342343434";
            p.Name = "Company name dummy";
            p.Phone1 = "3423423423";
            p.Phone2 = "234234232";

            System.Web.Mvc.ActionResult resultEditionView = pc.Edit(p.ProviderID);
            Assert.IsInstanceOf(typeof(System.Web.Mvc.ViewResult), resultEditionView);

            System.Web.Mvc.ActionResult resultEdition = pc.Edit(p);
            Assert.IsInstanceOf(typeof(System.Web.Mvc.RedirectToRouteResult), resultEdition);
        }

        [Test]
        public void TestDeleteProvider()
        {
            ProviderController pc = new ProviderController();
            System.Web.Mvc.ViewResult result = pc.Index(null);

            Provider c = ((IPagedList<Provider>)result.ViewData.Model).First();
            Assert.NotNull(c);

            //ask deletion action
            System.Web.Mvc.ActionResult providerAskDeletion = pc.Delete(c.ProviderID);
            Assert.IsInstanceOf(typeof(System.Web.Mvc.ViewResult), providerAskDeletion);

            //delete action
            System.Web.Mvc.ActionResult providerDeletion = pc.DeleteConfirmed(c.ProviderID);
            Assert.IsInstanceOf(typeof(System.Web.Mvc.RedirectToRouteResult), providerDeletion);
        }
    }
}

