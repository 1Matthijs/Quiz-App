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
            string choice = Console.ReadLine();
            if(choice == "1")
            {
                Quiz quiz = new Quiz();
                Console.WriteLine("copier het pad naar het csv bestand en plak het hier");
                string filePath = Console.ReadLine();
                quiz.LoadQuestionsFromCSV(filePath, context);
            }
        }
    }
}

