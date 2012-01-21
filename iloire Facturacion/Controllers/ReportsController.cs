/*
	Iván Loire - www.iloire.com
	Please readme README file for license terms.

	ASP.NET MVC3 ACME Invocing app (demo app for training purposes)
*/
using System;
using System.Linq;
using System.Web.Mvc;

namespace iloire_Facturacion.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private InvoiceDB db = new InvoiceDB();
      
        private Summary GetSummary(DateTime fromDate, DateTime toDate)
        {
            Summary s = new Summary();

            s.From = fromDate;
            s.To = toDate;

            s.Invoices = (from i in db.Invoices
                            where i.TimeStamp >= fromDate && i.TimeStamp <= toDate
                            select i).ToList().Where(i=>!i.IsProposal).ToList();

            s.Purchases = (from p in db.Purchases
                             where p.TimeStamp >= fromDate && p.TimeStamp <= toDate
                             select p).ToList();


            s.NetExpense = s.Purchases.Sum(i => i.SubTotal);
            s.NetIncome = s.Invoices.Sum(i => i.NetTotal);

            s.VATReceived = s.Invoices.Sum(i => i.VATAmount);

            s.AmountPaid = s.Invoices.Where(i => i.Paid).Sum(i => i.TotalToPay);

            s.VATBalance = s.Invoices.Sum(i => i.VATAmount) - s.Purchases.Sum(p => p.VATAmount);

            return s;
        }

        //
        // GET: /Reports/

        public ActionResult ProfitAndLoss(int? quarter, int? year) {
            DateTime start, end;

            if (!year.HasValue || !quarter.HasValue)
            {
                int q, y;
                TaxDateHelper.CalculateQuarter(DateTime.Now, out q, out y, out start, out end);
                quarter = q;
                year = y;
            }
            else {
                start = TaxDateHelper.GetStartDate(quarter.Value, year.Value);
                end = TaxDateHelper.GetEndDate(quarter.Value, year.Value);
            }

            ViewBag.PurchaseTypes = (from p in db.PurchaseTypes
                               select p).ToList();

            QuarterSummary quarter_Summary = new QuarterSummary()
            {
                Year = year.Value,
                Month1 = GetSummary(start, start.AddMonths(1).AddDays(-1)),
                Month2 = GetSummary(start.AddMonths(1), start.AddMonths(2).AddDays(-1)),
                Month3 = GetSummary(start.AddMonths(2), end)
            };

            ViewBag.Year = year.Value;
            ViewBag.Quarter = quarter.Value;
            
            
            return View(quarter_Summary);
        }
        
        
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

        //[OutputCache(Duration=60)]
        public ActionResult YearSummary(int id)
        {
            YearSummary y=new YearSummary();
            y.Q1 = GetSummary(TaxDateHelper.GetStartDate(1, id), TaxDateHelper.GetStartDate(2, id).AddDays(-1));
            y.Q2 = GetSummary(TaxDateHelper.GetStartDate(2, id), TaxDateHelper.GetStartDate(3, id).AddDays(-1));
            y.Q3 = GetSummary(TaxDateHelper.GetStartDate(3, id), TaxDateHelper.GetStartDate(4, id).AddDays(-1));
            y.Q4 = GetSummary(TaxDateHelper.GetStartDate(4, id), TaxDateHelper.GetStartDate(1, id).AddYears(1).AddDays(-1));


            return PartialView("YearSummary", y);
        }
        
        [OutputCache(Duration = 60)]
        public ActionResult ThisQuarterSummary()
        {
            int quarter=0;
            int year=0;
            DateTime start;
            DateTime end;
            
            TaxDateHelper.CalculateQuarter(DateTime.Now, out quarter, out year, out start, out end);

            return PeriodSummary(start, end);
        }


        public ActionResult ByYear(int id) {
            return View(id);    
        }
    }
}
