namespace BookMVC.ViewModels;

public class DeleteBookViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } // Title of the book to provide a clear indication of what's being deleted.
    public string Author { get; set; } // Optional: Include author to make it more specific.

    // You can add more properties if needed, like Author, to display on the confirmation dialog.
}
