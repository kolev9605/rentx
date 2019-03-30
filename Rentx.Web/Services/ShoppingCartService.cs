using Microsoft.EntityFrameworkCore;
using Rentx.Web.Data;
using Rentx.Web.Data.Entities;
using Rentx.Web.Models.ShoppingCart;
using Rentx.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rentx.Web.Common;
using Rentx.Web.Common.Exceptions;
using Rentx.Web.Models;

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
            model.ShoppingCartId = userCart.Id;

            return model;
        }

        public async Task<ErrorViewModel> AddAsync(AddToCartViewModel model)
        {
            var errorViewModel = new ErrorViewModel();
            var userCart = await this.GetOrCreateUserCartAsync(model.UserId);
            var productToAdd = this.dbContext.Products.FirstOrDefault(p => p.Id == model.ProductId);
            if (productToAdd == null)
            {
                errorViewModel.Message = $"Product With id {model.ProductId} could not be found.";
                //throw new RentxValidationException($"Product With id {model.ProductId} could not be found.");
            }

            if (userCart.ShoppingCartDetails.Any(scd => scd.ProductId == productToAdd.Id))
            {
                var productDetails = userCart.ShoppingCartDetails.FirstOrDefault(scd => scd.ProductId == productToAdd.Id);
                productDetails.Quantity++;

                if (productToAdd.AvailableQuantity < productDetails.Quantity)
                {
                    productDetails.Quantity = productToAdd.AvailableQuantity;
                    errorViewModel.Message = $"Not enough available quantity. Currently in stock: {productToAdd.AvailableQuantity}.";
                    //throw new RentxValidationException($"Not enough available quantity. Currently in stock: {productToAdd.AvailableQuantity}.");
                }

                this.dbContext.ShoppingCartDetails.Update(productDetails);
                await this.dbContext.SaveChangesAsync();
            }
            else
            {
                if (productToAdd.AvailableQuantity < model.Quantity)
                {
                    errorViewModel.Message = $"Not enough available quantity. Currently in stock: {productToAdd.AvailableQuantity}.";
                    //throw new RentxValidationException($"Not enough available quantity. Currently in stock: {productToAdd.AvailableQuantity}.");
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

            return errorViewModel;
        }

        public async Task<ErrorViewModel> RemoveAsync(int shoppingCartDetailsId)
        {
            var errorViewModel = new ErrorViewModel();

            var shoppingCartDetails = await this.dbContext.ShoppingCartDetails.FirstOrDefaultAsync(scd => scd.Id == shoppingCartDetailsId);
            if (shoppingCartDetails == null)
            {
                errorViewModel.Message = $"Shopping cart item with id {shoppingCartDetailsId} was not found";
                //throw new RentxValidationException($"Shopping cart item with id {shoppingCartDetailsId} was not found");
            }

            this.dbContext.ShoppingCartDetails.Remove(shoppingCartDetails);
            await this.dbContext.SaveChangesAsync();

            return errorViewModel;
        }

        public async Task<ErrorViewModel> UpdateAsync(ShoppingCartViewModel shoppingCartViewModel)
        {
            var errorViewModel = new ErrorViewModel();

            foreach (var item in shoppingCartViewModel.ShoppingCartItems)
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
                    errorViewModel.Message = $"Shopping cart details with Id {shoppingCartItem.Id} was not found.";
                    //throw new RentxValidationException($"Shopping cart details with Id {shoppingCartItem.Id} was not found.");
                }

                if (item.Quantity > shoppingCartItem.Product.AvailableQuantity)
                {
                    errorViewModel.Message = "Not enough available quantity in stock.";
                    //throw new RentxValidationException("Not enough available quantity in stock.");
                }

                shoppingCartItem.Quantity = item.Quantity;
                this.dbContext.ShoppingCartDetails.Update(shoppingCartItem);
            }

            await this.dbContext.SaveChangesAsync();
            return errorViewModel;
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