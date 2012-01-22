
# ASP.NET MVC3 Invoicing Application

 * This is a **sample invoicing application** used for **ASP.NET MVC3 training purposes**. 
 * This is a good code base to learn ASP.NET MVC3, since it covers many areas and development techniques.
 * I am using it as a personal invoicing tool for my freelancing activity, you may use it as well for this or any other purpose (it is very **easy to customize** to fit your needs) 
 * The solution has been created with **Visual Studio 2010 Express**, but you can open it with other versions of VS 2010.
 * Unit tests have been written with [NUnit](http://www.nunit.org/). [Moq](http://code.google.com/p/moq/) has been used for object mocking.

## Online Demo

[Check the online demo here](http://www.vitaminasdev.com/invoicing)

## How to best use this code:

 * This application shows **how to create an ASP.NET MVC application from scratch.** and how to use some of the cool features of ASP.NET MVC3 like:
   * **Code First**
   * **Entity Framework** and **LINQ**
   * **Razor** view engine 
   * **Custom Membership Provider** pointing to your own database users table.
   * **Partial views** and **partial actions** (with independent OutputCache for high concurrency page rendering) 
   * **Html Helpers**
   * **Data Annotation** validation
   * **AJAX** partial rendering
   * Custom **T4 templates** for customized scaffolding
   * **NUnit** for unit testing and **Moq for object mocking**.
 * Every major development on this invoicing app has been tagged (0.1, 0.2, etc...)
 * You can download each tag (starting with 0.1), check progress and move to the next when you understood everything that has been done.
 * Follow the change log (tag history) and enjoy!

## Installation

 * Download the code and open with Visual Studio 2010 Express or above.
 * NuGet packages are included. If you have any problem please follow [this instructions](http://stackoverflow.com/questions/6876732/how-do-i-get-nuget-to-install-update-all-the-packages-in-the-packages-config)
 * By default the project is using SQL Server Compact database. You can easily change it to use SQL Server Express or above by modifying web.config file (connectionStrings section).
 * By using Code First and EF 4.1 the database will be recreated when you first run the project.

### Altering connectionStrings section 

Based on convention, EF will look for a connection strign named as the DBContext (in this case "InvoiceDB"), and will use it, so feel free to set the data provider you want:

     <!-- 
         By default (convention over configuration, the connection string with the same name as your DBContext will be used 
         You can select then wherever you will use SQL CE, SQL Serer Express Edition, etc, here. 
     -->
     <add name="InvoiceDB" connectionString="Data Source=|DataDirectory|InvoiceDB.sdf" providerName="System.Data.SqlServerCe.4.0" />
     <!--
     <add name="InvoiceDB" connectionString="Data Source=.\SQLEXPRESS; Integrated Security=True; MultipleActiveResultSets=True" providerName="System.Data.SqlClient" />
     <add name="InvoiceDB" connectionString="metadata=res://*;provider=System.Data.SqlClient;provider connection string=&quot;Data Source=mssql2005a.active-ns.com;Initial Catalog=xxxxxxxxxx.org;user id=xxxxxxxxxxxx;password=xxxxxxxxxxx;MultipleActiveResultSets=True&quot;" providerName="System.Data.EntityClient" />
     -->

## Screenshots

### Home

![Home](https://github.com/iloire/ASP.NET-MVC-ACME-Invoicing--App/raw/master/screenshots/home01.png)

### Invoice Details

![Invoice Details](https://github.com/iloire/ASP.NET-MVC-ACME-Invoicing--App/raw/master/screenshots/detail_invoice01.png)

### Invoice (printed output)

![Printed Invoice](https://github.com/iloire/ASP.NET-MVC-ACME-Invoicing--App/raw/master/screenshots/printed_invoice01.png)

### Invoice list

![Invoice list](https://github.com/iloire/ASP.NET-MVC-ACME-Invoicing--App/raw/master/screenshots/list_invoice01.png)

### Customer list

![Customer list](https://github.com/iloire/ASP.NET-MVC-ACME-Invoicing--App/raw/master/screenshots/list_customer01.png)

### Project unit tests

![Unit testing](https://github.com/iloire/ASP.NET-MVC-ACME-Invoicing--App/raw/master/screenshots/unit_testing01.png)


## TAGS (change log):

**0.18**

 * Config tablesorter to handle datetime
 * Improved reports
 * CSS Styles
 * Other general improvements and fixes


**0.17**

 * Bug fixing in proposals.
 * Config tablesorter to handle decimals.
 * Added ProposalDetails field.
 * Other minor improvements.

**0.16**

 * Added "proposals". Proposals are invoices that don't have an invoice number yet. They become an invoice once the client approves it.
 * CSS improvements: added primary style to "create" buttons. Added nowrap html attribute to edit buttons td.
 * Added http://harvesthq.github.com/chosen/ jquery plugin to some extra dropdown menus in editor screens.
 * Added some help messages in invoices and proposals.

**0.15**

 * Added missing DLLS in Elmah and Moq packages (will work in restoring packages from config file instead of commiting binaries)
 * Added Chosen js plugin for dropdowns (cool stuff!) http://harvesthq.github.com/chosen/

**0.14**

 * Added globalization support in printing invoice. Added spanish resource file.
 * Adding and edition customer now works with a modal popup windows using AJAX.
 * Added InvoiceNumber field to Invoice so it can be defined and edited by the user.
 * Added ContactPerson and Notes fields to Customer. Changes in customer edition modal popup.
 * Database initializer configurable from web.config
 * Added PurchaseType (aka expense type) entity and table, in order to categorize expenses.
 * Added Profit and Loss report.
 * Partial view for redering money (negative balance in red)
 * CSS style for print media for printing invoices and expenses.
 * Many usability and formatting improvements. Changes in menu for easier access to items.
 * Default pagination size now is stored in web.config. 

**0.13**

 * Support for **SQL Server Compact Edition** (by default). You can choose to other database provider in web.config.
 * New field on invoice: "AdvancePaymentTax"
 * New Reports controller for **summary and reports** (used in home page so far). Yearly summary, quarter summary, etc.
 * Better dummy data generation when database is re-created.
 * **Search field** for Providers.
 * Date search in Invoices
 * **Paging** on expenses (purchases)
 * Extra **EditorTemplates and DisplayTemplates views**.
 * Aggregated data in invoice list for a customer.
 * Lots of style and usability improvements. Overall improvements to prepare the application for a 1.0 version.
 * Search by date. Auto select Q1, Q2, Q3, Q4 dates interval in order to search by date.
 * Pie chart using **Google Chart** calls.
 * More **coverage in unit testing**. Added **Moq reference for object mocking**.

**0.12**

 * Added pagination functionality using https://github.com/martijnboland/MvcPaging library to some entities (pending the other ones)
 * Added NUnit testing project as example of how to include it.
 * Make "new", "edit" and delete invoice details work in new modal window, using AJAX and the twitter bootstrap css library.
 * Layout changes
 * Basic print view for invoices, with a custom layout for printing.
 * Added error handling by including Elmah from NuGet repository (try it by accessing http://localhost:xxxx/elmah.axd)
 * Added a custom functionality to HandleErrorAttribute so it logs to Elmah.

**0.11**

 * Added search functionality in Customer list.
 * Style and css modifications.
 * New screenshots

**0.10**
 
 * Added DueDate field to invoice.
 * Some bug fixing.
 * Alphabetical and "by name" search in Customer.
 * Support for ajax in alphabetical search.

**0.9**

 * Added jquery localization file (http://plugins.jquery.com/node/8/release) in order tu support spanish format currency validation
 * Added range validation to qty, so you need to enter at least "1"

**0.8**

 * Improved the way the invoice and invoicedetail entity play together in the application for better UX
   * Making the invoice details views always display with their invoice parent object on the left.
 * Added partial views for encapsulation (ex: edit and add)
 * Added footer with aggregate data to invoice details lists.

**0.7**

 * Added DateTime editor template to be able to edit datetime fields accross de application
 * Added jQuery UI calendar functionality to datetime editor fields.
 * Set PAID / UNPAID display as text with style instead of checkbox

**0.6**
 
 * Added security so only authenticated users can access the app. To do so:
   * Added custom Membership Provider class (InvoicingMemembershipProvider)
   * Implement ValidateUser method on (InvoicingMemembershipProvider)
   * Change web.config to add InvoicingMemembershipProvider as default provider
   * Added [Authorize] attribute to controllers
   * Added User entity, added Users  table to DBConext and dummy data generation in initializer.
   * Fixing css style in Account views.

**0.5**
 
 * Added PartialActions to Invoice and Purchase controllers to return unpaid invoices and recently purchased items.
 * Added RenderAction methods to Home.Index, in order to display data from the recently created partial actions.
 * Removed unnecessary columns from list views.

**0.4**

 * Added System.ComponentModel.DataAnnotations reference to validate model.
 * Added validation attributes to POCO entities, in order to create basic client and server side validation
 * Added DisplayName attribute (System.ComponentModel) to fields in order to rename fields in CRUD operations.

**0.3**

 * Added T4 CodeTemplates so we can control the scaffolding generation process. Modifications on those templates so they render Boostrap css style.
 * All CRUD view regenerated using the new T4 CodeTemplates
 * Added CompanyNumber field to Customer and Provider tables.
 * Added tablesorter javascript plugin for index pages.

**0.2**

 * Added Twitter Boostrap CSS library to the project.
 * Changed customer create view to apply boostrap styling on it (styles of other CRUD pages haven't been changed at this time, btw, we will do that next by touching the scaffolding generation)

**0.1**

 * Starting app.
 * Creating model by using POCO entities.
 * Use code first for creating database
 * Scaffolding basic CRUD operations


## The Author

Ivan Loire, [ASP.NET MVC Freelance](http://www.aspnetfreelance.com)

I make clients happy for a living while developing software, so contact me if you need custom modifications on this app, or help in other ASP.NET MVC project.
I'm currently located in Spain.

Contact: [www.iloire.com](http://www.iloire.com/)

## LICENSE

Copyright (c) 2011 Iván Loire Mallén -  www.iloire.com

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.

