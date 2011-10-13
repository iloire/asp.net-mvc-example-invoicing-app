using System;
using System.Data.Entity;
using System.Collections.Generic;

public class InvoiceDB : DbContext {

    public InvoiceDB()
    {
        
    }

    public DbSet<Customer> Customers {get; set;}
    public DbSet<Provider> Providers { get; set; }
    public DbSet<Invoice> Invoices {get; set;}
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<InvoiceDetails> InvoiceDetails { get; set; }

    public DbSet<User> Users { get; set; }
}