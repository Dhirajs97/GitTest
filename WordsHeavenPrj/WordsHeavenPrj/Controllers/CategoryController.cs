using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WordsHeavenPrj.Models;
using WordsHeavenPrj.Services;
using Microsoft.Extensions.Logging;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace WordsHeavenPrj.Controllers
{
    
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories);
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(Category model)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.AddCategoryAsync(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> EditCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(Category model)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.UpdateCategoryAsync(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet][Route("delete/{id}")] 
        public async Task<IActionResult> Delete(int id) { 
            var category = await _categoryService.GetCategoryByIdAsync(id); 
            if (category == null) { 
                _logger.LogWarning("Category with ID {CategoryId} not found", id); 
                return NotFound(); 
            } 
            return View(category); 
        }
        
        [HttpPost, ActionName("Delete")]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var success = await _categoryService.DeleteCategoryAsync(id);
                if (!success)
                {
                    return NotFound();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting category with ID {CategoryId}", id);
                return BadRequest(ex.Message);
            }
        }

    }
}
