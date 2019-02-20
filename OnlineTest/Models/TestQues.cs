using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTest.Models
{
    public class TestQues
    {
        public int TestId { get; set; }
        public Test Test { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
