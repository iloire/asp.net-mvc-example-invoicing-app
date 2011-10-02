using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Invoice
{
    public int InvoiceID { get; set; }

    public int CustomerID { get; set; }
    public virtual Customer Customer { get; set; }

    public string Name { get; set; }

    public string Notes { get; set; }
    public DateTime TimeStamp { get; set; }

    public bool Paid { get; set; }

    public virtual ICollection<InvoiceDetails> InvoiceDetails { get; set; }

    public decimal Total {
        get {
            if (InvoiceDetails == null)
                return 0;

            return InvoiceDetails.Sum(i => i.Total);
        }
    }

    public decimal TotalWithVAT
    {
        get
        {
            if (InvoiceDetails == null)
                return 0;

            return InvoiceDetails.Sum(i => i.TotalPlusVAT);
        }
    }
}