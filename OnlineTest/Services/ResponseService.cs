using OnlineTest.DAL;
using OnlineTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTest.Services
{
    public interface IResponseService
    {
        void Add(int testId, int studentId, QuesGiveTestViewModel model);
        List<Response> GetResponsesByStudentIdAndTestId(int testId, int studentId);
    }
    public class ResponseService: IResponseService
    {
        private IResponseRepo _responseRepo;
        private IQuestionRepo _questionRepo;
        public ResponseService(
            IResponseRepo responseRepo,
            IQuestionRepo questionRepo
            )
        {
            _responseRepo = responseRepo;
            _questionRepo = questionRepo;
        }

        public void Add(int testId, int studentId, QuesGiveTestViewModel model)
        {
            var question = _questionRepo.GetById(model.Id);
            var response = new Response();
            switch (model.Answer)
            {
                case "A":
                    response.Answer = question.OptionA;
                    break;
                case "B":
                    response.Answer = question.OptionB;
                    break;
                case "C":
                    response.Answer = question.OptionC;
                    break;
                case "D":
                    response.Answer = question.OptionD;
                    break;
                default:
                    response.Answer = "";
                    break;

            }
            response.DataTime = DateTime.Now;
            response.QuestionId = model.Id;
            response.TestId = testId;
            response.StudentId = studentId;
            _responseRepo.Add(response);
        }

        public List<Response> GetResponsesByStudentIdAndTestId(int testId, int studentId)
        {
            return _responseRepo.GetResponsesByStudentIdAndTestId(testId, studentId);
        }
    }
}
