using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WordsHeavenPrj.Data;
using WordsHeavenPrj.Models;

namespace WordsHeavenPrj.Services
{

    public class BookService : IBookService
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ApplicationDbContext _context;

        public BookService(IWebHostEnvironment hostEnvironment, ApplicationDbContext context)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
        }

        public async Task<Book> AddBookAsync(BookDto bookDto)
        {
            if (bookDto.CoverImage != null && bookDto.PdfFile != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "coverimages");
                Directory.CreateDirectory(uploadsFolder);
                string uniqueCoverFileName = Guid.NewGuid().ToString() + "_" + bookDto.CoverImage.FileName;
                string coverFilePath = Path.Combine(uploadsFolder, uniqueCoverFileName);
                using (var fileStream = new FileStream(coverFilePath, FileMode.Create))
                {
                    await bookDto.CoverImage.CopyToAsync(fileStream);
                }

                string pdfFolder = Path.Combine(_hostEnvironment.WebRootPath, "pdffiles");
                Directory.CreateDirectory(pdfFolder);
                string uniquePdfFileName = Guid.NewGuid().ToString() + "_" + bookDto.PdfFile.FileName;
                string pdfFilePath = Path.Combine(pdfFolder, uniquePdfFileName);
                using (var pdfStream = new FileStream(pdfFilePath, FileMode.Create))
                {
                    await bookDto.PdfFile.CopyToAsync(pdfStream);
                }

                var book = new Book
                {
                    Title = bookDto.Title,
                    Author = bookDto.Author,
                    Description = bookDto.Description,
                    CoverImagePath = uniqueCoverFileName,
                    PdfFilePath = uniquePdfFileName,
                    CategoryId = bookDto.CategoryId
                };

                _context.Books.Add(book);
                await _context.SaveChangesAsync();

                return book;
            }
            throw new ArgumentException("Cover image and PDF file are required.");
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books.Include(b => b.Category).ToListAsync();
        }

        public async Task<Book> UpdateBookAsync(int id, BookDto bookDto)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                throw new ArgumentException("Book not found.");
            }

            // Update book properties here
            book.Title = bookDto.Title;
            book.Author = bookDto.Author;
            book.Description = bookDto.Description;
            book.CategoryId = bookDto.CategoryId;

            if (bookDto.CoverImage != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "coverimages");
                string uniqueCoverFileName = Guid.NewGuid().ToString() + "_" + bookDto.CoverImage.FileName;
                string coverFilePath = Path.Combine(uploadsFolder, uniqueCoverFileName);
                using (var fileStream = new FileStream(coverFilePath, FileMode.Create))
                {
                    await bookDto.CoverImage.CopyToAsync(fileStream);
                }
                book.CoverImagePath = uniqueCoverFileName;
            }

            if (bookDto.PdfFile != null)
            {
                string pdfFolder = Path.Combine(_hostEnvironment.WebRootPath, "pdffiles");
                string uniquePdfFileName = Guid.NewGuid().ToString() + "_" + bookDto.PdfFile.FileName;
                string pdfFilePath = Path.Combine(pdfFolder, uniquePdfFileName);
                using (var pdfStream = new FileStream(pdfFilePath, FileMode.Create))
                {
                    await bookDto.PdfFile.CopyToAsync(pdfStream);
                }
                book.PdfFilePath = uniquePdfFileName;
            }

            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            return book;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return false;
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
    }



}
