using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyStackOverflow.Data;

namespace MyStackOverflow.Data
{
    public class AuthorizationRepository
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool PasswordMatch(string userInput, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(userInput, passwordHash);
        }

        private readonly string _connectionString;

        public AuthorizationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }


        public User GetByEmail(string email)
        {
            using (var ctx = new DBContext(_connectionString))
            {
                return ctx.Users.FirstOrDefault(u => u.UserName == email);
            }
        }

        public User Login(string email, string password)
        {
            var user = GetByEmail(email);
            if (user == null)
            {
                return null;
            }

            bool isCorrectPassword = PasswordMatch(password, user.PasswordHash);
            if (isCorrectPassword)
            {
                return user;
            }

            return null;
        }

        public int AddUser(User user, string password)
        {
            string passwordHash = HashPassword(password);

            using (var ctx = new DBContext(_connectionString))
            {
                var u = new User { UserName = user.UserName, PasswordHash = passwordHash, Likes = user.Likes };
                ctx.Users.Add(user);
                ctx.SaveChanges();
                return u.Id;
            }
        }

    }
}


