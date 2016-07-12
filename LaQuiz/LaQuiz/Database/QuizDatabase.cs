using System.Collections.Generic;
using System.Linq;
using LaQuiz.Interfaces;
using LaQuiz.Items;
using SQLite;
using Xamarin.Forms;

namespace LaQuiz.Database
{
    public class QuizDatabase
    {
        static object locker = new object();

        SQLiteConnection sqLiteConnection;

        public QuizDatabase()
        {
            sqLiteConnection = DependencyService.Get<ISQLite>().GetConnectionQuiz();
            // Create the table
            sqLiteConnection.CreateTable<QuizItem>();
        }

        public IEnumerable<QuizItem> GetItems()
        {
            lock (locker)
            {
                return (from i in sqLiteConnection.Table<QuizItem>() select i).ToList();
            }
        }

        /// <summary>
        /// retruns a list of all questions based on the level
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public IEnumerable<QuizItem> GetQuestion(int level)
        {
            lock (locker)
            {
                return sqLiteConnection.Query<QuizItem>($"SELECT * FROM [questions] WHERE level = {level}").ToList();
            }
        }
    }
}
