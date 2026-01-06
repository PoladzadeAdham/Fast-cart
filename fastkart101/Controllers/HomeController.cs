using System.Diagnostics;
using fastkart101.Models;
using Microsoft.AspNetCore.Mvc;

namespace fastkart101.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        
    }
}
