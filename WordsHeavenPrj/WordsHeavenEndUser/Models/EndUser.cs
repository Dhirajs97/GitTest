namespace WordsHeavenEndUser.Models
{
    public class EndUser
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Review> Reviews { get; set; }

    }
}
