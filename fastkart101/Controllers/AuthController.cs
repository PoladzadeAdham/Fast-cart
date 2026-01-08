using System.Threading.Tasks;
using fastkart101.Abstractions;
using fastkart101.Models;
using fastkart101.ViewModel.Login;
using fastkart101.ViewModel.Register;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace fastkart101.Controllers
{
    public class AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService) : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }


        public async Task<IActionResult> SendEmail()
        {
            await emailService.SendEmailAsync("edhempoladzade2@gmail.com", "Something", "<h1 style: color='red'> Salam necesiz</h1>");
            return Ok();
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
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("AppUser", error.Description);
                }

                return View(vm);

            }

            await SendConfirmtaionEmailAsync(user);

            return RedirectToAction(nameof(Login));
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

            if (existUser is null)
            {
                ModelState.AddModelError("", "Email or password is wrong");
                return View(vm);
            }

            var isPassword = await userManager.CheckPasswordAsync(existUser, vm.Password);

            if (!isPassword)
            {
                ModelState.AddModelError("", "Email or password is wrong");
                return View(vm);
            }

            if (!existUser.EmailConfirmed)
            {
                ModelState.AddModelError("","Please confirm your email");
                await SendConfirmtaionEmailAsync(existUser);
                return View(vm);

            }


            await signInManager.SignInAsync(existUser, vm.IsRemember);

            return RedirectToAction("Index", "Home");


        }

 



        public async Task SendConfirmtaionEmailAsync(AppUser user)
        {
            string token = await userManager.GenerateEmailConfirmationTokenAsync(user);

            var url = Url.Action("ConfirmEmail", "Auth", new {token = token, userId = user.Id}, Request.Scheme);

            string emailBody = @$"<!DOCTYPE html>
<html lang=""en"">
  <head>
    <meta charset=""UTF-8"" />
    <title>Confirm Your Email</title>
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
  </head>
  <body style=""margin:0; padding:0; background-color:#f4f6f8; font-family:Arial, Helvetica, sans-serif;"">
    <table width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""background-color:#f4f6f8; padding:20px 0;"">
      <tr>
        <td align=""center"">
          <table width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""max-width:600px; background-color:#ffffff; border-radius:6px; overflow:hidden;"">
            
            <!-- Header -->
            <tr>
              <td style=""padding:24px; text-align:center; background-color:#0d6efd; color:#ffffff;"">
                <h1 style=""margin:0; font-size:24px;"">Confirm Your Email</h1>
              </td>
            </tr>

            <!-- Body -->
            <tr>
              <td style=""padding:32px; color:#333333; font-size:16px; line-height:1.5;"">
                <p style=""margin-top:0;"">Hi {user.UserName},</p>

                <p>
                  Thanks for signing up! Please confirm your email address by clicking the button below.
                </p>

                <p style=""text-align:center; margin:32px 0;"">
                  <a href=""{url}""
                     style=""background-color:#0d6efd; color:#ffffff; text-decoration:none; padding:14px 24px; border-radius:4px; display:inline-block; font-weight:bold;"">
                    Confirm Email
                  </a>
                </p>

                <p>
                  If you didn’t create an account, you can safely ignore this email.
                </p>

                <p style=""margin-bottom:0;"">
                  Thanks,<br />
                  <strong>Pronia</strong>
                </p>
              </td>
            </tr>

            <!-- Footer -->
            <tr>
              <td style=""padding:20px; text-align:center; font-size:12px; color:#888888; background-color:#f9fafb;"">
                <p style=""margin:0;"">
                  © 2026 Fastkart. All rights reserved.
                </p>
              </td>
            </tr>

          </table>
        </td>
      </tr>
    </table>
  </body>
</html>
";

            await emailService.SendEmailAsync(user.Email!,"Confirm your email", emailBody!);

        }

        public async Task<IActionResult> ConfirmEmail(string token, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if(user == null)
            {
                return NotFound();
            }

            var result = await userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            await signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");


        }


    }
}
