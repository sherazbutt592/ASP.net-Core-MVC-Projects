using ETickets.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace ETickets.Models
{
    public class MovieViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int CinemaId { get; set; }
        [Required]
        public int ProducerId { get; set; }
        [Required]
        public string Description { get; set; }

        public string? Image { get; set; }
        public IFormFile? File { get; set; }
        [Required]
        public MovieCategory MovieCategory { get; set; }
        [Required]
        public ICollection<int> ActorIds { get; set; } = new List<int>();
    }
}
