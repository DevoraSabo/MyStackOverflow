using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyStackOverflow.Data;
using MyStackOverflow.Web.Models;

namespace MyStackOverflow.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString;

        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            var repo = new QuestionRepository(_connectionString);
            var questions = repo.GetAllQuestions();
            return View(questions);
        }


        public IActionResult Question(int id)
        {
            var repo = new QuestionRepository(_connectionString);
            var q = repo.GetQuestion(id);
            
            return View(q);
        }




        public IActionResult AskAQuestion()
        {
            return View();
        }

        public IActionResult Add(Question question, IEnumerable<string> tags)
        {
            question.DatePosted = DateTime.Now;
            var repo = new QuestionRepository(_connectionString);
            repo.AddQuestion(question, tags);
            return Redirect("/questions");
        }
    }
}
