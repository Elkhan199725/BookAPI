using BookMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

public class HomeController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _apiBaseUrl = "https://localhost:7291/api";

    public HomeController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index()
    {
        List<BookReadDto> list = new List<BookReadDto>();
        var client = _httpClientFactory.CreateClient();
        client.BaseAddress = new Uri(_apiBaseUrl);

        var responseMessage = await client.GetAsync("/Books/GetAllBooks");

        if (responseMessage.IsSuccessStatusCode)
        {
            var data = await responseMessage.Content.ReadAsStringAsync();
            list = JsonConvert.DeserializeObject<List<BookReadDto>>(data);
        }

        return View(list);
    }
}
