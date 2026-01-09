using System.Diagnostics;
using System.Threading.Tasks;
using fastkart101.Context;
using fastkart101.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fastkart101.Controllers
{
    public class HomeController(AppDbContext context) : Controller
    {
      

        public async Task<IActionResult> Index()
        {
            var products = await context.Products.ToListAsync();

            return View(products);
        }

        
    }
}
