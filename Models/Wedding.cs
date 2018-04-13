using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace wedding_planner.Models
{
    public class Wedding
    {
        [Key]
        public int WeddingId { get; set; }
        public string Names { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set;}
        public List<Guest> Guests { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    
        public Wedding()
        {
            Guests = new List<Guest>();
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
        
    }
}