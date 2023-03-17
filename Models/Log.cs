using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Log
    {
        public int ID { get; set; }
        [Required]
        [EmailAddress]
        public string UserId { get; set; }
        [Required]
        public string Password { get; set; }
    }
}