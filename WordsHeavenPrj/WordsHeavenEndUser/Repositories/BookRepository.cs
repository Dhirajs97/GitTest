using Microsoft.EntityFrameworkCore;
using WordsHeavenEndUser.Data;
using WordsHeavenEndUser.Interfaces.Repositories;
using WordsHeavenEndUser.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WordsHeavenEndUser.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get All Books
        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books.Include(b => b.Category).ToListAsync();
        }

        // Search Book
        public async Task<IEnumerable<Book>> SearchBooksAsync(string query) { 
            return await _context.Books.Where(b => b.Title.Contains(query) || b.Author.Contains(query)).ToListAsync(); 
        
        }

        public async Task<Book> GetBookByIdAsync(int id) { 
            return await _context.Books.FindAsync(id); 
        }

    }
}
