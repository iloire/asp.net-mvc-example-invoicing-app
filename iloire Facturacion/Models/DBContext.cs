using System;
using System.Data.Entity;

public class DBContext : DbContext { 
    public DbSet<Customer> Customers {get; set;}
    public DbSet<Provider> Providers { get; set; }
    public DbSet<Invoice> Invoices {get; set;}
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<InvoiceDetails> InvoiceDetails { get; set; }

    public DbSet<User> Users { get; set; }

}