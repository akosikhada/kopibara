using Kopibara.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Kopibara.Data;

namespace Kopibara.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Login()
        {
            return View();
        }

        public IActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate()
        {

            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action("GoogleResponse")
                });
            return new EmptyResult();
        }

        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (result?.Principal != null)
            {

                var claims = result.Principal.Claims;

                var googleId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var profileImage = result.Principal.FindFirstValue("urn:google:picture");
                var fullName = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

                if (googleId == null || email == null)
                    return RedirectToAction("Login", "Auth");

                try
                {

                    var existingUser = await _context.Accounts.FirstOrDefaultAsync(u => u.GoogleId == googleId);

                    if (existingUser == null)
                    {

                        var newUser = new Accounts
                        {
                            GoogleId = googleId,
                            Email = email,
                            FullName = fullName,
                            ProfileImageUrl = profileImage,
                            LastLogin = DateTime.Now
                        };

                        _context.Accounts.Add(newUser);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
    
                        existingUser.LastLogin = DateTime.Now;
                        existingUser.FullName = fullName; 
                        existingUser.ProfileImageUrl = profileImage; 

                        _context.Accounts.Update(existingUser);
                        await _context.SaveChangesAsync();
                    }

                    if (profileImage != null)
                    {
                        Response.Cookies.Append("UserProfileImage", profileImage, new CookieOptions
                        {
                            Expires = DateTimeOffset.Now.AddDays(30),
                            IsEssential = true,
                            Secure = true,
                            HttpOnly = true,
                            SameSite = SameSiteMode.Lax
                        });

                        Response.Cookies.Append("UserName", fullName, new CookieOptions
                        {
                            Expires = DateTimeOffset.Now.AddDays(30),
                            IsEssential = true,
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Lax
                        });

                        Response.Cookies.Append("UserEmail", email, new CookieOptions
                        {
                            Expires = DateTimeOffset.Now.AddDays(30),
                            IsEssential = true,
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Lax
                        });
                    }

                    if (profileImage != null)
                        HttpContext.Session.SetString("UserProfileImage", profileImage);

                    if (fullName != null)
                        HttpContext.Session.SetString("UserName", fullName);

                    if (email != null)
                        HttpContext.Session.SetString("UserEmail", email);

                    return RedirectToAction("Home", "Home");
                }
                catch (Exception ex)
                {

                    return View("Error");
                }
            }

            return RedirectToAction("Login", "Auth");
        }

        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Clear();

            foreach (var cookie in Request.Cookies)
            {
                Response.Cookies.Delete(cookie.Key);
            }

            return RedirectToAction("Home", "Home");
        }
    }
}

