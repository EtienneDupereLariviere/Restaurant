using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class Resto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string PostalCode { get; set; }

        public string Url { get; set; }
    }
}