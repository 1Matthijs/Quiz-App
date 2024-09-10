using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz_app.Model;
using Microsoft.EntityFrameworkCore;

namespace Quiz_app.Data
{
    internal class QuizContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuizResult> QuizResults { get; set; }
        public DbSet<StudentAnswer> StudentAnswers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(
                "server=localhost;" +                     // Server name
                "port=3306;" +                            // Server port
                "user=root;" +                            // Username
                "password=;" +                            // Password
                "database=Quiz"                           // Database name
                , Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.21-mysql") // Version
                );

            }
        }
    }
}
