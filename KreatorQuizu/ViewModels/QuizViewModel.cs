using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using KreatorQuizu.Models;
using KreatorQuizu.ViewModels.BaseClass;

namespace KreatorQuizu.ViewModels
{
    class QuizViewModel : ViewModelBase
    {
        private int quizId;
        //Pierwszy arg -> nazwa, Drugi arg -> kategoria
        private string? quizName { get; set; }
        private string? quizCategory { get; set; }
        public string? QuizName
        {
            get => quizName;
            set
            {
                if (quizName != value)
                {
                    quizName = value;
                    OnPropertyChanged(nameof(QuizName));
                }
            }
        }
        public string? QuizCategory
        {
            get => quizCategory;
            set
            {
                if (quizCategory != value)
                {
                    quizCategory = value;
                    OnPropertyChanged(nameof(QuizCategory));
                }
            }
        }
        public QuizViewModel() //Konstruktor przy tworzeniu quizu
        {
            this.buttonText = "Create new quiz";
        }
        public QuizViewModel(Models.Quiz q) //Konstruktor przy edycji quizu
        {
            this.quizId = q.Id;
            this.quizName = q.Name;
            this.quizCategory = q.Category;
            this.buttonText = "Edit quiz";
        }
        private string? buttonText { get; set; }
        public string? ButtonText
        {
            get => buttonText;
            set
            {
                if (buttonText != value)
                {
                    buttonText = value;
                    OnPropertyChanged(nameof(ButtonText));
                }
            }
        }

        //Dodawanie quizu do bazy
        private ICommand addClick = null;
        public ICommand AddClick
        {
            get
            {
                if (addClick == null)
                {
                    if(this.buttonText == "Create new quiz")
                    {
                        addClick = new RelayCommand(
                        arg => { Add(QuizName, QuizCategory, arg as Window); },
                        arg => (QuizName != null) && (QuizCategory != null)
                     );
                    }
                    else if (this.buttonText == "Edit quiz")
                    {
                        addClick = new RelayCommand(
                        arg => { Edit(QuizName, QuizCategory, arg as Window); },
                        arg => (QuizName != null) && (QuizCategory != null)
                     );
                    }
                }

                return addClick;
            }
        }

        public void Add(string name, string category, Window currentWindow)
        {
            Quiz quiz1 = new Quiz(0, name, category);
            Data.QuizData.AddQuizToDb(quiz1);
            currentWindow.Close();
        }
        public void Edit(string name, string category, Window currentWindow)
        {
            Data.QuizData.EditQuizDb(this.quizId, name, category);
            currentWindow.Close();
        }
    }
}
