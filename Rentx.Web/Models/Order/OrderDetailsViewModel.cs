using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Rentx.Web.Models.Order
{
    public class OrderDetailsViewModel : MessageViewModel
    {
        public IList<OrderProductItemViewModel> Products { get; set; }

        public decimal TotalOrderPrice
        {
            get
            {
                decimal subtotal = 0;
                foreach (var product in this.Products)
                {
                    subtotal += product.Price * product.Quantity;
                }

                return subtotal;
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
        public string PaymentOption { get; set; }

        [Required]
        public string NameOnCard { get; set; }

        [Required]
        public string CreditCardNumber { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Required]
        public string Cvv { get; set; }
    }
}
