using System;
using System.IO;
using LaQuiz.iOS.Services;
using LaQuiz.Interfaces;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(SQLite_IOS))]
namespace LaQuiz.iOS.Services
{
    class SQLite_IOS : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "SpielerInfo.db3";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, sqliteFilename);
            // Create the connection
            var conn = new SQLite.SQLiteConnection(path);
            // Return the database connection
            return conn;
        }

        public SQLiteConnection GetConnectionQuiz()
        {
            var sqliteFilename = "german.db";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, sqliteFilename);
            // Create the connection

            // This is where we copy in the prepopulated database
            Console.WriteLine(path);
            if (!File.Exists(path))
            {
                File.Copy(sqliteFilename, path);
            }

            var conn = new SQLite.SQLiteConnection(path);

            // Return the database connection 
            return conn;
        }
    }
}
