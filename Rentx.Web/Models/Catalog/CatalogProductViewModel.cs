namespace Rentx.Web.Models.Catalog
{
    public class CatalogProductViewModel : MessageViewModel
    {
        public int Id { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public decimal? Price { get; set; }

        public decimal? RentPrice { get; set; }

        public bool IsRentable => this.RentPrice.HasValue;
    }
}
