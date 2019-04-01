using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rentx.Web.Models.Admin
{
    public class AdminDetailsViewModel : MessageViewModel
    {
        public IEnumerable<ProductViewModel> ProductViewModels { get; set; }

        public IEnumerable<CategoryViewModel> CategoryViewModels { get; set; }
    }
}
