using OnlineTest.Data;
using OnlineTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTest.DAL
{
    public interface ITestQuesRepo
    {
        void Add(TestQues testQues);
        List<TestQues> GetByTestId(int testId);
    }
    public class TestQuesRepo: ITestQuesRepo
    {
        private ApplicationDbContext _db;
        public TestQuesRepo(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Map test to ques
        /// </summary>
        /// <param name="testQues"></param>
        public void Add(TestQues testQues)
        {
            _db.TestQues.Add(testQues);
            _db.SaveChanges();
        }

        /// <summary>
        /// Get By TestId
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        public List<TestQues> GetByTestId(int testId)
        {
            return _db.TestQues.Where(t => t.TestId == testId).ToList();
        }
    }
}
