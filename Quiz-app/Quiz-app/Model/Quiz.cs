using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz_app.Data;

namespace Quiz_app.Model
{
    internal class Quiz
    {
        public List<Question> Questions { get; set; } = new List<Question>();

        public void LoadQuestionsFromCSV(string filePath, QuizContext context)
        {
            try
            {
                var lines = File.ReadAllLines(filePath);

                foreach (var line in lines.Skip(1)) 
                {
                    var data = line.Split(';');
                    if (data.Length == 6)
                    {
                        string questionText = data[1];

                      
                        var existingQuestion = context.Questions
                            .FirstOrDefault(q => q.Text == questionText);

                        if (existingQuestion == null)
                        {
                            var question = new Question
                            {
                                Text = questionText,
                                AnswerA = data[2],
                                AnswerB = data[3],
                                AnswerC = data[4],
                                CorrectAnswer = data[5].ToLower()[0]
                            };

                            context.Questions.Add(question);  
                        }
                        else
                        {
                            Console.WriteLine($"Vraag al in database: '{questionText}' - overslaan.");
                        }
                    }
                }

                context.SaveChanges();  
                Console.WriteLine("Vragen succesvol ingeladen en opgeslagen in de database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fout bij het inladen van vragen: {ex.Message}");
            }
        }


    }
}
