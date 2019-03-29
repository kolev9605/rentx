using System.Collections.Generic;
using System.Linq;

namespace Rentx.Web.Models.ShoppingCart
{
    public class ShoppingCartViewModel : ErrorViewModel
    {
        public IList<ShoppingCartItemViewModel> ShoppingCartItems { get; set; }

        public decimal TotalAmount => ShoppingCartItems.Sum(sci => sci.Subtotal);
    }
}
