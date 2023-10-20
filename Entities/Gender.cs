using System;
using System.ComponentModel.DataAnnotations;

namespace movies_api.Entities
{
    public class Gender
    {
        public int GenderId { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        public DateTime Date { get; set; }
        [Range(0, 1, ErrorMessage = "O campo deve ser 0 ou 1")]
        public int Active { get; set; }

    }
}
