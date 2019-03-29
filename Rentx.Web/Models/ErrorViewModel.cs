using System;

namespace Rentx.Web.Models
{
    public class ErrorViewModel
    {
        public string Message { get; set; }

        public bool HasError => !string.IsNullOrEmpty(this.Message);
    }
}