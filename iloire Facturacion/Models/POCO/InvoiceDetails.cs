using System;
using System.ComponentModel.DataAnnotations;

public class InvoiceDetails
{
    public int InvoiceDetailsID { get; set; }
    
    public int InvoiceID { get; set; }
    public virtual Invoice Invoice { get; set; }

    [Required]
    public string Article { get; set; }

    [Range(1, 100000, ErrorMessage = "Quantity must be between 1 and 100000")]
    public int Qty { get; set; }

    [Range(1, 999999999, ErrorMessage = "Price must be between 1 and 999999999")]
    public decimal Price { get; set; }

    [Range(1, 100, ErrorMessage = "VAT must be between 1 and 100")]
    public decimal VAT { get; set; }

    public DateTime TimeStamp { get; set; }

    public decimal Total {
        get {
            return Qty * Price;
        }
    }

    public decimal TotalPlusVAT
    {
        get
        {
            return Qty * Price * (1 + VAT / 100);
        }
    }
}