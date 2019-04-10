namespace Rentx.Web.Models.Enums
{
    public enum PaymentType
    {
        DebitCard = 0,
        CashОnDelivery = 1
    }

    public class PaymentTypeHelper
    {
        public static string GetName(PaymentType paymentType)
        {
            switch (paymentType)
            {
                case PaymentType.DebitCard:
                    return "Дебитна карта";
                case PaymentType.CashОnDelivery:
                    return "Наложен платеж";
                default:
                    return "";
            }
        }
    }
}
