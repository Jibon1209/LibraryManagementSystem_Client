using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using LibraryManagementSystem_Client.Models;
using Newtonsoft.Json;
using System.Text;
using LibraryManagementSystem_Client.Helper;

namespace LibraryManagementSystem_Client.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _client;

        public AuthController(HttpClient httpClient)
        {
            _client = httpClient;
            _client.BaseAddress = new Uri("https://localhost:7118/api/");
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
                
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            string result = JsonConvert.SerializeObject(login);
            StringContent content = new StringContent(result, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync("User/SignIn", content);

            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadFromJsonAsync<ServiceResponse<Login>>(); 

                if (user.IsSuccess)
                {
                    var loginUser = user.Data;

                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, loginUser.UserName),
                        new Claim("userName", "user"),
                    };

                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

    }
}
