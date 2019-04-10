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
        public PaymentType PaymentOption { get; set; }

        [Required]
        public string NameOnCard { get; set; }

        [Required]
        public string CreditCardNumber { get; set; }
        
        public int ExpirationMonth { get; set; }

        public int ExpirationYear { get; set; }

        [Required]
        public string Cvv { get; set; }

        public IEnumerable<PaymentOptionViewModel> PaymentOptions { get; set; }
    }
}
