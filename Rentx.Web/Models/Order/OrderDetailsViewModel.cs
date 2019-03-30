using System.Collections.Generic;
using System.Linq;

namespace Rentx.Web.Models.Order
{
    public class OrderDetailsViewModel : ErrorViewModel
    {
        public IEnumerable<OrderProductItemViewModel> Products { get; set; }

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

    }
}
