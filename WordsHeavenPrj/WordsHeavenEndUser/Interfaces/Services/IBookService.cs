using WordsHeavenEndUser.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace WordsHeavenEndUser.Interfaces.Services
{
    public interface IBookService
    {

        Task<IEnumerable<Book>> GetAllBooksAsync();

        Task<IEnumerable<Book>> SearchBooksAsync(string query);

        Task<Book> GetBookByIdAsync(int id);


    }
}
