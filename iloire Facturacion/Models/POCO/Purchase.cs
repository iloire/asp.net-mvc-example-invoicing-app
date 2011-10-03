using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

public class Purchase
{
    public int PurchaseID { get; set; }

    [Required]
    public string Article {get;set;}

    public decimal Price { get; set; }
    public decimal VAT { get; set; }

    public int ProviderID { get; set; }
    public virtual Provider Provider { get; set; }

    public string Notes { get; set; }

    [DisplayName("Created")]
    public DateTime TimeStamp { get; set; }
}