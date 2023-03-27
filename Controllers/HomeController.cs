using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WordClient.Models;
using Newtonsoft.Json;
namespace WordClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> AllWords()
        {
            HttpClient client = new();
            client.BaseAddress = new Uri("https://localhost:7146/");
            var response = await client.GetAsync("api/Words/All");
            try
            {
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();
                List<string> words = JsonConvert.DeserializeObject<List<string>>(content);
                return View(words);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return View();
        }

        public async Task<IActionResult> RandomWord()
        {
            HttpClient client = new();
            client.BaseAddress = new Uri("https://localhost:7146/");
            var response = await client.GetAsync("api/Words/Random");
            try
            {
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();
                return View(content as object);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return View();
        }

        public async Task<IActionResult> OrderedWords()
        {
            HttpClient client = new();
            client.BaseAddress = new Uri("https://localhost:7146/");
            var response = await client.GetAsync("api/Words/Ordered");
            try
            {
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();
                List<string> words = JsonConvert.DeserializeObject<List<string>>(content);
                return View(words);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return View();
        }
    }
}