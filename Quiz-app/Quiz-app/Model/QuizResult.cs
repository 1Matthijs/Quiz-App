using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_app.Model
{
    internal class QuizResult
    {
        public int QuizResultId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int CorrectAnswers { get; set; }
        public string Feedback { get; set; }
    }
}
