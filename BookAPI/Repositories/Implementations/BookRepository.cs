using BookAPI.Data;
using BookAPI.Models;
using BookAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Repositories.Implementations;

public class BookRepository : GenericRepository<Book>, IBookRepository
{
    public BookRepository(BookDbContext context) : base(context)
    {
    }
}
