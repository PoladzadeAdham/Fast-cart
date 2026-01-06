using System.Threading.Tasks;
using fastkart101.Models;
using fastkart101.ViewModel.Login;
using fastkart101.ViewModel.Register;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace fastkart101.Controllers
{
    public class AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm vm)
        {

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AppUser user = new()
            {
                Fullname = vm.FullName,
                Email = vm.EmailAddress,
                UserName = vm.Username
            };

            var result = await userManager.CreateAsync(user, vm.Password);

            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("AppUser", error.Description);
                }

                return View(vm);

            }

            return Ok("OKay");
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var existUser = await userManager.FindByEmailAsync(vm.EmailAddress);

            if(existUser is null)
            {
                ModelState.AddModelError("", "Email or password is wrong");
                return View(vm);
            }

            var isPassword = await userManager.CheckPasswordAsync(existUser,vm.Password);

            if (!isPassword)
            {
                ModelState.AddModelError("", "Email or password is wrong");
                return View(vm);
            }

            await signInManager.SignInAsync(existUser,false);

            return RedirectToAction("Index", "Home");


        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }


    }
}
