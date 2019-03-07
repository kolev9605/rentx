using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rentx.Web.Models.Catalog
{
    public class CatalogProductsViewModel
    {
        public IEnumerable<CatalogProductViewModel> ProductItems { get; set; }
    }
}
