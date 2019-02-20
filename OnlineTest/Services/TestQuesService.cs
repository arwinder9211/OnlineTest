using OnlineTest.DAL;
using OnlineTest.Data;
using OnlineTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTest.Services
{
    public interface ITestQuesService
    {
        void Add(int testId, List<QuesForTestViewModel> model);
        List<TestQues> GetTestQuesByTestId(int testId);
    }
    public class TestQuesService: ITestQuesService
    {
        private ITestQuesRepo _testQuesRepo;
        public TestQuesService(ITestQuesRepo testQuesRepo)
        {
            _testQuesRepo = testQuesRepo;
        }

        /// <summary>
        /// Map test to question
        /// </summary>
        /// <param name="testId"></param>
        /// <param name="model"></param>
        public void Add(int testId,List<QuesForTestViewModel> model)
        {
            foreach (var item in model)
            {
                if (item.IsSelected == true)
                {
                    var testQues = new TestQues()
                    {
                        QuestionId = item.Id,
                        TestId = testId
                    };
                    _testQuesRepo.Add(testQues);
                }
            }
        }

        public List<TestQues> GetTestQuesByTestId(int testId)
        {
            return _testQuesRepo.GetByTestId(testId);
        }
    }
}
