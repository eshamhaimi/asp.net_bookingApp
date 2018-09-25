using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookingApp.Models.AppModel
{
    public class AppUser
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Username { get; set; }

        public bool isBanned { get; set; }

        public string Role { get; set; }
    }
}