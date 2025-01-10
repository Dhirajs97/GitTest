using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace WordsHeavenPrj.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string Name { get; set; }
        public bool IsActive { get; set; } = false; 

        [Column(TypeName = "datetime")] 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
        
        [Column(TypeName = "datetime")] 
        public DateTime? UpdatedAt { get; set; }

    }
}
