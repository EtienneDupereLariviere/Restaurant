using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public class Survey
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public virtual List<Resto> Restaurants { get; set; }

        [Required]
        public virtual List<User> Users { get; set; }

        public bool Active { get; set; }

        public virtual List<Vote> Votes { get; set; }
    }
}