using System;
using System.Collections.Generic;
using System.Text;

namespace MyStackOverflow.Data
{
    public class Likes
    {
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public Question Question { get; set; }
        public User User { get; set; }
    }
}
