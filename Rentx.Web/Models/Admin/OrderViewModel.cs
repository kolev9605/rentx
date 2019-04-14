using Rentx.Web.Models.Enums;
using Rentx.Web.Models.Order;
using System.Collections.Generic;

namespace Rentx.Web.Models.Admin
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public IList<OrderProductItemViewModel> Products { get; set; }

        public decimal Total { get; set; }

        public decimal TotalRent { get; set; }
        
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

        public int? ExpirationMonth { get; set; }

        public int? ExpirationYear { get; set; }

        public string Cvv { get; set; }

        public bool Finished { get; set; }
    }
}
