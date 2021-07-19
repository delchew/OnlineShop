using System;

namespace OnlineShopWebApp.ViewModels
{
    public class PageViewModel
    {
        public int PageNumber { get; private set; }

        public int TotalPages { get; private set; }

        public int ProductsCount { get; private set; }

        public bool HasPreviousPage => PageNumber > 1;

        public bool HasNextPage => PageNumber < TotalPages;

        public PageViewModel(int productsCount, int pageNumber, int productsOnPageCount)
        {
            PageNumber = pageNumber;
            ProductsCount = productsCount;
            TotalPages = (int)Math.Ceiling(ProductsCount / (double)productsOnPageCount); //Наименьшее целое число, которое >= заданному дробному
        }
    }
}
