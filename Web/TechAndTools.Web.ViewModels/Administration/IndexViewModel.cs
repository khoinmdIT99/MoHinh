namespace TechAndTools.Web.ViewModels.Administration
{
    using Orders;

    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IList<ProcessedОrderViewModel> ProcessedОrdersViewModels { get; set; } = new List<ProcessedОrderViewModel>();

        public IList<UnprocessedОrderViewModel> UnprocessedОrdersViewModels { get; set; } = new List<UnprocessedОrderViewModel>();
    }
}
