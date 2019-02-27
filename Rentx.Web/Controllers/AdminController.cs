using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentx.Web.Common;

namespace Rentx.Web.Controllers
{
    [Authorize(Roles = RoleConstants.Administrator)]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
