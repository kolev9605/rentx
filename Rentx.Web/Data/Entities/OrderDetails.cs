namespace Rentx.Web.Data.Entities
{
    public class OrderDetails
    {
        public int Id { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
