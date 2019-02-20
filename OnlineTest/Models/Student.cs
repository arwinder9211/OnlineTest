using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTest.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Grade { get; set; }

        [Required]
        public string Name { get; set; }

        public List<TestStu> TestStus { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public bool IsActive { get; set; }
    }
}
