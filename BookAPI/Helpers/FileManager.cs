using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BookAPI.Helpers;

public class FileManager
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileManager(IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
    }

    public async Task<string> SaveFileAsync(IFormFile file, string folderName)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File is empty or null.", nameof(file));

        if (!file.ContentType.StartsWith("image/"))
            throw new ArgumentException("File is not an image.", nameof(file));

        string fileName = Path.GetFileName(file.FileName);
        fileName = fileName.Length > 64 ? fileName.Substring(fileName.Length - 64, 64) : fileName;
        fileName = Guid.NewGuid().ToString("N") + "_" + fileName; // Ensures uniqueness

        string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, folderName);
        string fullPath = Path.Combine(folderPath, fileName);

        Directory.CreateDirectory(folderPath); // Ensure the directory exists

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return GenerateFullUrl(fileName, folderName);
    }

    private string GenerateFullUrl(string fileName, string folderName)
    {
        var httpRequest = _httpContextAccessor.HttpContext.Request;
        var host = httpRequest.Host.Value;
        var scheme = httpRequest.Scheme;

        return $"{scheme}://{host}/{folderName}/{fileName}";
    }

    public void DeleteFile(string folderName, string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            return;

        string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, folderName, fileName);
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
    }
}