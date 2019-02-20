using OnlineTest.Data;
using OnlineTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTest.DAL
{
    public interface IResponseRepo
    {
        void Add(Response model);
        List<Response> GetResponsesByStudentIdAndTestId(int testId, int studentId);
    }
    public class ResponseRepo : IResponseRepo
    {
        private ApplicationDbContext _db;
        public ResponseRepo(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Save student response to db
        /// </summary>
        /// <param name="model"></param>
        public void Add(Response model)
        {
            _db.Responses.Add(model);
            _db.SaveChanges();
        }

        /// <summary>
        /// get response of student on a test
        /// </summary>
        /// <param name="testId"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public List<Response> GetResponsesByStudentIdAndTestId(int testId, int studentId)
        {
            return _db.Responses.Where(a => a.TestId == testId && a.StudentId == studentId).ToList();
        }
    }
}
