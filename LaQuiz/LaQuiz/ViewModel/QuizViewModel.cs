using System;
using System.Collections.ObjectModel;
using LaQuiz.Items;

namespace LaQuiz.ViewModel
{
    public class QuizViewModel : ViewModelBase
    {
        private int level;
        private int score;
        private QuizItem thisQuestion;
        private ObservableCollection<QuizItem> QuizCollection;
        public SpielerItem thisPlayer;

        public QuizViewModel(SpielerItem player)
        {
            thisPlayer = player;
            level = 1;
            score = 0;
            SetQuestionToLvl();
        }
        /// <summary>
        /// Setze thisQuestion als neue Frage mit angegebenem Level
        /// </summary>
        private void SetQuestionToLvl()
        {
            if (level == 16)
            {
                //Gewonnen
                return;
            }
            QuizCollection = new ObservableCollection<QuizItem>(App.Database.GetQuestion(level));
            Random rnd = new Random();
            int index = rnd.Next(0, QuizCollection.Count - 1);
            thisQuestion = QuizCollection[index];
        }
        /// <summary>
        /// Erhöhe Level und Score und lade neue Frage
        /// </summary>
        public void RightGuess()
        {
            level++;
            score++;
            if (level == 16)
            {
                //Gewonnen
                return;
            }
            SetQuestionToLvl();
        }
        /// <summary>
        /// Gibt an welcher string den höheren score hat 1. oder 2.
        /// </summary>
        public bool IsNewHigh(string sNeu)
        {
            int alt = Array.IndexOf(ScoreTable, thisPlayer.Score);
            int neu = Array.IndexOf(ScoreTable, sNeu);
            if (neu > alt)
                return true;
            return false;
        }
#region Eigenschaften
        private string[] ScoreTable =
        {
            "0€", "50€", "100€", "200€", "300€", "500€", "1.000€", "2.000€", "4.000€",
            "8.000€", "16.000€", "32.000€", "64.000€", "125.000€", "500.000€", "1.000.000€"
        };
        public string qBody
        {
            get { return thisQuestion.body; }            
        }
        public string qA => thisQuestion.a;

        public string qB
        {
            get { return thisQuestion.b; }
        }
        public string qC
        {
            get { return thisQuestion.c; }
        }
        public string qD
        {
            get { return thisQuestion.d; }
        }
        public string Correct
        {
            get
            {
                return thisQuestion.correct.ToString();           
            }
        }
        public string playername => thisPlayer.SpielerName;

        public string Score
        {
            get { return ScoreTable[score]; }
        }

        public string Spielername
        {
            get { return thisPlayer.SpielerName; }
        }
        public string Level => $"Level: {level} von 15";
        public string Highscore { get { return thisPlayer.Score; } set { thisPlayer.Score = value; } }
    }
#endregion
}
