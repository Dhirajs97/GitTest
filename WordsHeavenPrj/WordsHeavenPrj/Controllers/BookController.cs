using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WordsHeavenPrj.Models;
using WordsHeavenPrj.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WordsHeavenPrj.Data;

namespace WordsHeavenPrj.Controllers
{

    [Route("books")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookService bookService, ApplicationDbContext context, ILogger<BookController> logger)
        {
            _bookService = bookService;
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("AddBook")]
        public IActionResult AddBook()
        {
            var categories = _context.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [Route("AddBook")]
        public async Task<IActionResult> AddBook([FromForm] BookDto bookDto)
        {
            try
            {
                var book = await _bookService.AddBookAsync(bookDto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding book");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("EditBook/{id}")]
        public async Task<IActionResult> EditBook(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                _logger.LogWarning("Book with ID {BookId} not found", id);
                return NotFound();
            }

            var categories = _context.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", book.CategoryId);

            var bookDto = new BookDto
            {
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                CategoryId = book.CategoryId
            };

            return View(bookDto);
        }

        [HttpPost]
        [Route("EditBook/{id}")]
        public async Task<IActionResult> EditBook(int id, [FromForm] BookDto bookDto)
        {
            try
            {
                var book = await _bookService.UpdateBookAsync(id, bookDto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error editing book with ID {BookId}", id);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooksAsync();
            return View(books);
        }

        [HttpGet]
        [Route("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                _logger.LogWarning("Book with ID {BookId} not found", id);
                return NotFound();
            }
            return View(book);
        }


        [HttpGet][Route("delete/{id}")] 
        public async Task<IActionResult> Delete(int id) { 
            var book = await _bookService.GetBookByIdAsync(id); 
            if (book == null) { 
                _logger.LogWarning("Book with ID {BookId} not found", id); 
                return NotFound(); 
            } 
            return View(book); 
        }


        [HttpPost, ActionName("Delete")]
        [Route("delete/{id}")] 
        public async Task<IActionResult> DeleteConfirmed(int id) { 
            try { 
                var success = await _bookService.DeleteBookAsync(id); 
                if (!success) { 
                    return NotFound(); 
                } return RedirectToAction("Index"); 
            } catch (Exception ex) { 
                _logger.LogError(ex, "Error deleting book with ID {BookId}", id); 
                return BadRequest(ex.Message); 
            } 
        }

    }
}
