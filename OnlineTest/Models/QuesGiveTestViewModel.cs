using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTest.Models
{
    public class QuesGiveTestViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Question")]
        public string QuesSting { get; set; }

        [Display(Name = "Time In Min")]
        public int Time { get; set; }

        [Display(Name = "Option A")]
        public string OptionA { get; set; }

        [Display(Name = "Option B")]
        public string OptionB { get; set; }

        [Display(Name = "Option C")]
        public string OptionC { get; set; }

        [Display(Name = "Option D")]
        public string OptionD { get; set; }

        public bool A { get; set; }

        public bool B { get; set; }

        public bool C { get; set; }

        public bool D { get; set; }

        public string Answer { get; set; }
        public int  Marks { get; set; }
        public int No { get; set; }
        public int TestId { get;  set; }
    }
}
