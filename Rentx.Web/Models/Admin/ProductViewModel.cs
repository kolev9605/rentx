using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rentx.Web.Models.Admin
{
    public class ProductViewModel : MessageViewModel
    {
        public int Id { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int AvailableQuantity { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public string ImageFileName { get; set; }

        public List<SelectListItem> Categories { get; set; }

        public string Category { get; set; }
    }
}