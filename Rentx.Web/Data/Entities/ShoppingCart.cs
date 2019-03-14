using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rentx.Web.Data.Entities
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public List<ShoppingCartDetails> ShoppingCartDetails { get; set; }

        
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
