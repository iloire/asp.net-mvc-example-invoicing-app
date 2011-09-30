using System;

public class Purchase
{
    public int PurchaseID { get; set; }
    public string Article {get;set;}

    public decimal Price { get; set; }
    public decimal VAT { get; set; }

    public int ProviderID { get; set; }
    public virtual Provider Provider { get; set; }

    public string Notes { get; set; }
    public DateTime TimeStamp { get; set; }
}