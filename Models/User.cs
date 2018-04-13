using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace wedding_planner.Models
{
    public class User
    {   
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Wedding> Weddings { get; set; }
        public List<Guest> Guests { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public User()
        {
            Weddings = new List<Wedding>();
            Guests = new List<Guest>();
        }
    }
}