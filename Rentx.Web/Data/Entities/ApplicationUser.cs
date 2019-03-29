using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rentx.Web.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public int? ShoppingCartId { get; set; }

        public ShoppingCart ShoppingCart { get; set; }

        public IEnumerable<Order> Orders { get; set; }
    }
}