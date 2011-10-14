using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

public class Purchase
{
    public int PurchaseID { get; set; }

    [Required]
    public string Article {get;set;}

    [Range(0.1, 999999999, ErrorMessage = "Price must be between 1 and 999999999")]
    public decimal Price { get; set; }

    [Range(0.00, 100.0, ErrorMessage = "VAT must be a % between 0 and 100")]
    public decimal VAT { get; set; }

    public int ProviderID { get; set; }
    public virtual Provider Provider { get; set; }

    public string Notes { get; set; }

    [DisplayName("Created")]
    public DateTime TimeStamp { get; set; }

    public int PurchaseTypeID { get; set; }
    [DisplayName("Expense category")]
    public virtual PurchaseType PurchaseType { get; set; }
   
    #region Calculated fields
    public decimal SubTotal
    {
        get
        {
            return Price;
        }
    }

    public decimal TotalWithVAT
    {

        get
        {
            return Price + (Price * VAT / 100);
        }
    }

    public decimal VATAmount
    {
        get
        {
            return TotalWithVAT - SubTotal;
        }
    }
    #endregion

}