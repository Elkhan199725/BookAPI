using System.ComponentModel.DataAnnotations;

namespace BookAPI.Models;

public class Book
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public double Price { get; set; }
    public string ImageUrl { get; set; }
}