using System.Collections.Generic;
using System.Linq;
using LaQuiz.Interfaces;
using LaQuiz.Items;
using SQLite;
using Xamarin.Forms;

namespace LaQuiz.Database
{
    public class SpielerDatabase
    {
        static object locker = new object();
        private readonly SQLiteConnection _sqLiteConnection;

        public SpielerDatabase()
        {
            _sqLiteConnection = DependencyService.Get<ISQLite>().GetConnection();
            _sqLiteConnection.CreateTable<SpielerItem>();
        }

        public void AddToDb(string name, string score)
        {
            //ADD
            _sqLiteConnection.Insert(new SpielerItem { SpielerName = name, Score = score + "€" });
        }
        /// <summary>
        /// / Delets all Spieler
        /// </summary>

        public void DeleteFromDb()
        {
            //Delete
            _sqLiteConnection.DeleteAll<SpielerItem>();
        }

        public void DeletSpieler(string spielerName)
        {
            _sqLiteConnection.Delete(new SpielerItem {SpielerName = spielerName});
        }
        public void Update(string name, string score)
        {
            //Update
            _sqLiteConnection.Update(new SpielerItem { SpielerName = name, Score = score });
        }

        public IEnumerable<SpielerItem> GetSpieler()
        {
            lock (locker)
            {
                //Get all Spieler
                return _sqLiteConnection.Table<SpielerItem>().ToList();
            }

        }

        public IEnumerable<SpielerItem> sort_start_with_highest()
        {
            //Todo look up sort methode for LINQ
            return _sqLiteConnection.Query<SpielerItem>("SELECT * FROM [questions] GROUPEBY [SCORE] SORT = HIGHEST");
        }

        public void Sort_by_last_used()
        {
            //Todo impllement sort function   
        }
    }
}
