using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KreatorQuizu.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public Quiz(int id, string name, string category)
        {
            this.Id = id;
            this.Name = name;
            this.Category = category;
        }
        public string GetQuiz()
        {
            return this.Id.ToString() + " " + this.Name + " " + this.Category;
        }
    }
}
