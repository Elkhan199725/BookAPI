using BookMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

public class HomeController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _apiBaseUrl = "https://localhost:7291/api";

    public HomeController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    private HttpClient GetClient()
    {
        var client = _httpClientFactory.CreateClient();
        client.BaseAddress = new Uri(_apiBaseUrl);
        return client;
    }

    public async Task<IActionResult> Index()
    {
        var client = GetClient();
        var responseMessage = await client.GetAsync(_apiBaseUrl+"/Books/GetAllBooks");
        if (responseMessage.IsSuccessStatusCode)
        {
            var data = await responseMessage.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<BookReadDto>>(data);
            return View(list);
        }
        return View(new List<BookReadDto>());
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new CreateBookDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookDto book)
    {
        var client = GetClient();
        using var content = new MultipartFormDataContent();

        if (book.ImageFile != null)
        {
            var streamContent = new StreamContent(book.ImageFile.OpenReadStream());
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(book.ImageFile.ContentType);
            content.Add(streamContent, "ImageFile", book.ImageFile.FileName); // Ensure this name matches the API's expectation
        }

        // Ensure these names match the API's expectation
        content.Add(new StringContent(book.Title), "Title");
        content.Add(new StringContent(book.Author), "Author");
        content.Add(new StringContent(book.Price.ToString()), "Price");

        var response = await client.PostAsync($"{_apiBaseUrl}/Books/CreateBook", content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        else
        {
            // Read the response content to get more details about the error
            var errorContent = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"API call failed: {errorContent}");
            return View(book);
        }
    }


    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var client = GetClient();
        var response = await client.GetAsync($"{_apiBaseUrl}/Books/GetBookById/{id}");
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var book = JsonConvert.DeserializeObject<UpdateBookDto>(data);
            return View(book);
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateBookDto book)
    {
        var client = GetClient();
        using var content = new MultipartFormDataContent();

        // Add the image file to the content
        if (book.ImageFile != null)
        {
            var streamContent = new StreamContent(book.ImageFile.OpenReadStream());
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(book.ImageFile.ContentType);
            content.Add(streamContent, "ImageFile", book.ImageFile.FileName);
        }

        // Add other properties as string values
        content.Add(new StringContent(book.Title), "Title");
        content.Add(new StringContent(book.Author), "Author");
        content.Add(new StringContent(book.Price.ToString()), "Price");
        content.Add(new StringContent(book.Id.ToString()), "Id");

        // Call the API
        var response = await client.PutAsync($"{_apiBaseUrl}/Books/UpdateBook/{book.Id}", content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "API call failed. Please check the API and try again.");
            var errorResponse = await response.Content.ReadAsStringAsync();
            // Log the errorResponse or use it to display an error message
            return View(book);
        }
    }



}
