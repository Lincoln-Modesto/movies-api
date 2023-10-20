using System;
using System.ComponentModel.DataAnnotations;

namespace movies_api.DTO
{
    public class MovieDTO
    {
        public int MovieId { get; set; }
        public string Name { get; set; }
        public int GenderId { get; set; }
        public string GenderName { get; set; }
        public DateTime Date { get; set; }
        public bool Active { get; set; }
    }
}
