using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SeguridadDoctores.Models;
using SeguridadDoctores.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SeguridadDoctores.Controllers
{
    public class IdentityController : Controller
    {
        RepositoryDoctores repo;

        public IdentityController(RepositoryDoctores repo)
        {
            this.repo = repo;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(String username, int password)
        {
            Doctor doctor = this.repo.ExisteDoctor(username, password);
            if (doctor == null)
            {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
                return View();
            }
            else
            {
                ClaimsIdentity identidad =
                    new ClaimsIdentity(
                        CookieAuthenticationDefaults.AuthenticationScheme
                        , ClaimTypes.Name, ClaimTypes.Role);
                identidad.AddClaim(new Claim(ClaimTypes.NameIdentifier
                    , doctor.IdDoctor.ToString()));
                identidad.AddClaim(new Claim(ClaimTypes.Name
                    , doctor.Apellido));
                ClaimsPrincipal principal = new ClaimsPrincipal(identidad);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme
                    , principal, new AuthenticationProperties
                    {
                         IsPersistent = true,
                          ExpiresUtc =DateTime.Now.AddMinutes(15)
                    });
                String action = TempData["action"].ToString();
                String controller = TempData["controller"].ToString();
                return RedirectToAction(action, controller);
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync
                (CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
