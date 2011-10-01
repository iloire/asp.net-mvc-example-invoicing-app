using System.Data.Entity;
using System.Collections.Generic;

public class EntitiesContextInitializer : DropCreateDatabaseIfModelChanges<DBContext>
{
    protected override void Seed(DBContext context)
    {

        //users
        List<User> users = new List<User>{
            new User { Name="Usuario dummy", Login="user", Password="pass", Email="hello2@user.com", Enabled=true}
        };
        foreach (User u in users)
        {
            context.Users.Add(u);
        }


        //let's add some dummy customer data:
        List<Customer> customers = new List<Customer>
        {
            new Customer {Name="ACME Internation LS", Address="12 Stree NY", CP= "232323", CompanyNumber="3424324342", City="New York", Phone1="223-23232323", Fax="233-333333", Email="hello@hello.com"},
            new Customer {Name="Apple Inc.", Address="1233 Street NY", CP= "232323", CompanyNumber="23232323", City="NN CA", Phone1="343-23232323", Fax="233-333333", Email="apple@hello.com"}
        };
        foreach (Customer c in customers)
        {
            context.Customers.Add(c);
        }

        //let's add a few providers
        int providers_count=20;
        List<Provider> providers = new List<Provider>();
        for(int i=0;i<providers_count;i++){
            providers.Add(new Provider() 
                { 
                    Name="Provider " + i, 
                    Address = "Address for provider" + i, 
                    City = "Zaragoza", 
                    CompanyNumber= "212121212" + i, 
                    CP="50800", 
                    Phone1="2323-2222" + i, 
                    Email="email@provider" + i + ".com" });
        }

        foreach (Provider p in providers)
        {
            context.Providers.Add(p);
        }

        // add data into context and save to db
        context.SaveChanges();
    }
}