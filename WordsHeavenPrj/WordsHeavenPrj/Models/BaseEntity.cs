using System.ComponentModel.DataAnnotations.Schema;

namespace WordsHeavenPrj.Models
{
    public class BaseEntity
    {
        [Column(TypeName = "datetime")] 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;                
        
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }
        

        [Column(TypeName = "nvarchar(50)")]
        public int UpdatedBy { get; set; }
    }
}
