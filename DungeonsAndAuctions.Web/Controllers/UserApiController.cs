using D_A.Web.Models.Api;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace D_A.Web.Controllers
{
    public class UsersApiController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UsersApiController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("DungeonsAndAuctionsApi");
            var response = await client.GetAsync("api/Users");
            if (!response.IsSuccessStatusCode) return View(new List<UserApiDto>());

            var json = await response.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<List<UserApiDto>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(users);
        }

        public async Task<IActionResult> Details(int id)
        {
            var client = _httpClientFactory.CreateClient("DungeonsAndAuctionsApi");
            var response = await client.GetAsync($"api/Users/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<UserApiDto>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(user);
        }
    }
}