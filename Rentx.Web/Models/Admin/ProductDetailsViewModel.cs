namespace Rentx.Web.Models.Admin
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int AvailableQuantity { get; set; }
    }
}
