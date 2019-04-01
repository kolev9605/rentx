using Microsoft.AspNetCore.Mvc;
using Rentx.Web.Models;

namespace Rentx.Web.Extensions
{
    public static class MvcResponseExtensions
    {
        public static void SetMessage(this MessageViewModel messageViewModel, Controller controller)
        {
            if (messageViewModel.HasError)
            {
                controller.TempData["Error"] = messageViewModel.Message;
            }
            else if(messageViewModel.HasSuccess)
            {
                controller.TempData["Success"] = messageViewModel.Message;
            }
        }
    }
}
