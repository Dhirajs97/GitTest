using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WordsHeavenEndUser.Interfaces.Services;
using WordsHeavenEndUser.Models;
using Microsoft.Extensions.Logging;
using ErrorViewModel = WordsHeavenEndUser.Models.ErrorViewModel;
using System.Collections;

namespace WordsHeavenEndUser.Areas.EndUser.Controllers
{
    [Area("EndUser")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookService _bookService;

        public HomeController(ILogger<HomeController> logger, IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }

        [HttpGet("/EndUser")]
        public IActionResult RedirectToHomeIndex()
        {
            return RedirectToAction("Index", "Home", new { area = "EndUser" });
        }

        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooksAsync(); 
            _logger.LogInformation($"Fetched {books.Count()} books from the database."); // Log each book and its category for debugging
            
            foreach (var book in books) { 
                _logger.LogInformation($"Book: {book.Title}, Author: {book.Author}, Category: {book.Category?.Name}"); 
            } 
            return View(books); 
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("test-books")]
        public async Task<IActionResult> TestBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books); // Returns JSON with the book list
        }


        public async Task<IActionResult> Dashboard() { 
            var books = await _bookService.GetAllBooksAsync(); 
            return View(books); 
        }

    }
}
