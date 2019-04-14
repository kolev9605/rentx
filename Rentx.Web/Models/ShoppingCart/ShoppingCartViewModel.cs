using System.Collections.Generic;
using System.Linq;

namespace Rentx.Web.Models.ShoppingCart
{
    public class ShoppingCartViewModel : MessageViewModel
    {
        public int ShoppingCartId { get; set; }

        public IList<ShoppingCartItemViewModel> ShoppingCartItems { get; set; }

        public decimal TotalAmount => ShoppingCartItems.Sum(sci => sci.Subtotal);
        public decimal TotalRentAmount => ShoppingCartItems.Sum(sci => sci.SubtotalRent);
    }
}
