using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTest.Models
{
    public class ScoreCard
    {
        [Display(Name = "Maximum Marks")]
        public int MaxMarks { get; set; }
        [Display(Name = "Marks Scored")]
        public int MarksScored { get; set; }
        public double Percentage { get; set; }
    }
}
