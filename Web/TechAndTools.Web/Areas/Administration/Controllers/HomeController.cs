namespace TechAndTools.Web.Areas.Administration.Controllers
{
    using Services.Contracts;
    using Services.Mapping;
    using ViewModels.Administration;
    using ViewModels.Orders;

    using Microsoft.AspNetCore.Mvc;
    
    using System.Linq;

    public class HomeController : AdministrationController
    {
        private readonly IOrderService orderService;

        public HomeController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                UnprocessedОrdersViewModels = this.orderService.GetUnprocessedOrders()
                    .To<UnprocessedОrderViewModel>()
                    .OrderByDescending(x => x.OrderDate)
                    .ToList(),
                ProcessedОrdersViewModels = this.orderService.GetProcessedOrders()
                    .To<ProcessedОrderViewModel>()
                    .OrderByDescending(x => x.ShippingDate)
                    .ToList()
            };
            
            return View(viewModel);
        }
    }
}
