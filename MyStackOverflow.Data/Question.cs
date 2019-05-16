using System;
using System.Collections.Generic;
using System.Text;

namespace MyStackOverflow.Data
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime DatePosted { get; set; }
        public List<QuestionsTags> QuestionsTags { get; set; }
        public List<Answer> Answers { get; set; }
        public List<Likes> Likes { get; set; }
        //public List<Question> Questions { get; set; }
    }
}
