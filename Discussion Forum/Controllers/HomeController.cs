using Data.Entities;
using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Facades;
using Service.Home;
using System.Diagnostics;

namespace Discussion_Forum.Controllers
{
    public class HomeController : Controller
    {
        #region Dependency Injection
        private readonly GlobalFacade globalFacade;
        private readonly IHome homeService;
        public HomeController(GlobalFacade globalFacade, IHome homeService)
        {
            this.globalFacade = globalFacade;
            this.homeService = homeService;
        }
        #endregion

        public IActionResult Index() => View(homeService.DiscussionList());
    }
}
