using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Depotakip.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Depotakip.Controllers
{
    public class AdminController : Controller
        
    {

        private readonly MarketDbContext _context;

       

        public AdminController(MarketDbContext context)
        {
            _context = context;
        }


        [AllowAnonymous]    //Even if there is an ID block on all pages, we can release this page with this Allow
        [HttpGet]
       public IActionResult Login()
        {
            return View();
        }


        [AllowAnonymous]
        //Post
        [HttpPost]

        public async Task<IActionResult> Login(Admin admin)
        {
            var check = _context.Admin.FirstOrDefault(x => x.KullaniciAdi == admin.KullaniciAdi && x.Sifre == admin.Sifre);
            if (check != null)
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name,admin.KullaniciAdi) };
                var userIdentity = new ClaimsIdentity(claims, "/");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Urun");

            }

            
            else
            {
                ViewBag.ErrorMessage = "Geçersiz kullanıcı adı veya şifre.";
                return View();
            }

        }
        [HttpGet]

        public async Task< IActionResult> LogOut()

        {
           
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
       
            return RedirectToAction("Login","Admin");
        }

    }
}
