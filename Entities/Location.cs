using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace movies_api.Entities
{
    public class Location
    {
        public int LocationId { get; set; }
        
        [Required, StringLength(14)]
        public string CPFClient { get; set; }
        public DateTime DateLocation { get; set; }

        [Required]
        public List<Movie> Movies { get; set; }
    }
}
