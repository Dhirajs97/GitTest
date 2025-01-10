using System.Collections.Generic;
using System.Threading.Tasks;
using WordsHeavenPrj.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using WordsHeavenPrj.Data;

namespace WordsHeavenPrj.Services
{

    public class AudioBookService : IAudioBookService
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AudioBookService> _logger;

        public AudioBookService(IWebHostEnvironment hostEnvironment, ApplicationDbContext context, ILogger<AudioBookService> logger)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
            _logger = logger;
        }

        public async Task<AudioBook> AddAudioBookAsync(AudioBookDto audioBookDto)
        {
            try { 
                if (audioBookDto.CoverImage != null && audioBookDto.AudioFile != null) { 
                    string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "audiobookcovers"); 
                    Directory.CreateDirectory(uploadsFolder); 
                    string uniqueCoverFileName = Guid.NewGuid().ToString() + "_" + audioBookDto.CoverImage.FileName; 
                    string coverFilePath = Path.Combine(uploadsFolder, uniqueCoverFileName); 
                    using (var fileStream = new FileStream(coverFilePath, FileMode.Create)) { 
                        await audioBookDto.CoverImage.CopyToAsync(fileStream); 
                    } 
                    string audioFolder = Path.Combine(_hostEnvironment.WebRootPath, "audiobooks"); 
                    Directory.CreateDirectory(audioFolder); 
                    string uniqueAudioFileName = Guid.NewGuid().ToString() + "_" + audioBookDto.AudioFile.FileName; 
                    string audioFilePath = Path.Combine(audioFolder, uniqueAudioFileName); 
                    using (var audioStream = new FileStream(audioFilePath, FileMode.Create)) { 
                        await audioBookDto.AudioFile.CopyToAsync(audioStream); 
                    } 
                    var audioBook = new AudioBook { 
                        Title = audioBookDto.Title, 
                        Author = audioBookDto.Author, 
                        Reader = audioBookDto.Reader, 
                        CoverImagePath = uniqueCoverFileName, 
                        AudioFilePath = uniqueAudioFileName, 
                        CategoryId = audioBookDto.CategoryId 
                    }; 
                    _context.AudioBooks.Add(audioBook); await _context.SaveChangesAsync(); return audioBook; 
                } 
                throw new ArgumentException("Cover image and audio file are required."); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding an audio book."); 
                throw; // Re-throw to bubble up the exception
            } 
        }
        public async Task<AudioBook> GetAudioBookByIdAsync(int id)
        {
            return await _context.AudioBooks.Include(ab => ab.Category).FirstOrDefaultAsync(ab => ab.Id == id);
        }

        public async Task<IEnumerable<AudioBook>> GetAllAudioBooksAsync()
        {
            return await _context.AudioBooks.Include(ab => ab.Category).ToListAsync();
        }

        public async Task<AudioBook> UpdateAudioBookAsync(int id, AudioBookDto audioBookDto)
        {
            var audioBook = await _context.AudioBooks.FindAsync(id);
            if (audioBook == null)
            {
                throw new ArgumentException("AudioBook not found.");
            }

            // Update audioBook properties here
            audioBook.Title = audioBookDto.Title;
            audioBook.Author = audioBookDto.Author;
            audioBook.Reader = audioBookDto.Reader;
            audioBook.CategoryId = audioBookDto.CategoryId;

            if (audioBookDto.CoverImage != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "audiobookcovers");
                string uniqueCoverFileName = Guid.NewGuid().ToString() + "_" + audioBookDto.CoverImage.FileName;
                string coverFilePath = Path.Combine(uploadsFolder, uniqueCoverFileName);
                using (var fileStream = new FileStream(coverFilePath, FileMode.Create))
                {
                    await audioBookDto.CoverImage.CopyToAsync(fileStream);
                }
                audioBook.CoverImagePath = uniqueCoverFileName;
            }

            if (audioBookDto.AudioFile != null)
            {
                string audioFolder = Path.Combine(_hostEnvironment.WebRootPath, "audiobooks");
                string uniqueAudioFileName = Guid.NewGuid().ToString() + "_" + audioBookDto.AudioFile.FileName;
                string audioFilePath = Path.Combine(audioFolder, uniqueAudioFileName);
                using (var audioStream = new FileStream(audioFilePath, FileMode.Create))
                {
                    await audioBookDto.AudioFile.CopyToAsync(audioStream);
                }
                audioBook.AudioFilePath = uniqueAudioFileName;
            }

            _context.AudioBooks.Update(audioBook);
            await _context.SaveChangesAsync();

            return audioBook;
        }

        public async Task<bool> DeleteAudioBookAsync(int id)
        {
            var audioBook = await _context.AudioBooks.FindAsync(id);
            if (audioBook == null)
            {
                return false;
            }

            _context.AudioBooks.Remove(audioBook);
            await _context.SaveChangesAsync();
            return true;
        }
    }



}
