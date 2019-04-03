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
                    total += product.Price * product.Quantity;
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

        public bool ShippingAddressIsTheSameAsBillingAddress { get; set; }

        public bool SaveInformation { get; set; }

        [Required]
        public int PaymentOption { get; set; }

        [Required]
        public string NameOnCard { get; set; }

        [Required]
        public string CreditCardNumber { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Required]
        public string Cvv { get; set; }

        public IEnumerable<PaymentOptionViewModel> PaymentOptions { get; set; }
    }
}
