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
        [Test]
        public void TestListCustomers()
        {
            CustomerController c = new CustomerController();
            System.Web.Mvc.ViewResult result = c.Index(null);
            Assert.IsInstanceOf(typeof(IPagedList<Customer>), result.ViewData.Model);
            Assert.IsNotNull(result.ViewData.Model);
            //Assert.AreEqual(result.ViewName, "Index");
        }
    }
}

