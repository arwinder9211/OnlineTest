using OnlineTest.DAL;
using OnlineTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTest.Services
{
    public interface IStudentService
    {
        List<Student> GetStudents();
        Student GetStudentByEmail(string email);
    }
    public class StudentService : IStudentService
    {
        private IStudentRepo _studentRepo;
        public StudentService(IStudentRepo studentRepo)
        {
            _studentRepo = studentRepo;
        }

        /// <summary>
        /// Retun student from db of an email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Student GetStudentByEmail(string email)
        {
            return _studentRepo.GetStudentByEmail(email);
        }

        /// <summary>
        /// Get All Students from db
        /// </summary>
        /// <returns></returns>
        public List<Student> GetStudents()
        {
            return _studentRepo.GetStudents();
        }
    }
}
