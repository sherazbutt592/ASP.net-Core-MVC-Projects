using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETickets.Models
{
    public class Cinema
    {
        [Key]
        public int Id { get; set; }
        [NotMapped]
        public IFormFile? File { get; set; }
        [ValidateNever]
        [Display(Name = "Logo")]
        public string Logo { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Display(Name = "Movies")]
        [ValidateNever]
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
