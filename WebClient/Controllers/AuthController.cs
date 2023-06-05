using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using System.Security.Claims;
using System.Text;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class AuthController : Controller
    {
        private IHttpClientFactory _httpClientFactory;
        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory= httpClientFactory;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View("Login");
            }

            var _client = _httpClientFactory.CreateClient("TestApiClient");
            var jsonBody = JsonConvert.SerializeObject(login);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var response = _client.PostAsync("/Api/Auth", content).Result;
            if (response.IsSuccessStatusCode)
            {
                var token = response.Content.ReadFromJsonAsync<TokenModel>().Result;

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier , login.UserName),
                    new Claim(ClaimTypes.Name , login.UserName),
                    new Claim("AccessToken" , token.Token),

                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    AllowRefresh = true,
                };

                HttpContext.SignInAsync(principal, properties);
                return Redirect("/Home");
            }
            else
            {
                ModelState.AddModelError("Username", "User Not Valid");
                return View("Login");
            }
        }
    }
}
