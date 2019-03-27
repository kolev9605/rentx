using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentx.Web.Common;
using Rentx.Web.Common.Interfaces;
using Rentx.Web.ImageWriter;
using Rentx.Web.ImageWriter.Interfaces;
using Rentx.Web.Models.Admin;
using Rentx.Web.Models.Enums;
using Rentx.Web.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Rentx.Web.Controllers
{
    [Authorize(Roles = RoleConstants.Administrator)]
    public class AdminController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IImageHandler imageHandler;
        private readonly IPathHelper pathHelper;

        public AdminController(
            IProductService productService,
            IImageHandler imageHandler,
            IPathHelper pathHelper,
            ICategoryService categoryService)
        {
            this.productService = productService;
            this.imageHandler = imageHandler;
            this.pathHelper = pathHelper;
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(AdminMenuType type)
        {
            var model = new AdminDetailsViewModel();

            switch (type)
            {
                case AdminMenuType.ProductMenu:
                    {
                        var products = await this.productService.GetAllAsync();
                        model.ProductViewModels = products;
                        break;
                    }
                case AdminMenuType.CategoriesMenu:
                    {
                        var categories = await this.categoryService.GetAllAsync();
                        model.CategoryViewModels = categories;
                        break;
                    }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var success = await this.productService.DeleteByIdAsync(productId);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            var model = new ProductViewModel();
            var categories = await this.categoryService.GetAllAsync();
            model.Categories = categories
                .Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
                .ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var image = await this.imageHandler.UploadImage(model.Image);
            model.ImageFileName = image;

            await this.productService.AddAsync(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int productId)
        {
            var product = await this.productService.GetByIdAsync(productId);
            var categories = await this.categoryService.GetAllAsync();
            product.Categories = categories
                .Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
                .ToList();
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var image = await this.imageHandler.UploadImage(model.Image);

            model.ImageFileName = image;

            var success = await this.productService.UpdateAsync(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var success = await this.categoryService.DeleteByIdAsync(categoryId);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryViewModel);
            }

            await this.categoryService.AddAsync(categoryViewModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditCategory(int categoryId)
        {
            var category = await this.categoryService.GetByIdAsync(categoryId);
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var success = await this.categoryService.UpdateAsync(model);
            return RedirectToAction("Index");
        }
    }
}