using Microsoft.EntityFrameworkCore;
using OnlineTest.Data;
using OnlineTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTest.DAL
{
    public interface IQuestionRepo
    {
        List<Question> GetQuestions();
        void Add(Question model);
        Question GetById(int id);
        void Edit(int id, Question model);
    }

    public class QuestionRepo: IQuestionRepo
    {
        private ApplicationDbContext _db;
        public QuestionRepo(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// add question to database
        /// </summary>
        /// <param name="model"></param>
        public void Add(Question model)
        {
            _db.Questions.Add(model);
            _db.SaveChanges();
        }

        public void Edit(int id, Question model)
        {
            var question = _db.Questions.FirstOrDefault(q => q.Id == id);
            question.Answer = model.Answer;
            question.Marks = model.Marks;
            question.OptionA = model.OptionA;
            question.OptionB = model.OptionB;
            question.OptionC = model.OptionC;
            question.OptionD = model.OptionD;
            question.QuesString = model.QuesString;
            question.Time = model.Time;
            _db.SaveChanges();
        }

        /// <summary>
        /// return question by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Question GetById(int id)
        {
            return _db.Questions.FirstOrDefault(q => q.Id == id);
        }

        /// <summary>
        /// Get All question from db
        /// </summary>
        /// <returns>List<Question></returns>
        public List<Question> GetQuestions()
        {
            return _db.Questions.Include(a => a.TestQues).ToList();
        }

        
    }
}
