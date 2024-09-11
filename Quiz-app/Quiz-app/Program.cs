using Quiz_app.Data;
using Quiz_app.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_app
{
    class Program
    {
       
        static void Main(string[] args)
        {
            bool program = true;
            using (QuizContext context = new QuizContext())
            {
               

                while (program)
                {
                    Console.WriteLine("Welkom bij de StudentQuizApp!");
                    Console.WriteLine("1. Inloggen");
                    Console.WriteLine("2. Registreren");
                    string choice = Console.ReadLine();

                    if (choice == "1")
                    {
                        User user = Login(context);
                        if (user != null)
                        {
                            if (user.IsTeacher)
                            {
                                Console.WriteLine("login as teacher succes");
                                TeacherDashBoard(user, context);
                            }
                            else
                            {
                                Console.WriteLine("login as Student succes");
                                StudentDashboard(user, context);

                            }
                        }
                    }
                    else if (choice == "2")
                    {
                        Register(context);
                    }
                    else
                    {
                        program = false; 
                    }
                }
            }
        }

        static User Login(QuizContext context)
        {
            bool login = true;
            while (login)
            {
                Console.Write("Gebruikersnaam: ");
                string username = Console.ReadLine();
                Console.Write("Wachtwoord: ");
                string password = Console.ReadLine();

                User user = context.Users.SingleOrDefault(u => u.Username == username && u.Password == password);
                if (user != null)
                {
                    Console.WriteLine("Inloggen succesvol!");
                    login = false;
                    return user;
                }

                Console.WriteLine("Ongeldige inloggegevens.");
                
            }
            login = false;
            return null;
        }

        static void Register(QuizContext context)
        {
            bool register = true;
            string username = "";
            string password = "";

            while (register)
            {
                Console.Write("Gebruikersnaam: ");
                username = Console.ReadLine();
                Console.Write("Wachtwoord: ");
                password = Console.ReadLine();

                if(password == "" || username == "") {
                    Console.WriteLine("geen geldige input");
                }
                else
                {
                    register = false;
                    Console.WriteLine(password, username);
                   
                }                                   
            }


            User user = new User
            {
                Username = username,
                Password = password,
                IsTeacher = false
            };

            context.Users.Add(user);
            context.SaveChanges();
            Console.WriteLine("Registratie succesvol! Je kunt nu inloggen.");


        }
        static void TeacherDashBoard(User user, QuizContext context)
        {
            Console.WriteLine("welkom op het teacher dashboard");
            Console.WriteLine("[1]  upload vragen");
            Console.WriteLine("[2] geef feedback");
            string choice = Console.ReadLine();
            if(choice == "1")
            {
                Quiz quiz = new Quiz();
                Console.WriteLine("copier het pad naar het csv bestand en plak het hier");
                string filePath = Console.ReadLine();
                quiz.LoadQuestionsFromCSV(filePath, context);
            }
            if(choice == "2")
            {
                GiveFeedbackToStudent(context);
            }
        }
        static void StudentDashboard(User user, QuizContext context)
        {
            Console.WriteLine("\nWelkom student!");
            Console.WriteLine("\nDe quiz gaat beginnen!");

            int correctAnswers = 0;
            QuizResult quizResult = new QuizResult
            {
                UserId = user.UserId,
                CorrectAnswers = 0 
            };
            context.QuizResults.Add(quizResult);
            context.SaveChanges(); 

            foreach (Question question in context.Questions.ToList())
            {
                Console.WriteLine($"\n{question.Text}");
                Console.WriteLine($"A: {question.AnswerA}");
                Console.WriteLine($"B: {question.AnswerB}");
                Console.WriteLine($"C: {question.AnswerC}");
                Console.Write("Uw antwoord (A/B/C): ");

                string answer = Console.ReadLine().ToLower();
                bool isCorrect = answer == question.CorrectAnswer.ToString().ToLower();

               
                StudentAnswer studentAnswer = new StudentAnswer
                {
                    QuestionId = question.Id,
                    QuizResultId = quizResult.QuizResultId,
                    AnswerGiven = answer,
                    IsCorrect = isCorrect,
                    Feedback = null 
                };

                context.StudentAnswers.Add(studentAnswer);

                if (isCorrect)
                {
                    correctAnswers++;
                    Console.WriteLine("Correct!");
                }
                else
                {
                    Console.WriteLine($"Fout! Het juiste antwoord is {question.CorrectAnswer.ToString().ToUpper()}.");
                }
            }

            quizResult.CorrectAnswers = correctAnswers;
            context.SaveChanges();

            Console.WriteLine($"\nJe hebt {correctAnswers} van de {context.Questions.Count()} vragen correct beantwoord.");
        }
        static void GiveFeedbackToStudent(QuizContext context)
        {
            {
                
                var quizResults = context.QuizResults.ToList();
                var users = context.Users.ToList();

                if (!quizResults.Any())
                {
                    Console.WriteLine("Er zijn nog geen quizresultaten.");
                    return;
                }

                Console.WriteLine("Lijst van studenten en hun resultaten:");
                for (int i = 0; i < quizResults.Count; i++)
                {
                    var student = users.SingleOrDefault(u => u.UserId == quizResults[i].UserId);
                    Console.WriteLine($"{i + 1}. Student: {student.Username}, Correcte antwoorden: {quizResults[i].CorrectAnswers}");
                }

              
                Console.Write("\nKies een student om feedback te geven (voer het nummer in): ");
                if (int.TryParse(Console.ReadLine(), out int studentIndex) && studentIndex > 0 && studentIndex <= quizResults.Count)
                {
                    var selectedResult = quizResults[studentIndex - 1];
                    var selectedStudent = users.SingleOrDefault(u => u.UserId == selectedResult.UserId);

                    Console.WriteLine($"\nJe hebt gekozen om feedback te geven aan: {selectedStudent.Username}, Correcte antwoorden: {selectedResult.CorrectAnswers}");

                  
                    var studentAnswers = context.StudentAnswers.Where(a => a.QuizResultId == selectedResult.QuizResultId).ToList();

                    foreach (var answer in studentAnswers)
                    {
                        var question = context.Questions.SingleOrDefault(q => q.Id == answer.QuestionId);
                        Console.WriteLine($"\nVraag: {question.Text}");
                        Console.WriteLine($"Antwoord gegeven: {answer.AnswerGiven.ToUpper()}, Correct: {answer.IsCorrect}");
                        Console.Write("Geef feedback: ");
                        var feedback = Console.ReadLine();

                       
                        answer.Feedback = feedback;
                    }

                    
                    context.SaveChanges();
                    Console.WriteLine("Feedback succesvol opgeslagen.");
                }
                else
                {
                    Console.WriteLine("Ongeldige keuze. Probeer het opnieuw.");
                }
            }
        }

    }

}

