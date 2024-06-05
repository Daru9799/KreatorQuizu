using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KreatorQuizu.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Id_q { get; set; }
        public string Answer_A { get; set;}
        public string Answer_B { get; set;}
        public string Answer_C { get; set;}
        public string Answer_D { get; set;}
        public string Result { get; set;}

        public Question(int id, string title, int id_q, string answer_A, string answer_B, string answer_C, string answer_D, string result) 
        { 
            this.Id = id;
            this.Title = title;
            this.Id_q = id_q;
            this.Answer_A = answer_A;
            this.Answer_B = answer_B;
            this.Answer_C = answer_C;
            this.Answer_D = answer_D;
            this.Result = result;
        }
        public string GetQuestion()
        {
            return this.Id.ToString() + " " + this.Title + " " + this.Id_q.ToString() + " " + this.Answer_A + " " + this.Answer_B + " " +  this.Answer_C + " " + this.Answer_D + " " + this.Result; 
        }
    }
}
