﻿namespace Rentx.Web.Models.ShoppingCart
{
    public class ShoppingCartItemViewModel
    {
        public int Id { get; set; }

        public string Image { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal Subtotal => this.Price * this.Quantity;
    }
}
