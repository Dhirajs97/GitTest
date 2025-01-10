using System;

namespace WordsHeavenPrj.Models
{
    public class AudioBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Reader { get; set; }  // Added Reader property
        //public TimeSpan Duration { get; set; }
        public string CoverImagePath { get; set; }  // Path to the cover image
        public string AudioFilePath { get; set; }  // Path to the audio file
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
