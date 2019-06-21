using Rentx.Web.Models.Admin;
using Rentx.Web.Models.Enums;
using System.Collections.Generic;

namespace Rentx.Web.Models.Catalog
{
    public class CatalogViewModel : MessageViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public IEnumerable<CatalogProductViewModel> CatalogProducts { get; set; }

        public string SearchTerm { get; set; }

        public OrderType CurrentOrder { get; set; }
    }
}
