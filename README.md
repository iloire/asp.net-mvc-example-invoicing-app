## What is this?

This is a sample invoicing application used for ASP.NET MVC3 training purposes.

## How to best use this code:

 * This application is meant to show how to create an ASP.NET MVC application from scratch.
 * This application show how to use Code First, EF, Razor view engine, partial views, partial actions, Html Helpers, Data Annotation validation, etc..
 * Every major development on this invoicing app has been tagged (0.1, 0.2, etc...)
 * You can download each tag (starting with 0.1), check progress and move to the next when you understood everything that has been done.
 * Follow the change log (tag history) and enjoy!

## ScreenShots

![New Customer](https://github.com/iloire/ASP.NET-MVC-ACME-Invoicing--App/raw/master/screenshots/new_customer_bootstrap_style01.png)

![Invoice list](https://github.com/iloire/ASP.NET-MVC-ACME-Invoicing--App/raw/master/screenshots/list_invoice01.png)

![Invoice Details](https://github.com/iloire/ASP.NET-MVC-ACME-Invoicing--App/raw/master/screenshots/detail_invoice01.png)

![Customer list](https://github.com/iloire/ASP.NET-MVC-ACME-Invoicing--App/raw/master/screenshots/list_customer01.png)


## TAGS (change log):

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

## LICENSE

Copyright (c) 2011 Iván Loire Mallén

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

