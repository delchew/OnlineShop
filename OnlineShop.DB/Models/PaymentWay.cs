using System.ComponentModel;

namespace OnlineShop.DB.Models
{
    public enum PaymentWay
    {
        [Description("Наличные")]
        Cash,

        [Description("Банковская карта")]
        BankCard
    }
}