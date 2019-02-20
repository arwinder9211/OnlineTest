using OnlineTest.DAL;
using OnlineTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTest.Services
{
    public interface IQuestionService
    {
        List<Question> GetQuestions();
        void Add(Question model);
        List<QuesForTestViewModel> GetQuesForTest(int id);
        QuesGiveTestViewModel GetQuestionForGivingTest(int testId, int no);
        void Edit(int id, Question model);
    }
    public class QuestionService: IQuestionService
    {
        private IQuestionRepo _questionRepo;
        private ITestQuesRepo _testQuesRepo;
        public QuestionService(
            IQuestionRepo questionRepo,
            ITestQuesRepo testQuesRepo
            )
        {
            _questionRepo = questionRepo;
            _testQuesRepo = testQuesRepo;
        }

        /// <summary>
        /// add question to database
        /// </summary>
        /// <param name="model"></param>
        public void Add(Question model)
        {
            // Extract answers according to selected option
            switch (model.Answer)
            {
                case "A":
                    model.Answer = model.OptionA;
                    break;
                case "B":
                    model.Answer = model.OptionB;
                    break;
                case "C":
                    model.Answer = model.OptionC;
                    break;
                case "D":
                    model.Answer = model.OptionD;
                    break;
                default:
                    model.Answer = model.OptionA;
                    break;

            }
            _questionRepo.Add(model);
        }

        /// <summary>
        /// Get All Questions
        /// </summary>
        /// <returns>List<Question></returns>
        public List<Question> GetQuestions()
        {
            return _questionRepo.GetQuestions();
        }

        /// <summary>
        /// get question for test
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<QuesForTestViewModel> GetQuesForTest(int id)
        {
            var questions = _questionRepo.GetQuestions();
            var quesViewModel = new List<QuesForTestViewModel>();
            foreach (var ques in questions)
            {
                if(ques.TestQues.FirstOrDefault(a => a.TestId == id)==null)
                {
                    var quesVM = new QuesForTestViewModel()
                    {
                       Answer = ques.Answer,
                       Id = ques.Id,
                       IsSelected = false,
                       Marks = ques.Marks,
                       OptionA = ques.OptionA,
                       OptionB = ques.OptionB,
                       OptionC = ques.OptionC,
                       OptionD = ques.OptionD,
                       QuesString = ques.QuesString,
                       Time = ques.Time
                       
                    };
                    quesViewModel.Add(quesVM);
                }
            }

            return quesViewModel;
        }


        /// <summary>
        /// get question for giving test
        /// </summary>
        /// <param name="testId"></param>
        /// <param name="no"></param>
        /// <returns>question</returns>
        public QuesGiveTestViewModel GetQuestionForGivingTest(int testId,int no)
        {
            no = --no;
            //get current question for student to attempt
            var testQues = _testQuesRepo.GetByTestId(testId).Skip(no).Take(1).First();
            var question = _questionRepo.GetById(testQues.QuestionId);
            var quesForTest = new QuesGiveTestViewModel()
            {
                Id = question.Id,
                OptionA = question.OptionA,
                OptionB = question.OptionB,
                OptionC = question.OptionC,
                OptionD = question.OptionD,
                QuesSting = question.QuesString,
                Time = question.Time,
                Marks = question.Marks,
                TestId = testId
            };
            return quesForTest;
        }

        public void Edit(int id, Question model)
        {
            _questionRepo.Edit(id,model);
        }
    }
}
