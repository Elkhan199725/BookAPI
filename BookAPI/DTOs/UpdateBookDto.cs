namespace BookAPI.DTOs;

public class UpdateBookDto
{
    public string Title { get; set; }
    public string Author { get; set; }
    public double Price { get; set; }
    public IFormFile ImageFile { get; set; }
}
