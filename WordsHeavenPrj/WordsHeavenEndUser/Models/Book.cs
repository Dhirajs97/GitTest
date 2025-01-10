namespace WordsHeavenEndUser.Models
{
    public class Book
    {
            public int Id { get; set; }

            
            public string Title { get; set; }

            
            public string Author { get; set; }

            public string Description { get; set; }

            public string CoverImagePath { get; set; } // Path to the cover image

            public string PdfFilePath { get; set; } // Path to the PDF file

            
            public int CategoryId { get; set; }

            public Category Category { get; set; }
    

    }
}
