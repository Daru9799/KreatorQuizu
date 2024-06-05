using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KreatorQuizu.Models;

namespace KreatorQuizu.Data
{
    //W tej klasie są metody które dodają i zwracają rzeczy z bazy
    public static class QuizData
    {
        //QUIZY

        //Dodawanie quizu do bazy
        public static void AddQuizToDb(Quiz quiz)
        {
            using (var db = new QuizContext())
            {
                db.Add(quiz);
                db.SaveChanges();
            }
        }
        //Usuwanie quizu z bazy
        public static void DeleteQuizFromDb(int id)
        {
            using (var db = new QuizContext())
            {
                var quizToDelete = db.Quizes.FirstOrDefault(q => q.Id == id);
                var questionsToDelete = db.Questions.Where(q => q.Id_q == id);
                if (quizToDelete != null)
                {
                    db.Remove(quizToDelete);
                    db.RemoveRange(questionsToDelete);
                    db.SaveChanges();
                }
            }
        }
        //Edycja quizu o zadanym id
        public static void EditQuizDb(int id, string name, string category)
        {
            using (var db = new QuizContext())
            {
                var quizToUpdate = db.Quizes.FirstOrDefault(q => q.Id == id);
                if (quizToUpdate != null)
                {
                    quizToUpdate.Name = name;
                    quizToUpdate.Category = category;
                    db.SaveChanges();
                }
            }
        }
        //Wyciagniecie wszystkich quizow z bazy
        public static List<Quiz> GetQuizes()
        {
            //zwraca wszystko z quizes (po nazwie obiektu mozna robic inne rzeczy typu order by albo where)
            using (var db = new QuizContext())
            {
                return db.Quizes.ToList(); 
            }
        }

        //PYTANIA

        //Dodanie pytania do bazy
        public static void AddQuestionToDb(Question question)
        {
            using (var db = new QuizContext())
            {
                db.Add(question);
                db.SaveChanges();
            }
        }
        //Edycja pytania o zadanym id
        public static void EditQuestionDb(int id, string title, string answer_A, string answer_B, string answer_C, string answer_D, string result)
        {
            using (var db = new QuizContext())
            {
                var questionToUpdate = db.Questions.FirstOrDefault(q => q.Id == id);
                if (questionToUpdate != null)
                {
                    questionToUpdate.Title = title;
                    questionToUpdate.Answer_A = answer_A;
                    questionToUpdate.Answer_B = answer_B;
                    questionToUpdate.Answer_C = answer_C;
                    questionToUpdate.Answer_D = answer_D;
                    questionToUpdate.Result = result;
                    db.SaveChanges();
                }
            }
        }
        //Wyciagniecie wszystkich pytan z bazy (dla danego quizu po jego id)
        public static List<Question> GetQuestions(int id)
        {
            using (var db = new QuizContext())
            {
                return db.Questions.Where(q => q.Id_q == id).ToList();
            }
        }

        //Usuwanie quizu z bazy
        public static void DeleteQuestionFromDb(int id)
        {
            using (var db = new QuizContext())
            {
                var questionToDelete = db.Questions.FirstOrDefault(q => q.Id == id);
                if (questionToDelete != null)
                {
                    db.Remove(questionToDelete);
                    db.SaveChanges();
                }
            }
        }

    }
}
