namespace Rentx.Web.Models
{
    public class MessageViewModel
    {
        public string Message { get; set; }

        public bool HasError { get; protected set; }

        public bool HasSuccess { get; protected set; }

        public void SetError(string message)
        {
            this.Message = message;
            this.HasError = true;
        }

        public void SetSuccess(string message)
        {
            this.Message = message;
            this.HasSuccess = true;
        }
    }
}
