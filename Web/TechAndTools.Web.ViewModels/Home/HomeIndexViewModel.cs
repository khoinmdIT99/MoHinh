namespace TechAndTools.Web.ViewModels.Home
{
    using Products;

    using X.PagedList;

    public class HomeIndexViewModel
    {
        public IPagedList<ProductIndexViewModel> ProductsViewModels { get; set; }

        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }

        public int? CategoryId { get; set; }
    }
}
