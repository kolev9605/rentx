namespace Rentx.Web.Models.Order
{
    public class OrderProductItemViewModel
    {
        public int ProductId { get; set; }

        public string Title { get; set; }

        public decimal? Price { get; set; }

        public decimal? RentPrice { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }
    }
}
