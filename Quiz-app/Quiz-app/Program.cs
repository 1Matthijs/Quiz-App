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
            Console.WriteLine("[2]  geef feedback");
            Console.WriteLine("[3]  Verwijder Vragen");
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
            if(choice == "3")
            {
                DeleteQuestion(context);
            }
            return;
        }
        static void StudentDashboard(User user, QuizContext context)
        {
            Console.WriteLine("\nWelkom student!");
            Console.WriteLine("[1]  Maak quiz");
            Console.WriteLine("[2]  zie foute antwoorden");
            string choice = Console.ReadLine();
            if (choice == "1") {
                MakeQuiz(user, context);
            }
            if (choice == "2") {
                ShowIncorrectAnswersForStudent(user, context);

            }

        }

            static void MakeQuiz(User user, QuizContext context)
        {
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
                
                List<QuizResult> quizResults = context.QuizResults.ToList();
                List<User> users = context.Users.ToList();

                if (!quizResults.Any())
                {
                    Console.WriteLine("Er zijn nog geen quizresultaten.");
                    return;
                }

                Console.WriteLine("Lijst van studenten en hun resultaten:");
                for (int i = 0; i < quizResults.Count; i++)
                {
                    User student = users.SingleOrDefault(u => u.UserId == quizResults[i].UserId);
                    Console.WriteLine($"{i + 1}. Student: {student.Username}, Correcte antwoorden: {quizResults[i].CorrectAnswers}");
                }

              
                Console.Write("\nKies een student om feedback te geven (voer het nummer in): ");
                if (int.TryParse(Console.ReadLine(), out int studentIndex) && studentIndex > 0 && studentIndex <= quizResults.Count)
                {
                    QuizResult selectedResult = quizResults[studentIndex - 1];
                    User selectedStudent = users.SingleOrDefault(u => u.UserId == selectedResult.UserId);

                    Console.WriteLine($"\nJe hebt gekozen om feedback te geven aan: {selectedStudent.Username}, Correcte antwoorden: {selectedResult.CorrectAnswers}");

                  
                    List<StudentAnswer> studentAnswers = context.StudentAnswers.Where(a => a.QuizResultId == selectedResult.QuizResultId).ToList();

                    foreach (StudentAnswer answer in studentAnswers)
                    {
                        Question question = context.Questions.SingleOrDefault(q => q.Id == answer.QuestionId);
                        Console.WriteLine($"\nVraag: {question.Text}");
                        Console.WriteLine($"Antwoord gegeven: {answer.AnswerGiven.ToUpper()}, Correct: {answer.IsCorrect}");
                        Console.Write("Geef feedback: ");
                        string feedback = Console.ReadLine();

                       
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
        static void DeleteQuestion(QuizContext context)
        {
            {
                
                List<Question> questions = context.Questions.ToList();

                if (!questions.Any())
                {
                    Console.WriteLine("Er zijn geen vragen om te verwijderen.");
                    return;
                }

                Console.WriteLine("Lijst van vragen:");
                for (int i = 0; i < questions.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. Vraag: {questions[i].Text}");
                }

             
                Console.Write("\nKies een vraag om te verwijderen (voer het nummer in): ");
                if (int.TryParse(Console.ReadLine(), out int questionIndex) && questionIndex > 0 && questionIndex <= questions.Count)
                {
                    Question selectedQuestion = questions[questionIndex - 1];

                  
                    Console.WriteLine($"\nWeet je zeker dat je de vraag wilt verwijderen: '{selectedQuestion.Text}'? (y/n)");
                    string confirmation = Console.ReadLine();

                    if (confirmation.ToLower() == "y")
                    {
                       
                        context.Questions.Remove(selectedQuestion);
                        context.SaveChanges();

                        Console.WriteLine("Vraag succesvol verwijderd.");
                    }
                    else
                    {
                        Console.WriteLine("Verwijderen geannuleerd.");
                    }
                }
                else
                {
                    Console.WriteLine("Ongeldige keuze. Probeer het opnieuw.");
                }
            }
        }
        static void ShowIncorrectAnswersForStudent(User user, QuizContext context)
        {
            {

                QuizResult quizResult = context.QuizResults
                                        .Where(qr => qr.UserId == user.UserId)
                                        .FirstOrDefault();

                if (quizResult == null)
                {
                    Console.WriteLine("Geen quizresultaten gevonden voor deze student.");
                    return;
                }

             
                List<StudentAnswer> studentAnswers = new List<StudentAnswer>();

                foreach (StudentAnswer studentAnswer in context.StudentAnswers)
                {
                    if (studentAnswer.QuizResultId == quizResult.QuizResultId)
                    {
                        studentAnswers.Add(studentAnswer);
                    }
                }

                if (studentAnswers.Count == 0)
                {
                    Console.WriteLine("Geen antwoorden gevonden voor deze quiz.");
                    return;
                }

                Console.WriteLine($"\nResultaten voor student: {user.Username}\n");

                foreach (StudentAnswer answer in studentAnswers)
                {
                    Question question = context.Questions.SingleOrDefault(q => q.Id == answer.QuestionId);
                    if (question != null)
                    {
                        Console.WriteLine($"\nVraag: {question.Text}");
                        Console.WriteLine($"Uw antwoord: {answer.AnswerGiven.ToUpper()}");

                        if (!answer.IsCorrect)
                        {
                         
                            Console.WriteLine("Status: FOUT");
                            char correctAnswer = question.CorrectAnswer;
                            Console.WriteLine($"Correct antwoord: {correctAnswer.ToString().ToUpper()}");
                        }
                        else
                        {
                            Console.WriteLine("Status: CORRECT");
                        }
                    }
                }

            
                int correctAnswers = quizResult.CorrectAnswers;
                int totalQuestions = studentAnswers.Count;
                double percentage = ((double)correctAnswers / totalQuestions) * 100;
                Console.WriteLine($"\nU heeft {correctAnswers}/{totalQuestions} correcte antwoorden.");
                Console.WriteLine($"Percentage: {percentage:F2}%");

                
            }
        }

    }

}

