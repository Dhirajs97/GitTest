using Microsoft.AspNetCore.Mvc;
using WordsHeavenEndUser.Interfaces.Services;
using System.Threading.Tasks;
using WordsHeavenEndUser.Models;

namespace WordsHeavenEndUser.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService booksService)
        {
            _bookService = booksService;
        }

        // Search Book
        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            var books = await _bookService.SearchBooksAsync(query);
            return View(books);
        }

        // Get Book by Id
        public async Task<IActionResult> BookDetails(int id) { 
            var book = await _bookService.GetBookByIdAsync(id); 
            if (book == null) { 
                return NotFound(); 
            } 
            return View(book); 
        }
    }
}
