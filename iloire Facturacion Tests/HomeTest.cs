using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using iloire_Facturacion.Controllers;

namespace iloire_Facturacion_Tests
{
    [TestFixture]
    public class HomeTest
    {
        [TestFixtureSetUp]
        public void TestSetup()
        {
         
        }

        [Test]
        public void TestHomeIndex()
        {
            HomeController hc = new HomeController();
            System.Web.Mvc.ActionResult result = hc.Index();
            Assert.IsInstanceOf(typeof(System.Web.Mvc.ViewResult), result);
        }
    }
}
