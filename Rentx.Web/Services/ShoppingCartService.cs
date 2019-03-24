﻿using Microsoft.EntityFrameworkCore;
using Rentx.Web.Data;
using Rentx.Web.Data.Entities;
using Rentx.Web.Models.ShoppingCart;
using Rentx.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rentx.Web.Common;

namespace Rentx.Web.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ApplicationDbContext dbContext;

        public ShoppingCartService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ShoppingCartViewModel> GetShoppingCartAsync(string userId)
        {
            ShoppingCart userCart = await this.dbContext.ShoppingCarts
                .Select(sc => new ShoppingCart
                {
                    ShoppingCartDetails = sc.ShoppingCartDetails.Select(scd => new ShoppingCartDetails
                    {
                        Id = scd.Id,
                        Product = scd.Product,
                        ProductId = scd.ProductId,
                        Quantity = scd.Quantity
                    }).ToList(),
                    Id = sc.Id,
                    TotalAmount = sc.TotalAmount,
                    UserId = sc.UserId
                })
                .FirstOrDefaultAsync(sc => sc.UserId == userId);

            if (userCart == null)
            {
                return null;
                //throw new ArgumentException($"Shopping cart for user with id {userId} not found.");
            }

            var shoppingCartItems = userCart.ShoppingCartDetails
                .Select(scd => new ShoppingCartItemViewModel()
                {
                    Title = scd.Product.Title,
                    Image = scd.Product.Image,
                    Price = scd.Product.Price,
                    Description = scd.Product.Description.TrimDescription(),
                    Id = scd.Id,
                    Quantity = scd.Quantity
                })
                .ToList();

            var model = new ShoppingCartViewModel();
            model.ShoppingCartItems = shoppingCartItems;

            return model;
        }

        public async Task AddAsync(AddToCartViewModel model)
        {
            var userCart = await this.GetOrCreateUserCartAsync(model.UserId);
            var productToAdd = this.dbContext.Products.FirstOrDefault(p => p.Id == model.ProductId);
            if (productToAdd == null)
            {
                throw new ArgumentNullException($"Product With id {model.ProductId} could not be found.");
            }

            if (userCart.ShoppingCartDetails.Any(scd => scd.ProductId == productToAdd.Id))
            {
                var productDetails = userCart.ShoppingCartDetails.FirstOrDefault(scd => scd.ProductId == productToAdd.Id);
                productDetails.Quantity++;

                if (productToAdd.AvailableQuantity < productDetails.Quantity)
                {
                    throw new ArgumentException($"Not enough available quantity. Currently in stock: {productToAdd.AvailableQuantity}.");
                }

                this.dbContext.ShoppingCartDetails.Update(productDetails);
                await this.dbContext.SaveChangesAsync();
            }
            else
            {
                if (productToAdd.AvailableQuantity < model.Quantity)
                {
                    throw new ArgumentException($"Not enough available quantity. Currently in stock: {productToAdd.AvailableQuantity}.");
                }

                ShoppingCartDetails shoppingCartDetails = new ShoppingCartDetails
                {
                    ProductId = productToAdd.Id,
                    Quantity = model.Quantity,
                    ShoppingCartId = userCart.Id
                };

                await this.dbContext.ShoppingCartDetails.AddAsync(shoppingCartDetails);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task RemoveAsync(int shoppingCartDetailsId)
        {
            var shoppingCartDetails = await this.dbContext.ShoppingCartDetails.FirstOrDefaultAsync(scd => scd.Id == shoppingCartDetailsId);
            if (shoppingCartDetails == null)
            {
                throw new ArgumentNullException($"Shopping cart item with id {shoppingCartDetailsId} was not found");
            }

            this.dbContext.ShoppingCartDetails.Remove(shoppingCartDetails);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(IEnumerable<ShoppingCartItemViewModel> shoppingCartItems)
        {
            foreach (var item in shoppingCartItems)
            {
                var shoppingCartItem = await this.dbContext.ShoppingCartDetails
                    .Select(scd => new ShoppingCartDetails
                    {
                        Id = scd.Id,
                        Product = scd.Product,
                        ProductId = scd.ProductId,
                        Quantity = scd.Quantity,
                        ShoppingCartId = scd.ShoppingCartId
                    })
                    .FirstOrDefaultAsync(scd => scd.Id == item.Id);

                if (shoppingCartItem == null)
                {
                    throw new ArgumentNullException($"Shopping cart details with Id {shoppingCartItem.Id} was not found.");
                }

                if (item.Quantity > shoppingCartItem.Product.AvailableQuantity)
                {
                    throw new ArgumentException("Not enough available quantity in stock.");
                }

                shoppingCartItem.Quantity = item.Quantity;
                this.dbContext.ShoppingCartDetails.Update(shoppingCartItem);
            }

            await this.dbContext.SaveChangesAsync();
        }

        private async Task<ShoppingCart> GetOrCreateUserCartAsync(string userId)
        {
            ShoppingCart userCart = this.dbContext.ShoppingCarts
                    .Select(sc => new ShoppingCart
                    {
                        ShoppingCartDetails = sc.ShoppingCartDetails.Select(scd => new ShoppingCartDetails
                        {
                            Id = scd.Id,
                            Product = scd.Product,
                            ProductId = scd.ProductId,
                            Quantity = scd.Quantity,
                            ShoppingCartId = scd.ShoppingCartId
                        }).ToList(),
                        Id = sc.Id,
                        TotalAmount = sc.TotalAmount,
                        UserId = sc.UserId,
                    })
                    .FirstOrDefault(sc => sc.UserId == userId);

            if (userCart == null)
            {
                ShoppingCart shoppingCart = new ShoppingCart()
                {
                    UserId = userId
                };

                this.dbContext.ShoppingCarts.Add(shoppingCart);
                await dbContext.SaveChangesAsync();

                return shoppingCart;
            }

            return userCart;
        }
    }
}