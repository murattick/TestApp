using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestApi.Models
{
    public class Category
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Превышена допустима длина строки")]
        [MinLength(2, ErrorMessage = "Слишком короткя длина строки")]
        public string Name { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Превышена допустима длина строки")]
        [MinLength(2, ErrorMessage = "Слишком короткя длина строки")]
        public string Discription { get; set; }
    }
}