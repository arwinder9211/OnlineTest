using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTest.Models
{
    public class Test
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Grade { get; set; }

        public List<TestQues> TestQues { get; set; }

        public List<TestStu> TestStus { get; set; }

        public bool IsActive { get; set; }
    }
}
