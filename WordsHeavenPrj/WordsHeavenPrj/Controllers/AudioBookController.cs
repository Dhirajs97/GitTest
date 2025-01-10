using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using WordsHeavenPrj.Data;
using WordsHeavenPrj.Models;
using WordsHeavenPrj.Services;

[Route("audiobooks")]
public class AudioBookController : Controller
{
    private readonly IAudioBookService _audioBookService;
    private readonly ApplicationDbContext _context;
    private readonly ILogger<AudioBookController> _logger;

    public AudioBookController(IAudioBookService audioBookService, ApplicationDbContext context, ILogger<AudioBookController> logger)
    {
        _audioBookService = audioBookService;
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    [Route("add")]
    public IActionResult AddAudioBook()
    {
        var categories = _context.Categories.ToList();
        ViewBag.Categories = new SelectList(categories, "Id", "Name");
        return View();
    }

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddAudioBook([FromForm] AudioBookDto audioBookDto)
    {
        if (!ModelState.IsValid)
        {
            var categories = _context.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(audioBookDto);
        }

        try
        {
            var audioBook = await _audioBookService.AddAudioBookAsync(audioBookDto);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding audiobook");
            ModelState.AddModelError(string.Empty, "An error occurred while adding the audiobook.");
            var categories = _context.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(audioBookDto);
        }
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Index()
    {
        var audioBooks = await _audioBookService.GetAllAudioBooksAsync();
        return View(audioBooks);
    }

    [HttpGet]
    [Route("details/{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var audioBook = await _audioBookService.GetAudioBookByIdAsync(id);
        if (audioBook == null)
        {
            _logger.LogWarning("AudioBook with ID {AudioBookId} not found", id);
            return NotFound();
        }
        return View(audioBook);
    }

    [HttpGet]
    [Route("edit/{id}")]
    public async Task<IActionResult> EditAudioBook(int id)
    {
        var audioBook = await _audioBookService.GetAudioBookByIdAsync(id);
        if (audioBook == null)
        {
            _logger.LogWarning("AudioBook with ID {AudioBookId} not found", id);
            return NotFound();
        }

        var categories = _context.Categories.ToList();
        ViewBag.Categories = new SelectList(categories, "Id", "Name", audioBook.CategoryId);

        var audioBookDto = new AudioBookDto
        {
            Title = audioBook.Title,
            Author = audioBook.Author,
            Reader = audioBook.Reader,
            CategoryId = audioBook.CategoryId
        };

        return View(audioBookDto);
    }

    [HttpPost]
    [Route("edit/{id}")]
    public async Task<IActionResult> EditAudioBook(int id, [FromForm] AudioBookDto audioBookDto)
    {
        if (!ModelState.IsValid)
        {
            var categories = _context.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(audioBookDto);
        }

        try
        {
            var audioBook = await _audioBookService.UpdateAudioBookAsync(id, audioBookDto);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error editing audiobook with ID {AudioBookId}", id);
            ModelState.AddModelError(string.Empty, "An error occurred while editing the audiobook.");
            var categories = _context.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(audioBookDto);
        }
    }



    [HttpGet] 
    [Route("delete/{id}")] 
    public async Task<IActionResult> Delete(int id) { var audioBook = await _audioBookService.GetAudioBookByIdAsync(id); if (audioBook == null) { _logger.LogWarning("AudioBook with ID {AudioBookId} not found", id); return NotFound(); } return View(audioBook); }
    [HttpPost, ActionName("Delete")][Route("delete/{id}")] public async Task<IActionResult> DeleteConfirmed(int id) { try { var success = await _audioBookService.DeleteAudioBookAsync(id); if (!success) { return NotFound(); } return RedirectToAction("Index"); } catch (Exception ex) { _logger.LogError(ex, "Error deleting audiobook with ID {AudioBookId}", id); return BadRequest(ex.Message); } }



}
