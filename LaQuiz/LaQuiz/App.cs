using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LaQuiz.Database;
using LaQuiz.Pages;
using Xamarin.Forms;

namespace LaQuiz
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            MainPage = new MainPage();
        }

        //Singelton
        private static QuizDatabase database;
        public static QuizDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new QuizDatabase();
                }
                return database;
            }
        }

        //Singelton
        private static SpielerDatabase spielerDatabase;

        public static SpielerDatabase SpielerDatabase
        {
            get
            {
                if (spielerDatabase == null)
                {
                    spielerDatabase = new SpielerDatabase();
                }
                return spielerDatabase;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
