using System.Collections.Generic;
using System.Threading.Tasks;
using WordsHeavenEndUser.Models;
using WordsHeavenEndUser.Interfaces.Repositories;
using WordsHeavenEndUser.Interfaces.Services;
using WordsHeavenEndUser.Repositories;

namespace WordsHeavenEndUser.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }


        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllBooksAsync();
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(string query)
        {
            return await _bookRepository.SearchBooksAsync(query);

        }

        public async Task<Book> GetBookByIdAsync(int id) { 
            return await _bookRepository.GetBookByIdAsync(id); 
        }


    }
}
