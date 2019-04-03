using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rentx.Web.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public List<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        public string Email { get; set; }

        [Required]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string PostCode { get; set; }

        [Required]
        public string PaymentOption { get; set; }

        [Required]
        public string NameOnCard { get; set; }

        [Required]
        public string CreditCardNumber { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Required]
        public string Cvv { get; set; }

        public bool Finished { get; set; }
    }
}
