using OnlineTest.DAL;
using OnlineTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTest.Services
{
    public  interface ITestService
    {
        List<Test> GetTests();
        void Add(Test model);
        List<Test> GetTestByGrade(string grade);
        ScoreCard GetScore(int testId, int studentId);
        void Edit(int id, Test model);
    }
    public class TestService: ITestService
    {
        private ITestRepo _testRepo;
        private IResponseRepo _responseRepo;
        private IQuestionRepo _questionRepo;
        private ITestQuesRepo _testQuesRepo;
        public TestService(
            ITestRepo testRepo,
            IResponseRepo responseRepo,
            IQuestionRepo questionRepo,
            ITestQuesRepo testQuesRepo
            )
        {
            _testRepo = testRepo;
            _responseRepo = responseRepo;
            _questionRepo = questionRepo;
            _testQuesRepo = testQuesRepo;
        }

        /// <summary>
        /// Save test to db
        /// </summary>
        /// <param name="model"></param>
        public void Add(Test model)
        {
            model.IsActive = true;
            _testRepo.Add(model);
        }

        /// <summary>
        /// Get Test from db
        /// </summary>
        /// <returns></returns>
        public List<Test> GetTests()
        {
            return _testRepo.GetTests();
        }

        /// <summary>
        /// Get Tests for a particular grade
        /// </summary>
        /// <returns></returns>
        public List<Test> GetTestByGrade(string grade)
        {
            return _testRepo.GetTestByGrade(grade);
        }

        public ScoreCard GetScore(int testId, int studentId)
        {
            // calculating score and max marks
            var responses = _responseRepo.GetResponsesByStudentIdAndTestId(testId, studentId);
            var testQues = _testQuesRepo.GetByTestId(testId);
            var questions = new List<Question>();
            int maxMarks = 0;
            foreach (var item in testQues)
            {
                var ques = _questionRepo.GetById(item.QuestionId);
                maxMarks = maxMarks + ques.Marks;
            }
            
            int marks = 0;
            
            foreach(var item in responses)
            {
                var ques = _questionRepo.GetById(item.QuestionId);
                if(ques.Answer == item.Answer)
                {
                    marks = marks + ques.Marks;
                }
            }
            //calculating percentage    
            var per = (double)marks / (double)maxMarks * 100;
            var scoreCard = new ScoreCard()
            {
                MarksScored = marks,
                MaxMarks = maxMarks,
                Percentage = per
            };
            return scoreCard;
        }

        public void Edit(int id, Test model)
        {
            _testRepo.Edit(id,model);
        }
    }
}
