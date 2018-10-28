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

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public bool Active { get; set; }

        public List<Resto> Restos { get; set; }

        public List<User> Users { get; set; }

        public List<Vote> Votes { get; set; }
    }
}