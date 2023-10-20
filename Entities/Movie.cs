using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace movies_api.Entities
{
    public class Movie
    {
        
        public int MovieId { get; set; }
        [Required, StringLength(200)]
        public string Name { get; set; }
        public DateTime Date { get; set; }
        [Range(0,1, ErrorMessage = "O campo deve ser 0 ou 1")]
        public int Active { get; set; }
        public int GenderId { get; set; }
        public Gender Gender { get; set; }

        public List<Location> Locations { get; set; }
    }
}
