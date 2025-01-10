using System.Collections.Generic;
using System.Threading.Tasks;
using WordsHeavenEndUser.Models;


namespace WordsHeavenEndUser.Interfaces.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();

        Task<IEnumerable<Book>> SearchBooksAsync(string query);

        Task<Book> GetBookByIdAsync(int id);

    }
}
