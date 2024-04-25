namespace BookMVC.ViewModels;

public class CreateBookDto
{
    public string Title { get; set; }
    public string Author { get; set; }
    public double Price { get; set; }
    public IFormFile? ImageFile { get; set; } // File upload for the book image
}