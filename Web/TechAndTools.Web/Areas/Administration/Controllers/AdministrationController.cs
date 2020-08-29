namespace TechAndTools.Web.Areas.Administration.Controllers
{
    using Commons.Constants;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Web.Controllers;

    [Area(GlobalConstants.AdministrationArea)]
    [Authorize(Roles = GlobalConstants.AdminRole)]
    public class AdministrationController : BaseController
    { }
}