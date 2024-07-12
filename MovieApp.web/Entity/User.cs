using System;

namespace MovieApp.web.Entity
{
    public class User
    {
        public int userId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Person Person { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Role { get; set; }
    }
}
