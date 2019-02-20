using OnlineTest.Data;
using OnlineTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTest.DAL
{
    public interface IStudentRepo
    {
        List<Student> GetStudents();
        Student GetStudentByEmail(string email);
    }
    public class StudentRepo: IStudentRepo
    {
        private ApplicationDbContext _db;
        public StudentRepo(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Get All Students From Db
        /// </summary>
        /// <returns></returns>
        public List<Student> GetStudents()
        {
            return _db.Students.ToList();
        }

        /// <summary>
        /// Return user of an email from db
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Student GetStudentByEmail(string email)
        {
            return _db.Students.FirstOrDefault(s => s.Email == email);
        }
    }
}
