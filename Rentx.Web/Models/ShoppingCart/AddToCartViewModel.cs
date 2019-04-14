using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rentx.Web.Models.ShoppingCart
{
    public class AddToCartViewModel : MessageViewModel
    {
        public int ProductId { get; set; }

        public string UserId { get; set; }

        public int Quantity { get; set; }
    }
}
