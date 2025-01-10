using Microsoft.AspNetCore.Http;

namespace WordsHeavenPrj.Models
{
    public class BookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public IFormFile CoverImage { get; set; }
        public IFormFile PdfFile { get; set; }
        public int CategoryId { get; set; }
    }
}
