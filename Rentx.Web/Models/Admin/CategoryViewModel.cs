using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rentx.Web.Models.Admin
{
    public class CategoryViewModel : ErrorViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
