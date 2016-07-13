using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaQuiz.Interfaces;
using LaQuiz.Items;
using LaQuiz.ViewModel;
using Xamarin.Forms;
using LaQuiz.Database;

namespace LaQuiz.Pages
{
    public partial class MainPage : ContentPage
    {
        private readonly ObservableCollection<SpielerItem> SpielerCollection =
            new ObservableCollection<SpielerItem>(App.SpielerDatabase.GetSpieler());

        public MainPage()
        {
            InitializeComponent();

            //ListView mit Spielern füllen
            BenutzerListView.ItemsSource = SpielerCollection;

            //Btn Events
            DeleteBtn.Clicked += (sender, args) => DeleteClicked();
            addBtn.Clicked += (sender, args) => AddBtnClicked();
            NameEntry.Completed += (sender, args) => AddBtnClicked();
        }

        /// <summary>
        /// Bei Auswahl eines Spielers das Spiel starten
        /// </summary>
        public async void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            //safe cast checks is e is null
            var spieler = e.SelectedItem as SpielerItem;

            var answer =
                await DisplayAlert("Start", "Hallo " + spieler.SpielerName + "!\nBist du bereit?", "Ja", "Nein");
            if (answer)
                await Navigation.PushModalAsync(new GamePage(new QuizViewModel(spieler)));
            else
                BenutzerListView.SelectedItem = null;
        }


        private void OnDelete(object sender, EventArgs e)
        {

            //safe cast
            var item = ((MenuItem)sender);
            var sp = item.CommandParameter as SpielerItem;
            var db = new SpielerDatabase();
            db.DeletSpieler(sp.SpielerName);
            var dba = DependencyService.Get<ISQLite>().GetConnection();
            BenutzerListView.ItemsSource = dba.Table<SpielerItem>();

        }


        #region Button Events

        public void AddBtnClicked()
        {
            var dba = DependencyService.Get<ISQLite>().GetConnection();
            var db = new SpielerDatabase();
            if (NameEntry.Text != "")
            {
                //Versuche Nutzer an zu legen
                try
                {
                    db.AddToDb(NameEntry.Text, "0");
                }
                catch (Exception e)
                {
                    DisplayAlert("Ups", "Der Name ist schon vergeben", "Ok");
                }

                NameEntry.Text = "";
            }
            else
            {
                DisplayAlert("Ups", "Bitte Namen eintragen", "OK");
            }

            //ListeView aktuellisieren
            BenutzerListView.ItemsSource = dba.Table<SpielerItem>();
        }

        public async void DeleteClicked()
        {
            var answer =
                await DisplayAlert("Löschen", " Achtung!\n Möchtest du wirklich\n alle Spieler löschen?", "Ja", "Nein");
            if (answer)
            {
                var dba = DependencyService.Get<ISQLite>().GetConnection();
                var db = new SpielerDatabase();

                //Nuter löschen
                db.DeleteFromDb();

                //ListeView aktuellisieren
                BenutzerListView.ItemsSource = dba.Table<SpielerItem>();
            }
        }

        #endregion

        public void StartGameNavigation()
        {
            if (BenutzerListView.SelectedItem == null)
            {
                DisplayAlert("Ups", "Bitte Spieler auswählen", "OK");
                return;
            }
        }

        private void MainPage_OnAppearing(object sender, EventArgs e)
        {
            //ListeView aktualisieren  
            var dba = DependencyService.Get<ISQLite>().GetConnection();
            BenutzerListView.ItemsSource = dba.Table<SpielerItem>();
        }
    }
}