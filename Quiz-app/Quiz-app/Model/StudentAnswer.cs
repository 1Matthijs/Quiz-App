using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_app.Model
{
    internal class StudentAnswer
    {
        public int StudentAnswerId { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public int QuizResultId { get; set; }
        public QuizResult QuizResult { get; set; }
        public string AnswerGiven { get; set; }
        public bool IsCorrect { get; set; }
        public string? Feedback { get; set; } // Feedback van de docent
    }
}
