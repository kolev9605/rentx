namespace Rentx.Web.Data.Entities
{
    public class ShoppingCartDetails
    {
        public int Id { get; set; }

        public Product Product { get; set; }

        public int Qantity { get; set; }
    }
}
