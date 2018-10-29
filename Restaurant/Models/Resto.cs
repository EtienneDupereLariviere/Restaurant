using System.Collections.Generic;

namespace Restaurant.Models
{
    public class Resto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public List<Survey> Poll { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string PostalCode { get; set; }

        public string Url { get; set; }
    }
}