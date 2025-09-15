using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;

namespace ETickets.Models
{
    public class Producer
    {
        [Key]
        public int Id { get; set; }
        [NotMapped]
        public IFormFile? File { get; set; }
        [ValidateNever]
        [Display(Name = "Picture")]
        public string ProfilePicture { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        public string FullName { get; set; }
        [Display(Name = "Bio")]
        [Required(ErrorMessage = "Bio is required")]
        public string Bio { get; set; }
        [Display(Name = "Movies")]
        [ValidateNever]
        public ICollection<Movie> Movies { get; set; }= new List<Movie>();
    }
}
