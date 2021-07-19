using System.ComponentModel;

namespace OnlineShop.DB.Models
{
    public enum OrderStatus
    {
        [Description("Ожидает подтверждения")]
        AwaitingConfirm,

        [Description("Подтверждён")]
        Confirmed,

        [Description("Доставляется")]
        OnDelivery,

        [Description("Доставлен")]
        Delivered,

        [Description("Отменён")]
        Canceled
    }
}
