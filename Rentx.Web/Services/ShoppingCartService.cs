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
    /// <summary>
    /// Service responsible for database operations on shopping carts
    /// </summary>
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ApplicationDbContext dbContext;

        public ShoppingCartService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Gets shopping cart by user id
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>Model of the shopping cart</returns>
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
                    RentPrice = scd.Product.RentPrice,
                    Description = scd.Product.Description.TrimDescription(),
                    Id = scd.Id,
                    Quantity = scd.Quantity,
                    ProductId = scd.ProductId
                })
                .ToList();

            var model = new ShoppingCartViewModel();
            model.ShoppingCartItems = shoppingCartItems;
            model.ShoppingCartId = userCart.Id;

            return model;
        }

        /// <summary>
        /// Adds shopping cart into the database
        /// </summary>
        /// <param name="model">Model which is used to create Shopping Cart entity</param>
        /// <returns>Message model with error message in case of error or success message.</returns>
        public async Task<MessageViewModel> AddAsync(AddToCartViewModel model)
        {
            var messageViewModel = new MessageViewModel();
            var userCart = await this.GetOrCreateUserCartAsync(model.UserId);

            var productToAdd = this.dbContext.Products.FirstOrDefault(p => p.Id == model.ProductId);
            if (productToAdd == null)
            {
                messageViewModel.SetError($"Продукт с ID {model.ProductId} не съществува.");
                return messageViewModel;
            }

            if (userCart.ShoppingCartDetails.Any(scd => scd.ProductId == productToAdd.Id))
            {
                var productDetails = userCart.ShoppingCartDetails.FirstOrDefault(scd => scd.ProductId == productToAdd.Id);
                productDetails.Quantity++;

                if (productToAdd.AvailableQuantity < productDetails.Quantity)
                {
                    messageViewModel.SetError($"Недостатъчно количесто, в момента наличното е: {productToAdd.AvailableQuantity}.");
                    return messageViewModel;
                }

                this.dbContext.ShoppingCartDetails.Update(productDetails);
                await this.dbContext.SaveChangesAsync();
            }
            else
            {
                if (productToAdd.AvailableQuantity < model.Quantity)
                {
                    messageViewModel.SetError($"Недостатъчно количесто, в момента наличното е: {productToAdd.AvailableQuantity}.");
                    return messageViewModel;
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

            messageViewModel.SetSuccess("Продуктът успешно беше добавен към вашата количка.");
            return messageViewModel;
        }

        /// <summary>
        /// Removes shopping cart details from the shopping cart
        /// </summary>
        /// <param name="shoppingCartDetailsId">The id of the shopping cart</param>
        /// <returns>Message model with error message in case of error or success message.</returns>
        public async Task<MessageViewModel> RemoveAsync(int shoppingCartDetailsId)
        {
            var messageViewModel = new MessageViewModel();

            var shoppingCartDetails = await this.dbContext.ShoppingCartDetails.FirstOrDefaultAsync(scd => scd.Id == shoppingCartDetailsId);
            if (shoppingCartDetails == null)
            {
                messageViewModel.SetError($"Количка с ID {shoppingCartDetailsId} не съществува.");
                return messageViewModel;
            }

            this.dbContext.ShoppingCartDetails.Remove(shoppingCartDetails);
            await this.dbContext.SaveChangesAsync();

            messageViewModel.SetSuccess("Продуктът успешно е премахнат от количката.");
            return messageViewModel;
        }

        /// <summary>
        /// Updates the shopping cart from the given model
        /// </summary>
        /// <param name="shoppingCartViewModel">Model which is used to update the shopping cart</param>
        /// <returns>Message model with error message in case of error or success message.</returns>
        public async Task<MessageViewModel> UpdateAsync(ShoppingCartViewModel shoppingCartViewModel)
        {
            var messageViewModel = new MessageViewModel();

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
                    messageViewModel.SetError($"Предмет с ID {shoppingCartItem.Id} съществува.");
                    return messageViewModel;
                }

                if (item.Quantity > shoppingCartItem.Product.AvailableQuantity)
                {
                    messageViewModel.SetError($"Недостатъчно количесто, в момента наличното е: {shoppingCartItem.Product.AvailableQuantity}.");
                    return messageViewModel;
                }

                shoppingCartItem.Quantity = item.Quantity;
                this.dbContext.ShoppingCartDetails.Update(shoppingCartItem);
            }

            await this.dbContext.SaveChangesAsync();

            messageViewModel.SetSuccess("Количката беше обновена.");
            return messageViewModel;
        }

        /// <summary>
        /// Clears the shopping cart by user id
        /// </summary>
        /// <param name="userId">The Id of the user</param>
        /// <returns>Message model with error message in case of error or success message.</returns>
        public async Task<MessageViewModel> ClearShoppingCart(string userId)
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
                return null;
            }

            this.dbContext.ShoppingCartDetails.RemoveRange(userCart.ShoppingCartDetails);
            await this.dbContext.SaveChangesAsync();

            return null;
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