using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using LaQuiz.Interfaces;
using LaQuiz.WinPhone.Services;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_WinPhone))]

namespace LaQuiz.WinPhone.Services
{
    class SQLite_WinPhone : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "SpielerInfo.db3";
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);
            var conn = new SQLiteConnection(path);
            return conn;
        }

        public SQLiteConnection GetConnectionQuiz()
        {
            var sqliteFilename = "german.db";
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);
            var conn = new SQLiteConnection(path);
            return conn;
        }
    }
}
