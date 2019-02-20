using OnlineTest.Data;
using OnlineTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTest.DAL
{
    public interface ITestRepo
    {
        List<Test> GetTests();
        void Add(Test model);
        List<Test> GetTestByGrade(string grade);
        void Edit(int id, Test model);
    }
    public class TestRepo: ITestRepo
    {
        private ApplicationDbContext _db;
        public TestRepo(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Get All Test
        /// </summary>
        /// <returns>List<Test></returns>
        public List<Test> GetTests()
        {
            return _db.Tests.ToList();
        }

        /// <summary>
        /// Save test to db
        /// </summary>
        /// <param name="model"></param>
        public void Add(Test model)
        {
            _db.Tests.Add(model);
            _db.SaveChanges();
        }

        /// <summary>
        /// Get test for particular grade
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public List<Test> GetTestByGrade(string grade)
        {
            return _db.Tests.Where(g => g.Grade == grade).ToList();
        }

        public void Edit(int id, Test model)
        {
            var test = _db.Tests.FirstOrDefault(t => t.Id == id);
            test.IsActive = model.IsActive;
            _db.SaveChanges();
        }
    }
}
