using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETickets.Models
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }

        [Display(Name = "Picture")] //This attribute is used to specify a user-friendly name for a property, which is then used in the UI
        [ValidateNever]
        public string ProfilePicture { get; set; } = string.Empty;


        [Display(Name = "Name")]
        [Required(ErrorMessage = "Full Name is required")]
        public string FullName { get; set; }


        [Required(ErrorMessage = "Bio is required")]
        [Display(Name = "Bio")]
        public string Bio { get; set; }


        [Display(Name = "Movies")]
        [ValidateNever]
        public ICollection<Movie> Movies { get; set; }= new List<Movie>();
    }
}
