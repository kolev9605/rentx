using Microsoft.AspNetCore.Mvc;

namespace Rentx.Web.Controllers
{
    public class CatalogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
