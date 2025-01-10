using Microsoft.AspNetCore.Http;

namespace WordsHeavenPrj.Models
{
    public class AudioBookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Reader { get; set; }
        public IFormFile CoverImage { get; set; }
        public IFormFile AudioFile { get; set; }
        public int CategoryId { get; set; }
    }
}
