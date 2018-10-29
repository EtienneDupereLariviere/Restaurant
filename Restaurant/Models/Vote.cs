using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class Vote
    {
        public int Id { get; set; }

        [Required]
        public int RestoId { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}