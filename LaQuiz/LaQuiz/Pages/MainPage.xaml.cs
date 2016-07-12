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
        private readonly ObservableCollection<SpielerItem> SpielerCollection = new ObservableCollection<SpielerItem>(App.SpielerDatabase.GetSpieler());

        public MainPage()
        {
            InitializeComponent();

            //ListView mit Spielern füllen
            BenutzerListView.ItemsSource = SpielerCollection;

            //Btn Events
            DeleteBtn.Clicked += (sender, args) => DeleteClicked();
            addBtn.Clicked += (sender, args) => AddBtnClicked();

        }
 
        /// <summary>
        /// Bei Auswahl eines Spielers das Spiel starten
        /// </summary>
        public async void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            var spieler = (SpielerItem)e.SelectedItem;

            var answer = await DisplayAlert("Start", "Hallo " + spieler.SpielerName + "!\nBist du bereit?", "Ja", "Nein");
            if (answer)
                await Navigation.PushModalAsync(new GamePage(new QuizViewModel(spieler)));
            else
                BenutzerListView.SelectedItem = null;

        }


        private void OnDelete(object sender, EventArgs e)
        {

            //var item = (MenuItem)sender;
            SpielerItem spieler = (SpielerItem)BenutzerListView.SelectedItem;
            var db = new SpielerDatabase();
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

        public void DeleteClicked()
        {
            var dba = DependencyService.Get<ISQLite>().GetConnection();
            var db = new SpielerDatabase();

            //Nuter löschen
            db.DeleteFromDb();

            //ListeView aktuellisieren
            BenutzerListView.ItemsSource = dba.Table<SpielerItem>();
        }

        #endregion
        public void StartGameNavigation()
        {
            if (BenutzerListView.SelectedItem == null)
            {
                DisplayAlert("Ups", "Bitte Spieler auswählen", "OK");
                return;
            }

            //    SpielerItem spieler = (SpielerItem)BenutzerListView.SelectedItem;  
            //    DisplayAlert("Spieler gewählt", spieler.SpielerName + spieler.Score, "OK"); // Nur Imp check ob es richtig funktioniert später löschen?  
            //    // Jetzt init Page mit View Model mit PLayer ITem  
            //    Navigation.PushModalAsync(new GamePage(new QuizViewModel(spieler)));  
        }

        private void MainPage_OnAppearing(object sender, EventArgs e)
        {
            //ListeView aktualisieren  
            var dba = DependencyService.Get<ISQLite>().GetConnection();
            BenutzerListView.ItemsSource = dba.Table<SpielerItem>();
        }
    }
}
