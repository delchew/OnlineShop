namespace OnlineShop.DB.Services.Interfaces
{
    public interface IFavoriteProductsRepository : IProductsGroupRepository
    {
        int GetAllFavoritesCount();
    }
}
