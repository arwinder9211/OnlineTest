using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineTest.Models;
using OnlineTest.Services;

namespace OnlineTest.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IStudentService _studentService;
        private IQuestionService _questionService;
        private ITestService _testService;
        private ITestQuesService _testQuesService;
        public AdminController(
            IStudentService studentService, 
            IQuestionService questionService,
            ITestService testService,
            ITestQuesService testQuesService
            )
        {
            _studentService = studentService;
            _questionService = questionService;
            _testService = testService;
            _testQuesService = testQuesService;
        }

        /// <summary>
        /// Show all students to admin
        /// </summary>
        /// <returns>View("Students")</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View(_studentService.GetStudents());
        }

        /// <summary>
        /// Show all questions to admin
        /// </summary>
        /// <returns>View("Questions")</returns>
        [HttpGet]
        public IActionResult Questions()
        {
            return View(_questionService.GetQuestions());
        }

        /// <summary>
        /// Render View for creating question
        /// </summary>
        /// <returns>View("CreateQuestion")</returns>>
        [HttpGet]
        public  IActionResult CreateQuestion()
        {
            return View();
        }

        /// <summary>
        /// Create a question
        /// </summary>
        /// <returns>View("CreateQuestion")</returns>
        [HttpPost]
        public IActionResult CreateQuestion(Question model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _questionService.Add(model);
                    ViewBag.Msg = "Question Created Successfully";
                }
            }
            catch
            {
                ViewBag.Msg = "Something went wrong";
            }
            return View();
        }

        /// <summary>
        /// Show tests
        /// </summary>
        /// <returns>View("Tests")</returns>
        public IActionResult Tests()
        {
            return View(_testService.GetTests());
        }

        /// <summary>
        /// Create view for creating test
        /// </summary>
        /// <returns>View()</returns>
        [HttpGet]
        public IActionResult CreateTest()
        {
            return View();
        }

        /// <summary>
        /// Create test
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateTest(Test model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _testService.Add(model);
                    ViewBag.Msg = "Success";
                }
                else
                {
                    ViewBag.Error = "Something went wrong";
                }
            }
            catch
            {
                ViewBag.Error = "Something went wrong";
            }
            return View();
        }

        /// <summary>
        /// render view for adding questions
        /// </summary>
        /// <returns>View("questions")</returns>
        public IActionResult AddQues(int id)
        {
            var questions = _questionService.GetQuesForTest(id);
            ViewBag.Id = id;
            return View(questions);
        }

        /// <summary>
        /// Add question to test
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddQues(int id, List<QuesForTestViewModel> model)
        {
            try
            {
                _testQuesService.Add(id, model);
                ViewBag.Msg = "Success";
            }
            catch
            {
                ViewBag.Error = "Something went wrong";
            }
            var questions = _questionService.GetQuesForTest(id);
            ViewBag.Id = id;
            return View(questions);
        }

        /// <summary>
        /// Render view for editing test
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View("test")</returns>
        [HttpGet]
        public IActionResult EditTest(int id)
        {
            var test = _testService.GetTests().FirstOrDefault(t => t.Id == id);
            return View(test);
        }

        /// <summary>
        /// edit test
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View()</returns>
        [HttpPost]
        public IActionResult EditTest(int id, Test model)
        {
            try
            {
                _testService.Edit(id,model);
                ViewBag.Msg = "Success";
            }
            catch
            {
                ViewBag.Error = "Something went wrong";
            }
            var test = _testService.GetTests().FirstOrDefault(t => t.Id == id);
            return View(test);
        }

        /// <summary>
        /// Render view for editing question
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View(question)</returns>
        [HttpGet]
        public IActionResult EditQues(int id)
        {
            var question = _questionService.GetQuestions().FirstOrDefault(q => q.Id == id);
            return View(question);
        }

        /// <summary>
        /// Edit and save question in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View(question</returns>
        [HttpPost]
        public IActionResult EditQues(int id, Question model)
        {
            try
            {
                _questionService.Edit(id,model);
                ViewBag.Msg = "Success";
            }
            catch
            {
                ViewBag.Error = "Something went wrong";
            }
            var question = _questionService.GetQuestions().FirstOrDefault(q => q.Id == id);
            return View(question);
        }

    }
}