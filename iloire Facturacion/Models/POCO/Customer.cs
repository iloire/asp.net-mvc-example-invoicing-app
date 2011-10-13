using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

public class Customer {
 
    public int CustomerID {get;set;}

    [Required (ErrorMessage="Name required")]
    public string Name { get; set; }

    [DisplayName("Company Number")]
    [Required(ErrorMessage = "Field required")]
    public string CompanyNumber { get; set; }

    [Required(ErrorMessage = "Address required")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Zip code required")]
    [DisplayName("Zip Code")]
    public string CP { get; set; }

    [Required(ErrorMessage = "City required")]
    public string City { get; set; }
    
    [Required(ErrorMessage = "Contact person required")]
    [DisplayName("Contact person")]
    public string ContactPerson { get;set; }

    [Required(ErrorMessage = "Telephone required")]
    [DisplayName("Telephone")]
    public string Phone1 { get; set; }

    [DisplayName("Mobile")]
    public string Phone2 { get; set; }

    public string Fax { get; set; }

    [Required(ErrorMessage = "Email required")]
    [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Wrong email format")]
    public string Email { get; set; }

    public string Notes { get;set; }
    
    public virtual ICollection<Invoice> Invoices { get; set; }
}