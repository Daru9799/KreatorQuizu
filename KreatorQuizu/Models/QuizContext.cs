using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace KreatorQuizu.Models
{
    public class QuizContext : DbContext
    {
        public DbSet<Quiz> Quizes { get; set; } //przypisanie obiektu
        public DbSet<Question> Questions { get; set; }
        private static string getPathToDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Quizes\\";

        private readonly string projectPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "quiz.db");
        private readonly string targetPath = Path.Combine(getPathToDesktop, "quiz.db");

        public string path = @"quiz.db"; //path to the database
        public QuizContext()
        {
            EnsureDatabaseExists();
        }

        private void EnsureDatabaseExists()
        {
            string targetDirectory = Path.GetDirectoryName(targetPath);
            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }

            if (!File.Exists(targetPath))
            {
                File.Copy(projectPath, targetPath);
            }
        }

        //connection string
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={targetPath}"); 

    }
}
