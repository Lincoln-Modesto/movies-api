using Microsoft.VisualBasic;
using movies_api.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace movies_api.ViewModel
{
    public class MovieViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int GenderId { get; set; }
        [Required]
        public bool Active { get; set; }

        public DateFormat Date { get; set; }

        public bool IsValid(IEnumerable<Gender> validGenres)
        {
            return validGenres.Any(x => x.GenderId == GenderId);
        }
    }
}
