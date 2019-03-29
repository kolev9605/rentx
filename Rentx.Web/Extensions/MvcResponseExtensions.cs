using Microsoft.AspNetCore.Mvc;
using Rentx.Web.Models;

namespace Rentx.Web.Extensions
{
    public static class MvcResponseExtensions
    {
        public static void SetErrorMessage(this ErrorViewModel errorViewModel, Controller controller)
        {
            if (errorViewModel.HasError)
            {
                controller.TempData["Error"] = errorViewModel.Message;
            }
        }
    }
}
