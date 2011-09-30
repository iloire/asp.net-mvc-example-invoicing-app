using System;
using System.Collections;
using System.Collections.Generic;

public class Invoice
{
    public int InvoiceID { get; set; }

    public int CustomerID { get; set; }
    public virtual Customer Customer { get; set; }

    public string Notes { get; set; }
    public DateTime TimeStamp { get; set; }

    public virtual ICollection<InvoiceDetails> InvoiceDetails { get; set; }
}