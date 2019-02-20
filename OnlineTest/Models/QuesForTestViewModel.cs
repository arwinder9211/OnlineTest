using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTest.Models
{
    public class QuesForTestViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Question")]
        public string QuesString { get; set; }

        [Required]
        [Display(Name = "Time In Min")]
        public int Time { get; set; }

        [Required]
        [Display(Name = "Option A")]
        public string OptionA { get; set; }

        [Required]
        [Display(Name = "Option B")]
        public string OptionB { get; set; }

        [Display(Name = "Option C")]
        public string OptionC { get; set; }

        [Display(Name = "Option D")]
        public string OptionD { get; set; }

        [Required]
        public string Answer { get; set; }

        [Required]
        public int Marks { get; set; }

        public bool IsSelected { get; set; }
    }
}
