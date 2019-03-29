using Rentx.Web.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rentx.Web.Models.Catalog
{
    public class CatalogViewModel : ErrorViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public IEnumerable<CatalogProductViewModel> CatalogProducts { get; set; }
    }
}
