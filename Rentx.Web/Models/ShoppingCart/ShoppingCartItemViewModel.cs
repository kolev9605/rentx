namespace Rentx.Web.Models.ShoppingCart
{
    public class ShoppingCartItemViewModel : MessageViewModel
    {
        public int Id { get; set; }

        public string Image { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal? Price { get; set; }

        public decimal? RentPrice { get; set; }

        public int Quantity { get; set; }

        public int ProductId { get; set; }

        public decimal Subtotal => this.Price.GetValueOrDefault() * this.Quantity;

        public decimal SubtotalRent => this.RentPrice.GetValueOrDefault() * this.Quantity;
    }
}
