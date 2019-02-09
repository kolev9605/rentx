﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Rentx.Web.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public IEnumerable<OrderDetails> OrderDetails { get; set; }

        public string UserId { get; set; }

        public IdentityUser User { get; set; }
    }
}
