using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

public class InvoiceDetails
{
    public int InvoiceDetailsID { get; set; }
    
    public int InvoiceID { get; set; }
    public virtual Invoice Invoice { get; set; }

    [Required]
    public string Article { get; set; }

    [Range(1, 100000, ErrorMessage = "Quantity must be between 1 and 100000")]
    public int Qty { get; set; }

    [Range(0.01, 999999999, ErrorMessage = "Price must be between 0.01 and 999999999")]
    public decimal Price { get; set; }

    [Range(0.01, 100, ErrorMessage = "VAT must be a % between 0.01 and 100")]
    public decimal VAT { get; set; }

    [DisplayName("Created")]
    public DateTime TimeStamp { get; set; }

    #region Calculated fields
    public decimal Total {
        get {
            return Qty * Price;
        }
    }

    public decimal VATAmount
    {
        get
        {
            return TotalPlusVAT - Total;
        }
    }

    public decimal TotalPlusVAT
    {
        get
        {
            return Total * (1 + VAT / 100);
        }
    }
    #endregion
}