using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

public class Summary
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }

    public List<Invoice> Invoices { get; set; }
    public List<Purchase> Purchases { get; set; }

    public decimal NetIncome { get; set; }
    public decimal NetExpense { get; set; }
    public decimal NetBenefit { get { return NetIncome - NetExpense; } }

    public decimal VATBalance { get; set; }
}