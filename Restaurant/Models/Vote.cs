using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class Vote
    {

        public int Id { get; set; }

        // Foreign Key
        [Required]
        public int RestoId { get; set; }

        // Navigation property
        Resto Resto { get; set; }

        // Foreign Key
        [Required]
        public int UserId { get; set; }

        // Navigation property
        User User { get; set; }
    }
}