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
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Username { get; set; }

        public string Email { get; set; }
        
        public string Address1 { get; set; }

        public string Address2 { get; set; }
        
        public string Country { get; set; }
        
        public string PostCode { get; set; }
        
        public string PaymentOption { get; set; }
        
        public string NameOnCard { get; set; }
        
        public string CreditCardNumber { get; set; }
        
        public int? ExpirationMonth { get; set; }

        public int? ExpirationYear { get; set; }
        
        public string Cvv { get; set; }

        public bool Finished { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal TotalRentAmount { get; set; }
    }
}
