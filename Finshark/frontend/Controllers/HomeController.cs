using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace frontend.Controllers
{
    public class HomeController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7265/finshark");
        private readonly HttpClient _client;

        public HomeController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Stock> stockList = new List<Stock>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Stock/GetAll").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                stockList = JsonConvert.DeserializeObject<List<Stock>>(data);
            }
            return View(stockList);
        }
    }
}
