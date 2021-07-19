using System.ComponentModel;

namespace OnlineShop.DB.Models
{
    public enum DeliveryType
    {
        [Description("Самовывоз")]
        PickUp,

        [Description("Курьерская доставка")]
        Courier
    }
}
