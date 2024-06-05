using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using KreatorQuizu.Data;
using KreatorQuizu.Models;
using KreatorQuizu.ViewModels.BaseClass;


namespace KreatorQuizu.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        //Aktualizacja bazy
        private ObservableCollection<Models.Quiz> quizes = new ObservableCollection<Quiz>(QuizData.GetQuizes());
        public ObservableCollection<Models.Quiz> QuizItems
        {
            get { return quizes; }
            set
            {
                quizes = value;
                OnPropertyChanged(nameof(QuizItems));
            }
        }

        //Dodawanie Quizu
        private ICommand addQuizCommand = null;
        public ICommand AddQuizCommand
        {
            get
            {
                return addQuizCommand ?? (addQuizCommand = new RelayCommand(arg => AddQuiz(), null));
            }
        }

        private void AddQuiz()
        {
            var addQuizWindow = new AddQuizWindow();
            addQuizWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            addQuizWindow.ShowDialog();
            UpdateQuizItems();
        }

        private void UpdateQuizItems()
        {
            ObservableCollection<Models.Quiz> newQuizes = new ObservableCollection<Quiz>(QuizData.GetQuizes());
            QuizItems = newQuizes;
        }

        //Sledzenie aktualnie wybranego quizu z DataGridu
        private Models.Quiz selectedQuiz;
        public Models.Quiz SelectedQuiz
        {
            get { return selectedQuiz; }
            set
            {
                selectedQuiz = value;
                OnPropertyChanged(nameof(SelectedQuiz));
            }
        }

        //Usuwanie quizu
        private ICommand deleteQuizCommand = null;
        public ICommand DeleteQuizCommand
        {
            get
            {
                return deleteQuizCommand ?? (deleteQuizCommand = new RelayCommand(arg => DeleteQuiz(selectedQuiz), arg => selectedQuiz != null));
            }
        }
        private void DeleteQuiz(Models.Quiz selectedQuiz)
        {
            int id = selectedQuiz.Id;
            Data.QuizData.DeleteQuizFromDb(id);
            UpdateQuizItems();

        }
        //Modyfikacja Quizu (jego nazwy i kategorii)
        private ICommand editQuizCommand = null;
        public ICommand EditQuizCommand
        {
            get
            {
                return editQuizCommand ?? (editQuizCommand = new RelayCommand(arg => EditQuiz(), arg => selectedQuiz != null));
            }
        }

        private void EditQuiz()
        {
            Quiz q = selectedQuiz;
            //przekazanie obiektu quizu
            var editQuizViewModel = new QuizViewModel(q);

            var editQuizWindow = new AddQuizWindow();
            editQuizWindow.DataContext = editQuizViewModel;

            editQuizWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            editQuizWindow.ShowDialog();
            UpdateQuizItems();
        }

        //Modyfikacja pytań
        private ICommand modifyQuestionsCommand = null;
        public ICommand ModifyQuestionsCommand
        {
            get
            {
                return modifyQuestionsCommand ?? (modifyQuestionsCommand = new RelayCommand(arg => ModifyQuestions(), arg => selectedQuiz != null));
            }
        }
        private void ModifyQuestions()
        {
            //Zmiana widocznosci gridow::
            MenuVisibility = Visibility.Collapsed;
            QuestionsVisibility = Visibility.Visible;
            QuestionItems = new ObservableCollection<Question>(QuizData.GetQuestions(selectedQuiz.Id));
        }

        private Visibility menuVisibility = Visibility.Visible;
        public Visibility MenuVisibility
        {
            get { return menuVisibility; }
            set
            {
                menuVisibility = value;
                OnPropertyChanged(nameof(MenuVisibility));
            }
        }

        private Visibility questionsVisibility = Visibility.Collapsed;
        public Visibility QuestionsVisibility
        {
            get { return questionsVisibility; }
            set
            {
                questionsVisibility = value;
                OnPropertyChanged(nameof(QuestionsVisibility));
            }
        }

        public MainViewModel()
        {
            MenuVisibility = Visibility.Visible;
            QuestionsVisibility = Visibility.Collapsed;
        }


        //--------------------------------------------------------
        //Okno z pytaniami dla danego quizu

        private ObservableCollection<Models.Question> questions;

        //Aktualizacja bazy
        public ObservableCollection<Models.Question> QuestionItems
        {
            get { return questions; }
            set
            {
                questions = value;
                OnPropertyChanged(nameof(QuestionItems));
            }
        }
        //Sledzenie aktualnie wybranego pytania z DataGridu
        private Models.Question selectedQuestion;
        public Models.Question SelectedQuestion
        {
            get { return selectedQuestion; }
            set
            {
                selectedQuestion = value;
                OnPropertyChanged(nameof(SelectedQuestion));
            }
        }
        //Powrot do menu glownego
        private ICommand returnCommand = null;
        public ICommand ReturnCommand
        {
            get
            {
                if (returnCommand == null)
                {
                    return returnCommand ?? (returnCommand = new RelayCommand(arg => ReturnToQuizes(), null));
                }

                return returnCommand;
            }
        }
        private void ReturnToQuizes()
        {
            MenuVisibility = Visibility.Visible;
            QuestionsVisibility = Visibility.Collapsed;
        }

        private ICommand deleteQuestionCommand = null;
        public ICommand DeleteQuestionCommand
        {
            get
            {
                if (deleteQuestionCommand == null)
                {
                    return deleteQuestionCommand ?? (deleteQuestionCommand = new RelayCommand(arg => DeleteQuestion(), arg => selectedQuestion != null));
                }

                return deleteQuestionCommand;
            }
        }
        private void DeleteQuestion()
        {
            int id = selectedQuestion.Id;
            Data.QuizData.DeleteQuestionFromDb(id);
            UpdateQuestionItems();
        }
        private void UpdateQuestionItems()
        {
            QuestionItems = new ObservableCollection<Question>(QuizData.GetQuestions(selectedQuiz.Id));
        }

        //Dodawanie pytan
        private ICommand addQuestionCommand = null;
        public ICommand AddQuestionCommand
        {
            get
            {
                return addQuestionCommand ?? (addQuestionCommand = new RelayCommand(arg => AddQuestion(), null));
            }
        }

        private void AddQuestion()
        {
            var addQuestionViewModel = new QuestionViewModel(selectedQuiz.Id);

            var addQuestionWindow = new AddQuestionWindow();
            addQuestionWindow.DataContext = addQuestionViewModel;
            addQuestionWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            addQuestionWindow.ShowDialog();
            UpdateQuestionItems();
        }
        //Edycja pytan
        private ICommand editQuestionCommand = null;
        public ICommand EditQuestionCommand
        {
            get
            {
                return editQuestionCommand ?? (editQuestionCommand = new RelayCommand(arg => EditQuestion(), arg => selectedQuestion != null));
            }
        }
        private void EditQuestion()
        {
            Question q = selectedQuestion;
            var editQuestionViewModel = new QuestionViewModel(selectedQuiz.Id, q);

            var editQuestionWindow = new AddQuestionWindow();
            editQuestionWindow.DataContext = editQuestionViewModel;
            editQuestionWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            editQuestionWindow.ShowDialog();
            UpdateQuestionItems();
        }
    }
}
