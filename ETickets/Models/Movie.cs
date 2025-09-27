using ETickets.Data;
using ETickets.Data.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETickets.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Title")]
        public string Name { get; set; }

        [Required]
        [Display(Name ="Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name ="Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name ="End Date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name ="Price")]
        public double Price { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
        [Display(Name = "Movie Poster")]
        public string Image { get; set; }

        [Required]
        [Display(Name = "Movie Category")]
        public MovieCategory MovieCategory { get; set; }

        [Required]
        [Display(Name = "Select Actors")]
        public ICollection<Actor> Actors { get; set; } = new List<Actor>();

        [Required]
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }

        [Required]
        public int ProducerId { get; set; }
        public Producer Producer { get; set; }

    }
}
