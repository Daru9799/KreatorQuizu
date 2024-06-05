using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KreatorQuizu.Models;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media.TextFormatting;
using KreatorQuizu.ViewModels.BaseClass;

namespace KreatorQuizu.ViewModels
{
    class QuestionViewModel : ViewModelBase
    {
        private string? header { get; set; }
        public string? Header
        {
            get => header;
            set
            {
                if (header != value)
                {
                    header = value;
                    OnPropertyChanged(nameof(Header));
                }
            }
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

        private int quizId;
        private int questionId;
        private string? title { get; set; }
        private string? answerA { get; set; }
        private string? answerB { get; set; }
        private string? answerC { get; set; }
        private string? answerD { get; set; }
        public string? Title
        {
            get => title;
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }
        public string? AnswerA
        {
            get => answerA;
            set
            {
                if (answerA != value)
                {
                    answerA = value;
                    OnPropertyChanged(nameof(AnswerA));
                }
            }
        }
        public string? AnswerB
        {
            get => answerB;
            set
            {
                if (answerB != value)
                {
                    answerB = value;
                    OnPropertyChanged(nameof(AnswerB));
                }
            }
        }
        public string? AnswerC
        {
            get => answerC;
            set
            {
                if (answerC != value)
                {
                    answerC = value;
                    OnPropertyChanged(nameof(AnswerC));
                }
            }
        }
        public string? AnswerD
        {
            get => answerD;
            set
            {
                if (answerD != value)
                {
                    answerD = value;
                    OnPropertyChanged(nameof(AnswerD));
                }
            }
        }
        private bool? isCheckedA { get; set; }
        private bool? isCheckedB { get; set; }
        private bool? isCheckedC { get; set; }
        private bool? isCheckedD { get; set; }
        public bool? IsCheckedA
        {
            get => isCheckedA;
            set
            {
                if (isCheckedA != value)
                {
                    isCheckedA = value;
                    OnPropertyChanged(nameof(IsCheckedA));
                }
            }
        }
        public bool? IsCheckedB
        {
            get => isCheckedB;
            set
            {
                if (isCheckedB != value)
                {
                    isCheckedB = value;
                    OnPropertyChanged(nameof(IsCheckedB));
                }
            }
        }
        public bool? IsCheckedC
        {
            get => isCheckedC;
            set
            {
                if (isCheckedC != value)
                {
                    isCheckedC = value;
                    OnPropertyChanged(nameof(IsCheckedC));
                }
            }
        }
        public bool? IsCheckedD
        {
            get => isCheckedD;
            set
            {
                if (isCheckedD != value)
                {
                    isCheckedD = value;
                    OnPropertyChanged(nameof(IsCheckedD));
                }
            }
        }
        public QuestionViewModel() { }
        public QuestionViewModel(int quiz_id) //Konstruktor przy dodawaniu pytania do bazy
        {
            this.buttonText = "Create new question";
            this.header = "Add Question";
            this.quizId = quiz_id;
            this.isCheckedA = false;
            this.isCheckedB = false;
            this.isCheckedC = false;
            this.isCheckedD = false;
        }
        public QuestionViewModel(int quiz_id, Models.Question q) //Konstruktor przy edycji pytania
        {
            this.buttonText = "Edit question";
            this.header = "Edit Question";
            this.quizId = quiz_id;
            this.questionId = q.Id;
            this.title = q.Title;
            this.AnswerA = q.Answer_A;
            this.AnswerB = q.Answer_B;
            this.AnswerC = q.Answer_C;
            this.AnswerD = q.Answer_D;
            GenerateResult(q.Result); //Zaznacza odpowiednie checkboxy
        }
        //Dodawanie pytania do bazy
        private ICommand addClick = null;
        public ICommand AddClick
        {
            get
            {
                if (addClick == null)
                {
                    if (this.buttonText == "Create new question")
                    {
                        addClick = new RelayCommand(
                        arg => { Add(arg as Window); },
                        arg => ((isCheckedA == true) || (isCheckedB == true) || (isCheckedC == true) || (isCheckedD == true)) && ((title != null) && (answerA != null) && (answerB != null) && (answerC != null) && (answerD != null))
                     );
                    }
                    else if (this.buttonText == "Edit question")
                    {
                        addClick = new RelayCommand(
                        arg => { Edit(arg as Window); },
                        arg => ((isCheckedA == true) || (isCheckedB == true) || (isCheckedC == true) || (isCheckedD == true)) && ((title != null) && (answerA != null) && (answerB != null) && (answerC != null) && (answerD != null))
                     );
                    }

                }
                return addClick;
            }
        }
        private string result {  get; set; }
        private void Result()
        {
            if(isCheckedA == true) {
                this.result += "A;";
            }
            if (isCheckedB == true)
            {
                this.result += "B;";
            }
            if (isCheckedC == true)
            {
                this.result += "C;";
            }
            if (isCheckedD == true)
            {
                this.result += "D;";
            }
            this.result = this.result.Substring(0, this.result.Length - 1); //usuwam dwukropek na koncu zeby format odpowiedzi byl A:B:C 
        }
        private void GenerateResult(string res)
        {
            isCheckedA = false;
            isCheckedB = false;
            isCheckedC = false;
            isCheckedD = false;
            if (res.Contains("A"))
            {
                IsCheckedA = true;
            }
            if (res.Contains("B"))
            {
                IsCheckedB = true;
            }
            if (res.Contains("C"))
            {
                IsCheckedC = true;
            }
            if (res.Contains("D"))
            {
                IsCheckedD = true;
            }
        }

        public void Add(Window currentWindow)
        {
            Result();
            Question question = new Question(0, this.title, this.quizId, this.answerA, this.answerB, this.answerC, this.answerD, this.result);
            Data.QuizData.AddQuestionToDb(question);
            currentWindow.Close();
        }
        public void Edit(Window currentWindow)
        {
            Result();
            Data.QuizData.EditQuestionDb(this.questionId, this.title, this.answerA, this.answerB, this.answerC, this.answerD, this.result);
            currentWindow.Close();
        }
    }
}
