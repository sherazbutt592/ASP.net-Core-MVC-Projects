using System.ComponentModel.DataAnnotations;

namespace Diary_App.Models
{
    public class DiaryEntry
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "please enter title!")]
        [StringLength(100, ErrorMessage = "title length must be between 3 to 100", MinimumLength = 3)]
        public string Title { get; set; } = string.Empty; //string.Empty represents the default value.
        [Required(ErrorMessage = "Please Enter Content!")]
        public string Content { get; set; } = string.Empty ;
        [Required]
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
