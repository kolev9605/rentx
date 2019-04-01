using Rentx.Web.Models.Admin;
using System.Collections.Generic;

namespace Rentx.Web.Models.Catalog
{
    public class CatalogViewModel : MessageViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public IEnumerable<CatalogProductViewModel> CatalogProducts { get; set; }
    }
}
