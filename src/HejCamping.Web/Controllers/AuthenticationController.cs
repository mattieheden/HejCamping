using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

using HejCamping.Application.Interfaces;
using HejCamping.Application.DTOs;
using HejCamping.Web.Models;


namespace HejCamping.Web.Controllers;

public class AuthenticationController : Controller
{
    // Mocked user data
    private const string MockedUsername = "demo";
    private const string MockedPassword = "pass"; // Note: NEVER hard-code passwords in real applications.

    public IActionResult Login()
    {
        return View();
    }

    

    [HttpPost]
    [ValidateAntiForgeryToken] // This ensures that the form is submitted with a valid anti-forgery token to prevent CSRF attacks.
    public async Task<IActionResult> LoginAsync(LoginViewModel model)
    {
        // Check model validators
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Mocked user verification
        if (model.Username == MockedUsername && model.Password == MockedPassword)
        {
            // Normally, here you'd set up the session/cookie for the authenticated user.
            // Set up the session/cookie for the authenticated user.
            var claims = new[] { new Claim(ClaimTypes.Name, model.Username) };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            //return RedirectToAction("Index", "Home"); // Redirect to a secure area of your application.
            return RedirectToAction("DashBoard", "AdminPortal"); // Redirect to a secure area of your application.
            
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt."); // Generic error message for security reasons.
        return View(model);
    }


    [Authorize]
    public IActionResult Logout()
    {
        return SignOut(
            new AuthenticationProperties
            {
                RedirectUri = Url.Action("Index", "Home")
            },
            CookieAuthenticationDefaults.AuthenticationScheme);
    }
}