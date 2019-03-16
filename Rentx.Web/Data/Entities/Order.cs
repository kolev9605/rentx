using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Rentx.Web.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public List<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
