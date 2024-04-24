using Microsoft.AspNetCore.Mvc;
using BookAPI.Helpers;
using BookAPI.Repositories.Interfaces;
using BookAPI.DTOs;
using BookAPI.Models;

namespace BookAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _repository;
        private readonly FileManager _fileManager;

        public BooksController(IBookRepository repository, FileManager fileManager)
        {
            _repository = repository;
            _fileManager = fileManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _repository.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _repository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound($"No book found with ID {id}.");
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromForm] CreateBookDto createBookDto)
        {
            if (createBookDto.ImageFile != null)
            {
                string filePath = await _fileManager.SaveFileAsync(createBookDto.ImageFile, "uploads");
                var book = new Book
                {
                    Title = createBookDto.Title,
                    Price = createBookDto.Price,
                    Author = createBookDto.Author,
                    ImageUrl = filePath
                };
                await _repository.AddAsync(book);
                return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
            }
            return BadRequest("Image is required.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromForm] UpdateBookDto updateBookDto)
        {
            var book = await _repository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound($"No book found with ID {id}.");
            }

            book.Title = updateBookDto.Title;
            book.Price = updateBookDto.Price;
            book.Author = updateBookDto.Author;

            if (updateBookDto.ImageFile != null)
            {
                _fileManager.DeleteFile("uploads", book.ImageUrl);
                book.ImageUrl = await _fileManager.SaveFileAsync(updateBookDto.ImageFile, "uploads");
            }

            await _repository.UpdateAsync(book);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _repository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound($"No book found with ID {id}.");
            }
            _fileManager.DeleteFile("uploads", book.ImageUrl);
            await _repository.DeleteAsync(book);
            return NoContent();
        }
    }
}
