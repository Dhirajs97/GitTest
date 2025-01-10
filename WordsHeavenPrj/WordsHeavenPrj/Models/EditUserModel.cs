using System.ComponentModel.DataAnnotations;

namespace WordsHeavenPrj.Models
{
    public class EditUserModel
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
