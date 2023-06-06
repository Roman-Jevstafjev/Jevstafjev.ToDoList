using Jevstafjev.ToDoList.Models;
using JorgeSerrano.Json;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Jevstafjev.ToDoList.Services;

namespace Jevstafjev.ToDoList.Controllers
{
    public class MainController : Controller
    {
        private IHttpClientFactory _httpClientFactory;
        private AccessTokenService _accessTokenService;

        public MainController(IHttpClientFactory httpClientFactory, AccessTokenService accessTokenService)
        {
            _httpClientFactory = httpClientFactory;
            _accessTokenService = accessTokenService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClientFactory
                .CreateClient("TodoAPI")
                .GetAsync("tasks");

            if (!response.IsSuccessStatusCode)
            {
                return View("Message", "Error. Maybe invalid token.");
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var tasks = JsonSerializer.Deserialize<List<Models.Task>>(jsonString, new JsonSerializerOptions
            {
                PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy()
            });

            return View(tasks);
        }

        public IActionResult AddTask()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTask(AddTaskBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _httpClientFactory.CreateClient("TodoAPI")
                .PostAsJsonAsync("tasks", model, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy()
                });

            return Redirect(nameof(Index));
        }

        public IActionResult DeleteTask(string id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTask(DeleteTaskBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _httpClientFactory.CreateClient("TodoAPI")
                .DeleteAsync($"tasks/{model.Id}");

            return Redirect(nameof(Index));
        }

        public IActionResult PerformTask(string id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PerformTask(PerformTaskBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _httpClientFactory.CreateClient("TodoAPI")
                .PutAsJsonAsync($"tasks/{model.Id}", new
                {
                    marked_as_done = true
                });

            return Redirect(nameof(Index));
        }

        public IActionResult SetAccessToken()
        {
            var model = new SetAccessTokenBindingModel
            {
                AccessToken = _accessTokenService.GetToken()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult SetAccessToken(SetAccessTokenBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _accessTokenService.SetToken(model.AccessToken);
            return View(model);
        }
    }
}