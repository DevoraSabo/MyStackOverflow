using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyStackOverflow.Data
{
    public class QuestionRepository
    {
        private string _connectionString;

        public QuestionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Question> GetAllQuestions()
        {
            using (var ctx = new DBContext(_connectionString))
            {
                return ctx.Questions.OrderBy(q => q.DatePosted).ToList();
            }
        }

        public Question GetQuestion(int id)
        {
            using (var ctx = new DBContext(_connectionString))
            {
                return ctx.Questions.Include(t => t.QuestionsTags).ThenInclude(a => a.Tag)
                    .FirstOrDefault(q => q.Id == id);
            }
        }

        private Tag GetTag(string name)
        {
            using (var ctx = new DBContext(_connectionString))
            {
                return ctx.Tags.FirstOrDefault(t => t.Name == name);
            }
        }

        private int AddTag(string name)
        {
            using (var ctx = new DBContext(_connectionString))
            {
                var tag = new Tag { Name = name };
                ctx.Tags.Add(tag);
                ctx.SaveChanges();
                return tag.Id;
            }
        }

        public void AddQuestion(Question question, IEnumerable<string> tags)
        {
            using (var ctx = new DBContext(_connectionString))
            {
                ctx.Questions.Add(question);
                foreach (string tag in tags)
                {
                    Tag t = GetTag(tag);
                    int tagId;
                    if (t == null)
                    {
                        tagId = AddTag(tag);
                    }
                    else
                    {
                        tagId = t.Id;
                    }
                    ctx.QuestionsTags.Add(new QuestionsTags
                    {
                        QuestionId = question.Id,
                        TagId = tagId
                    });
                }

                ctx.SaveChanges();
            }
        }
    }
}
