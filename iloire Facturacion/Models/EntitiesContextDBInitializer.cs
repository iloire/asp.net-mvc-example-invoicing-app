using System.Data.Entity;
using System.Collections.Generic;
using System;
using System.Data.Entity.Validation;

public class EntitiesContextInitializer : DropCreateDatabaseIfModelChanges<InvoiceDB>
{
    protected override void Seed(InvoiceDB context)
    {
        #region Add Users
        List<User> users = new List<User>{
            new User { Name="Default user", Login="user", Password="pass", Email="hello2@user.com", Enabled=true}
        };
        foreach (User u in users)
        {
            context.Users.Add(u);
        }
        #endregion

        #region let's add some dummy customer data:
        List<Customer> customers = new List<Customer>
        {
            new Customer {Name="ACME International S.L", ContactPerson="Miguel Pérez", Address="12 Stree NY", CP="232323", CompanyNumber="3424324342", City="New York", Phone1="223-23232323", Fax="233-333333", Email="hello@hello.com"},
            new Customer {Name="Apple Inc.", ContactPerson="Juan Rodriguez", Address="1233 Street NY", CP="232323", CompanyNumber="23232323", City="NN CA", Phone1="343-23232323", Fax="233-333333", Email="apple@hello.com"},
            new Customer {Name="Zaragoza Activa", ContactPerson="José Ángel García", Address="Edificio: Antigua Azucarera, Mas de las Matas, 20 Planta B", CP="50015", CompanyNumber="BBBBBB", City="Zaragoza", Phone1="343-23232323", Fax="233-333333", Email="zaragozaactiva@hello.com"},
            new Customer {Name="Conecta S.L", ContactPerson="Rocío Ruíz", Address="C/ San Flores 213", CP="50800", CompanyNumber="BBBBBB", City="Zaragoza", Phone1="343-23232323", Fax="233-333333", Email="contacta@hello.com"},
            new Customer {Name="VitaminasDev", ContactPerson="Antonio Roy", Address="C/ San Pedro 79 2", CP="50800", CompanyNumber="29124609", City="Zuera, Zaragoza", Phone1="654 249068", Fax="", Email="hola@vitaminasdev.com"}
        };
        for (int i = 0; i < 5; i++)
        {
            customers.Add(new Customer()
            {
                ContactPerson="Contact person for " + i,
                Notes ="Notes for "+ i,
                Name = "Extra customer " + i,
                Address = "Address for customer" + i,
                City = "Zaragoza",
                CompanyNumber = "212121212" + i,
                CP = "50800",
                Phone1 = "2323-2222" + i,
                Email = "email@customer" + i + ".com"
            });
        }
        foreach (Customer c in customers)
        {
            context.Customers.Add(c);
        }
        #endregion

        #region Add some dummy random invoices
        var dummy_services = new string[] { "ASP.NET MVC3 training", ".NET training, ASP.NET MVC3 consultancy", "ASP.NET MVC3 in-house training" };

        int invoice_number = 1;
        for (int m= 1; m <= DateTime.Now.Month; m++)
        {
            for (int i=0;i<5;i++){
                Invoice invoice = new Invoice();
                invoice.InvoiceNumber = invoice_number;
                invoice.Customer = customers[new Random(m).Next(0, customers.Count - 1)]; //random customer
                invoice.AdvancePaymentTax = 15;
                invoice.Name = "Consulting services, as detailed in the invoice";
                invoice.TimeStamp = new DateTime(2011, m, new Random().Next(1, 28)); //random date (this month)
                invoice.DueDate = invoice.TimeStamp.AddDays(90);
                invoice.Notes = invoice.Name + " notes";
                invoice.Paid = new Random().Next(0, 10)>=1; //low probability of unpaid
                invoice.CustomerID = invoice.Customer.CustomerID;

                invoice_number++;

                int number_invoice_details = new Random().Next(4, 10);
                for (int id = 0; id < number_invoice_details; id++)
                {
                    invoice.InvoiceDetails.Add(new InvoiceDetails()
                    {
                        Article = dummy_services[new Random(id).Next(0, dummy_services.Length)],
                        Price = 70,
                        Qty = new Random().Next(5, 10), //random qty
                        VAT = 18,
                        TimeStamp = invoice.TimeStamp,
                    });
                }
                context.Invoices.Add(invoice);
            }
        }
        #endregion

        #region let's add a few providers
        List<Provider> providers=new List<Provider>();
        int providers_count = 80;
        var dummy_provider_names = new string[] { "Ebay", "PayPal", "Computer Store","MarketPlace", "Hard disks from China Inc.", "Printing items Inc." };
        for (int i = 0; i < providers_count; i++)
        {
            providers.Add(new Provider()
            {
                Name = dummy_provider_names[new Random(i).Next(0, dummy_provider_names.Length)] + " " + i,
                Address = "Address for provider" + i,
                City = "Zaragoza",
                CompanyNumber = "212121212" + i,
                CP = "50800",
                Phone1 = "2323-2222" + i,
                Email = "dummy_email@provider" + i + ".com"
            });
        }
        foreach (Provider p in providers)
        {
            context.Providers.Add(p);
        }
        #endregion

        #region AddExpense types
        var expense_cats = new string[] { "Automobile", "Contractors", "Marketing", "Meals", "Medical", "Misc", "Office supplies", "Rent", "Telephone/Communications", "Travel" };

        List<PurchaseType> expenseCats = new List<PurchaseType>();
        for (int ec = 0; ec < expense_cats.Length; ec++)
        {
            expenseCats.Add(new PurchaseType() { Name = expense_cats[ec], Descr = expense_cats[ec] });
        }

        foreach (PurchaseType pt in expenseCats)
        {
            context.PurchaseTypes.Add(pt);
        }
        #endregion

        #region randon Expenses
        var articles_dummy = new string[] { "Food expense", "Car expense", "Computer item", "Train ticket", "Plain ticket" };
        for (int m = 1; m < DateTime.Now.Month; m++)
        {
            int expenses_count_per_month = new Random(m).Next(5, 15);
            for (int i = 0; i < expenses_count_per_month; i++)
            {
                context.Purchases.Add(new Purchase()
                {
                    Provider = providers[new Random(i).Next(0, providers.Count - 1)],
                    Article = articles_dummy[new Random(i).Next(0, articles_dummy.Length - 1)],
                    Price = new Random(i).Next(10, 100),
                    VAT = 18,
                    PurchaseType = expenseCats[new Random(i).Next(0, expenseCats.Count - 1)],
                    TimeStamp = new DateTime (DateTime.Now.Year, m, new Random(i).Next(1, 28))
                });
            }
        }
        #endregion

        // add data into context and save to db
        try
        {
            context.SaveChanges();
        }
        catch (DbEntityValidationException dbEx) //debug errors
        {
            foreach (var validationErrors in dbEx.EntityValidationErrors)
            {
                foreach (var validationError in validationErrors.ValidationErrors)
                {
                    Console.Write("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                }
            }
        }
    }
}