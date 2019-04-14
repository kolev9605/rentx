using Microsoft.AspNetCore.Mvc.Rendering;
using Rentx.Web.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Rentx.Web.Models.Order
{
    public class OrderDetailsViewModel : MessageViewModel
    {
        public IList<OrderProductItemViewModel> Products { get; set; }

        public decimal Total
        {
            get
            {
                decimal total = 0;
                foreach (var product in this.Products)
                {
                    total += product.Price.GetValueOrDefault() * product.Quantity;
                }

                return total;
            }
        }

        public decimal TotalRent
        {
            get
            {
                decimal total = 0;
                foreach (var product in this.Products)
                {
                    total += product.RentPrice.GetValueOrDefault() * product.Quantity;
                }

                return total;
            }
        }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Username { get; set; }

        public string Email { get; set; }
        
        public string Address1 { get; set; }

        public string Address2 { get; set; }
        
        public string Country { get; set; }
        
        public string PostCode { get; set; }
        
        public PaymentType PaymentOption { get; set; }
        
        public string NameOnCard { get; set; }
        
        public string CreditCardNumber { get; set; }
        
        public int ExpirationMonth { get; set; }

        public int ExpirationYear { get; set; }
        
        public string Cvv { get; set; }

        public IEnumerable<SelectListItem> PaymentOptions { get; set; }
    }
}
