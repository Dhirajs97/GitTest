using System.Collections.Generic;
using System.Threading.Tasks;
using WordsHeavenPrj.Models;
using Microsoft.AspNetCore.Http;

namespace WordsHeavenPrj.Services
{
    public interface IBookService
    {
        Task<Book> AddBookAsync(BookDto bookDto);
        Task<Book> GetBookByIdAsync(int id);
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> UpdateBookAsync(int id, BookDto bookDto);
        Task<bool> DeleteBookAsync(int id);
    }

}
