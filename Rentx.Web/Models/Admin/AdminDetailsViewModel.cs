using System.Collections.Generic;

namespace Rentx.Web.Models.Admin
{
    public class AdminDetailsViewModel : MessageViewModel
    {
        public IEnumerable<ProductViewModel> ProductViewModels { get; set; }

        public IEnumerable<CategoryViewModel> CategoryViewModels { get; set; }

        public IEnumerable<OrderViewModel> OrdersViewModel { get; set; }
    }
}
