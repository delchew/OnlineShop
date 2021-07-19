using System.Collections.Generic;

namespace OnlineShopWebApp.ViewModels
{
    public class ProductsPageViewModel
    {
        private int _avaliablePageRefsCount
        {
            get
            {
                return Page.TotalPages <= AvaliablePageReferences ? Page.TotalPages : AvaliablePageReferences;
            }
        }

        public int AvaliablePageReferences { get; set; } = 7;

        public IEnumerable<ProductViewModel> Products { get; set; }

        public PageViewModel Page { get; set; }

        public int PaginationStartPageNumber
        {
            get
            {
                if (Page.PageNumber <= _avaliablePageRefsCount / 2 + 1)
                {
                    return 1;
                }
                else if (Page.PageNumber < Page.TotalPages - _avaliablePageRefsCount / 2)
                {
                    return Page.PageNumber - _avaliablePageRefsCount / 2;
                }
                else
                {
                    return Page.TotalPages - _avaliablePageRefsCount + 1;
                }
            }
        }

        public int PaginationEndPageNumber
        {
            get
            {
                if (Page.PageNumber < Page.TotalPages - _avaliablePageRefsCount / 2)
                {
                    return PaginationStartPageNumber + _avaliablePageRefsCount - 1;
                }
                else
                {
                    return Page.TotalPages;
                }
            }
        }
    }
}
