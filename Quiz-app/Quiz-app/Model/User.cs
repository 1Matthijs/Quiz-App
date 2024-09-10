using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_app.Model
{
    internal class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsTeacher { get; set; }
        public ICollection<QuizResult> QuizResults { get; set; }
    }
}
