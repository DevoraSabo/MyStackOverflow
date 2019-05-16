using System;
using System.Collections.Generic;
using System.Text;

namespace MyStackOverflow.Data
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }

        public List<Likes> Likes { get; set; }
        public List<Question> Questions { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
