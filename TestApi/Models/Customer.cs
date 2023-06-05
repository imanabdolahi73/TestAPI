using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestApi.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    [Required(ErrorMessage = "Enter First Name")]
    [StringLength(50)]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "Enter Last Name")]
    [StringLength(50)]
    public string? LastName { get; set; }

    [EmailAddress(ErrorMessage = "Email Format not valid")]
    public string? Email { get; set; }

    [Phone]
    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? ZipCode { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
