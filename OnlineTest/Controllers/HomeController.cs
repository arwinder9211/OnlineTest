using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineTest.Models;
using OnlineTest.Services;

namespace OnlineTest.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private ITestService _testService;
        private IStudentService _studentService;
        private IQuestionService _questionService;
        private ITestQuesService _testQuesService;
        private IResponseService _responseService;
        public HomeController(
            UserManager<ApplicationUser> userManager,
            ITestService testService,
            IStudentService studentService,
            IQuestionService questionService,
            ITestQuesService testQuesService,
            IResponseService responseService
            )
        {
            _userManager = userManager;
            _testService = testService;
            _studentService = studentService;
            _questionService = questionService;
            _testQuesService = testQuesService;
            _responseService = responseService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "OnlineTest a fully automated test portal";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Contact Us";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Student can view test to be given
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Tests()
        {
            var email = await GetEmail();
            var student = _studentService.GetStudentByEmail(email);
            var tests = _testService.GetTestByGrade(student.Grade);
            return View(tests);
        }

        /// <summary>
        /// Render view for giving test
        /// </summary>
        /// <param name="id"></param>
        /// <param name="no"></param>
        /// <returns>View("question")</returns>
        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> QuesForTest(int id,int no)
        {
            try
            {
                var email = await GetEmail();
                var student = _studentService.GetStudentByEmail(email);
                var test = _testService.GetTests().FirstOrDefault(t => t.Id == id);
                // check if test has expired by admin or not
                if (test.IsActive == false)
                {
                    ViewBag.Error = "Test has expired you can only view score";
                    return View("TestCompleted");
                }

                //check if user has previously given the test or not
                if (_responseService.GetResponsesByStudentIdAndTestId(id,student.Id).Count()>0)
                {
                    ViewBag.Error = "You have already given this test";
                    return View("TestCompleted");
                }
                
                var question = _questionService.GetQuestionForGivingTest(id, no);
                ViewBag.Id = id;
                ViewBag.Count = _testQuesService.GetTestQuesByTestId(id).Count();
                ViewBag.No = no;
                return View(question);
            }
            catch(InvalidOperationException ioe)
            {
                ViewBag.Error = "No Questions in the test";
                return View("TestCompleted");
            }
            catch(Exception e)
            {
                ViewBag.Error = "Something went wrong";
                return View("TestCompleted");
            }
            
        }

        /// <summary>
        /// show next question and save responses
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> QuesForTest(int id, QuesGiveTestViewModel model)
        {
            try
            {
                var email = await GetEmail();
                var student = _studentService.GetStudentByEmail(email);
                _responseService.Add(model.TestId, student.Id, model);
                ViewBag.Id = model.TestId;

                var count = _testQuesService.GetTestQuesByTestId(model.TestId).Count();
                ViewBag.Count = count;
                if (count <= model.No)
                {
                    return RedirectToAction("TestCompleted");
                }
                var question = _questionService.GetQuestionForGivingTest(model.TestId, model.No + 1);
                ViewBag.No = ++model.No;

                return View(question);
            }
            catch(Exception e)
            {
                ViewBag.Error = "Something went wrong";
                return View("TestCompleted");
            }
            
        }

        /// <summary>
        /// show score of selected test
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View(scoreCard)</returns>
        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> ViewScore(int id)
        {
            var email = await GetEmail();
            var student = _studentService.GetStudentByEmail(email);
            var scoreCard = _testService.GetScore(id,student.Id);
            ViewBag.Id = id;
            if(Double.IsNaN(scoreCard.Percentage))
            {
                ViewBag.Msg = "Score not genererated. Please complete the test";
            }
            return View(scoreCard);
        }

        /// <summary>
        /// Show that test is completed successfully
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "User")]
        public IActionResult TestCompleted()
        {
            ViewBag.Msg = "Test Completed Successfully";
            return View();
        }

        /// <summary>
        /// get email of logged in user
        /// </summary>
        /// <returns></returns>
        
        private async Task<string> GetEmail()
        {
            var user = await _userManager.GetUserAsync(User);
            var email = user.Email;
            return email;
        }


    }
}
