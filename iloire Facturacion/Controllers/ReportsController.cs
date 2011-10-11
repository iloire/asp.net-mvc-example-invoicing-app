/*
	Iván Loire - www.iloire.com
	Please readme README file for license terms.

	ASP.NET MVC3 ACME Invocing app (demo app for training purposes)
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iloire_Facturacion.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        DateTime startQ1 = new DateTime(DateTime.Now.Year, 1, 1);
        DateTime startQ2 = new DateTime(DateTime.Now.Year, 4, 1);
        DateTime startQ3 = new DateTime(DateTime.Now.Year, 7, 1);
        DateTime startQ4 = new DateTime(DateTime.Now.Year, 10, 1);

        private InvoiceDB db = new InvoiceDB();
      
        private Summary GetSummary(DateTime fromDate, DateTime toDate)
        {
            Summary s = new Summary();
            s.From = fromDate;
            s.To = toDate;

            s.Invoices = (from i in db.Invoices
                            where i.TimeStamp >= fromDate && i.TimeStamp <= toDate
                            select i).ToList();

            s.Purchases = (from p in db.Purchases
                             where p.TimeStamp >= fromDate && p.TimeStamp <= toDate
                             select p).ToList();

            s.NetExpense = s.Purchases.Sum(i => i.SubTotal);
            s.NetIncome = s.Invoices.Sum(i => i.NetTotal);

            s.VATBalance = s.Invoices.Sum(i => i.VATAmount) - s.Purchases.Sum(p => p.VATAmount);

            return s;
        }

        //
        // GET: /Reports/

        public ActionResult PeriodSummary(DateTime fromDate, DateTime toDate)
        {
            return PartialView("PeriodSummary", GetSummary(fromDate, toDate));
        }

        public ActionResult ThisMonthSummary()
        {
            return PeriodSummary(
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), 
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))
                );
        }

        [OutputCache(Duration=60)]
        public ActionResult ThisYearSummary()
        {
            YearSummary y=new YearSummary();
            y.Q1 = GetSummary(startQ1,startQ2.AddDays(-1));
            y.Q2 = GetSummary(startQ2,startQ3.AddDays(-1));
            y.Q3 = GetSummary(startQ3,startQ4.AddDays(-1));
            y.Q4 = GetSummary(startQ4,startQ1.AddYears(1).AddDays(-1));


            return PartialView("YearSummary", y);
        }

        [OutputCache(Duration = 60)]
        public ActionResult ThisQuarterSummary()
        {
            DateTime start;
            DateTime end;

            if (DateTime.Now < startQ2) {
                start = startQ1;
                end = startQ2.AddDays(-1);
            }
            else if (DateTime.Now < startQ3) {
                start = startQ2;
                end = startQ3.AddDays(-1);
            }
            else if (DateTime.Now < startQ4)
            {
                start = startQ3;
                end = startQ4.AddDays(-1);
            }
            else { 
                start = startQ4;
                end = new DateTime(DateTime.Now.Year, 12, 31);
            }

            return PeriodSummary(start, end);
        }

    }
}
