using System.ComponentModel.DataAnnotations;

namespace WordsHeavenPrj.Models
{
    public class RegisterModel
    {

        [Required]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Name must contain only alphabets and spaces.")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$", ErrorMessage = "Password must have at least one uppercase letter, one lowercase letter, one number, one special character, and be at least 8 characters long.")]
        public string Password { get; set; }

        [Phone]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Role { get; set; }

    }
}
