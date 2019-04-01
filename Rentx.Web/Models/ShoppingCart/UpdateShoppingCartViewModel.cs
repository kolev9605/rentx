using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rentx.Web.Models.ShoppingCart
{
    public class UpdateShoppingCartViewModel : MessageViewModel
    {
        public IEnumerable<ShoppingCartItemViewModel> ShoppingCartItems { get; set; }
    }
}
