namespace OnlineShop.DB
{
    public static class Constants
    {
        public const string UserRoleName = "User";

        public const string AdminRoleName = "Admin";

        public const int ProductsOnPageCount = 20; //число продуктов одновременно отображаемых на главной странице

        public const string RubleSign = "\u20bd"; //знак валюты рубля

        public const string EmailRegex = @"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$";

        public const string UnsignedUserId = "unsignedUser";
    }
}
