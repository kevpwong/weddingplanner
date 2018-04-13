using System;
using System.ComponentModel.DataAnnotations;

namespace wedding_planner.Models
{
    public class Guest
    {
        [Key]
        public int GuestId { get; set; }
        public int UserId { get; set;}
        public int WeddingId { get; set;}
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    
        public Guest()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}