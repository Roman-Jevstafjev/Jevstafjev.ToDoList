using Jevstafjev.ToDoList.Models;
using JorgeSerrano.Json;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Jevstafjev.ToDoList.Controllers
{
    public class UsersController : Controller
    {
        private IHttpClientFactory _httpClientFactory;

        public UsersController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var response = await _httpClientFactory
                .CreateClient("TodoAPI")
                .PostAsJsonAsync("users", model, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", response.ReasonPhrase!);
                return View(model);
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var createUserResult = JsonSerializer.Deserialize<CreateUserResult>(jsonString, new JsonSerializerOptions
            {
                PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy()
            });

            return View("Message", $"Token: {createUserResult!.AccessToken}");
        }
    }
}
