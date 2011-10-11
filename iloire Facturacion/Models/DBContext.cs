using System;
using System.Data.Entity;
using System.Collections.Generic;

public class InvoiceDB : DbContext {

    public InvoiceDB()
    {
        //Set initializer to populate data on database creation
        System.Data.Entity.Database.SetInitializer(new EntitiesContextInitializer());
    }

    public DbSet<Customer> Customers {get; set;}
    public DbSet<Provider> Providers { get; set; }
    public DbSet<Invoice> Invoices {get; set;}
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<InvoiceDetails> InvoiceDetails { get; set; }

    public DbSet<User> Users { get; set; }
}