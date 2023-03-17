using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Emp
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Gmail { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public Nullable<int> Salary { get; set; }
    }
}