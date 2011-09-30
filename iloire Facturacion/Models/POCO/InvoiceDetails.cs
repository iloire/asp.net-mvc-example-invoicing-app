using System;

public class InvoiceDetails
{
    public int InvoiceDetailsID { get; set; }
    
    public int InvoiceID { get; set; }
    public virtual Invoice Invoice { get; set; }

    public string Article { get; set; }
    public int Qty { get; set; }
    public decimal Price { get; set; }
    public decimal VAT { get; set; }

    public DateTime TimeStamp { get; set; }
}