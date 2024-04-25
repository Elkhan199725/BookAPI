namespace BookMVC.ViewModels;

public class UpdateBookDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public double Price { get; set; }
    public IFormFile? ImageFile { get; set; }
}
