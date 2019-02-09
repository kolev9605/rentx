using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Rentx.Web.Data.Entities
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public IEnumerable<ShoppingCartDetails> ShoppingCartDetails { get; set; }

        public string UserId { get; set; }

        public IdentityUser User { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
