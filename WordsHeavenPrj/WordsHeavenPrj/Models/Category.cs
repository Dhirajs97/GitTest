﻿using System.ComponentModel.DataAnnotations;

namespace WordsHeavenPrj.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
