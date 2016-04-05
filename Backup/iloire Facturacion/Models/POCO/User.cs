using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

public class User
{
    public int UserID { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Login { get; set; }

    [Required]
    public string Password { get; set; }

    public bool Enabled { get; set; }

    [Required]
    [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Wrong email format")]
    public string Email { get; set; }

}