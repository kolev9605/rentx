﻿using System.Collections.Generic;

namespace Rentx.Web.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int AvailableQuantity { get; set; }

        public string Image { get; set; }

        public List<ShoppingCartDetails> ShoppingCartDetails { get; set; } = new List<ShoppingCartDetails>();
    }
}
