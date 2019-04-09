using Microsoft.EntityFrameworkCore;
using Rentx.Web.Common;
using Rentx.Web.Data;
using Rentx.Web.Data.Entities;
using Rentx.Web.Models;
using Rentx.Web.Models.Enums;
using Rentx.Web.Models.Order;
using Rentx.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rentx.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext dbContext;

        public OrderService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<OrderDetailsViewModel>> GetAllOrderDetailsAsync()
        {
            var products = await this.dbContext.Orders
                .Select(o => new OrderDetailsViewModel
                {
                    Address1 = o.Address1,
                    Address2 = o.Address2,
                    Country = o.Country,
                    CreditCardNumber = o.CreditCardNumber,
                    Cvv = o.Cvv,
                    Email = o.Email,
                    ExpirationDate = o.ExpirationDate,
                    FirstName = o.FirstName,
                    LastName = o.LastName,
                    NameOnCard = o.NameOnCard,
                    PaymentOption = (PaymentType)Enum.Parse(typeof(PaymentType), o.PaymentOption),
                    PostCode = o.PostCode,
                    Username = o.Username
                })
                .ToListAsync();

            return products;
        }

        public async Task<OrderDetailsViewModel> GetOrderDetailsAsync(int shoppingCartId)
        {
            OrderDetailsViewModel model = new OrderDetailsViewModel();

            var products = await this.dbContext.ShoppingCartDetails
                .Where(scd => scd.ShoppingCartId == shoppingCartId)
                .Select(p => new OrderProductItemViewModel
                {
                    Title = p.Product.Title,
                    Price = p.Product.Price,
                    RentPrice = p.Product.RentPrice,
                    Quantity = p.Quantity,
                    Description = p.Product.Description.TrimDescription(),
                    ProductId = p.ProductId
                })
                .ToListAsync();

            model.Products = products;

            List<PaymentOptionViewModel> paymentOptions = new List<PaymentOptionViewModel>();
            var options = Enum.GetValues(typeof(PaymentType));
            foreach (var option in options)
            {
                paymentOptions.Add(new PaymentOptionViewModel
                {
                    Name = option.ToString(),
                    Value = (int)option
                });
            }

            model.PaymentOptions = paymentOptions;

            return model;
        }

        public async Task<MessageViewModel> SubmitOrderAsync(OrderDetailsViewModel model, string userId)
        {
            Order order = new Order()
            {
                UserId = userId,
                Address1 = model.Address1,
                Address2 = model.Address2,
                Country = model.Country,
                CreditCardNumber = model.CreditCardNumber,
                Cvv = model.Cvv,
                Email = model.Email,
                ExpirationDate = model.ExpirationDate,
                NameOnCard = model.NameOnCard,
                PaymentOption = (model.PaymentOption).ToString(),
                PostCode = model.PostCode,
                Username = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                TotalAmount = model.Total,
                TotalRentAmount = model.TotalRent
            };

            await this.dbContext.Orders.AddAsync(order);
            await this.dbContext.SaveChangesAsync();

            order.OrderDetails = model.Products
                .Select(p => new OrderDetails
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity,
                    ProductDetailPrice = p.Quantity * p.Price.GetValueOrDefault(0),
                    ProductDetailRentPrice = p.Quantity * p.RentPrice.GetValueOrDefault(0)
                })
                .ToList();
                
            var result = await this.dbContext.SaveChangesAsync();

            var messageModel = new MessageViewModel();
            messageModel.SetSuccess("Поръчката ви е получена успешно");

            return messageModel;
        }
    }
}
