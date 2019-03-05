using System.ComponentModel.DataAnnotations;

namespace Rentx.Web.Models.Admin
{
    public class ProductViewModel
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
    }
}