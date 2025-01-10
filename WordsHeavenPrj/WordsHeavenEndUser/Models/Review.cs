namespace WordsHeavenEndUser.Models
{
    public class Review
    {

        public int ReviewId { get; set; }        
        public int EndUserId { get; set; }       
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; } = DateTime.Now;        
        public EndUser EndUser { get; set; }
        

    }
}
