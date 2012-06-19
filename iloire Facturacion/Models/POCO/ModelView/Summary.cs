using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

public class Summary
{
    public int Year { get; set; }

    public DateTime From { get; set; }
    public DateTime To { get; set; }

    public List<Invoice> Invoices { get; set; }
    public List<Purchase> Purchases { get; set; }
    public List<PurchaseType> PurchaseTypes { get; set; }

    public decimal AmountPaid { get; set; }
    public decimal NetIncome { get; set; }
    public decimal GrossIncome { get; set; }
    
    public decimal NetExpense { get; set; }
    public decimal GrossExpense { get; set; }
    public decimal NetBenefit { get { return NetIncome - NetExpense; } }

    public decimal VATReceived { get; set; }
    public decimal VATPaid { get; set; }

    public decimal VATBalance { get; set; }

    public decimal AdvancePaymentTaxPaid { get; set; }
}