﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaQuiz.Database;
using LaQuiz.Interfaces;
using LaQuiz.ViewModel;
using Xamarin.Forms;

namespace LaQuiz.Pages
{
    public partial class GamePage : ContentPage
    {
        public bool isTabbed = false;
        public bool time = true;
        private QuizViewModel thisModel;

        public GamePage(QuizViewModel MyQview)
        {
            InitializeComponent();
            thisModel = MyQview;
            BindingContext = thisModel;
            StartTimer();
        }

        public void StartTimer()
        {
            var t = 59;

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                progressBar.ProgressTo(0, 15000, Easing.Linear);
                //progressBar.Animate("SetProgress", (arg) => { progressBar.Progress = arg; }, 0, 1500, Easing.SinIn);
                label.Text = $" Zeit: 00:{t}";

                t -= 1;
                if (t == 6)
                    DependencyService.Get<IAudioService>().PlayCountdown(true);

                if (t != -1 && time) return true;
                if (!time)
                    return false;
                OnTimePassed();
                return false;
            });
        }

        public void TapGestureRecognizer_OnTapped(object sender, EventArgs eventArgs)
        {
            if (isTabbed) return;
            isTabbed = true;
            //Stop Countdown
            time = false;
            DependencyService.Get<IAudioService>().PlayCountdown(false);

            //Stop ProgressBar
            //progressBar.AbortAnimation("SetProgress");

            //Gwt selected Antwort
            var a = (Label)sender;
            var antwort = a.ClassId;

            //Check auf Antwort + Highlighten
            if (antwort == thisModel.Correct)
            {

                //Play sound
                DependencyService.Get<IAudioService>().PlayCorrectSound();

                //erhöt das Level und den Score
                thisModel.RightGuess();

                //on right answer
                ColorMyLabel(antwort, false);
                NextBtn.IsEnabled = true;

                //on win
                if (thisModel.Level == "16")
                    OnWin();
            }

            else
            {
                //Color red color right green
                ColorMyLabel(antwort, true);
                ColorMyLabel(thisModel.Correct, false);
                OnWrongAnswer();
            }
        }

        private void ColorMyLabel(string Who, bool red)
        {
            switch (Who)
            {
                case "1":
                    if (red) StackA.BackgroundColor = Color.FromHex("#42C80E0E");
                    else StackA.BackgroundColor = Color.FromHex("#4519D221");
                    break;
                case "2":
                    if (red) StackB.BackgroundColor = Color.FromHex("#42C80E0E");
                    else StackB.BackgroundColor = Color.FromHex("#4519D221");
                    break;
                case "3":
                    if (red) StackC.BackgroundColor = Color.FromHex("#42C80E0E");
                    else StackC.BackgroundColor = Color.FromHex("#4519D221");
                    break;
                case "4":
                    if (red) StackD.BackgroundColor = Color.FromHex("#42C80E0E");
                    else StackD.BackgroundColor = Color.FromHex("#4519D221");
                    break;
            }
        }

        public async void OnWrongAnswer()
        {
            bool nextGame = false;
            time = false;

            if (thisModel.IsNewHigh(thisModel.Score))
            {
                //NEW HIGH SCORE
                var db = new SpielerDatabase();
                db.Update(thisModel.Spielername, thisModel.Score);
                thisModel.Highscore = thisModel.Score;

                // NEW HIGH SCORE SOUND
                DependencyService.Get<IAudioService>().PlayHighScore();

                await Task.Delay(2500);
                var answer = await DisplayAlert(
                    $"Glückwunsch {thisModel.Spielername}!",
                    $"Du hast einen neuen Highscore: {thisModel.Score}\nErneut Spielen?", "Ja", "Nein");
                nextGame = answer;
            }
            else
            {
                DependencyService.Get<IAudioService>().PlayFaildSound();

                await Task.Delay(2000);
                var answer = await DisplayAlert(
                    $" Falsche Antwort {thisModel.Spielername}, aber du hast {thisModel.Score} gewonnen",
                    "Erneut Spielen?", "Ja", "Nein");
                nextGame = answer;
           }
            if (nextGame)
            {
                //await Navigation.PopModalAsync();
                await Navigation.PushModalAsync(new GamePage(new QuizViewModel(thisModel.thisPlayer)));
            }
            else
                await Navigation.PushModalAsync(new MainPage());
        }

        public async void OnWin()
        {

            //Play sound
            DependencyService.Get<IAudioService>().PlayFaildSound();

            var db = new SpielerDatabase();
            db.Update(thisModel.Spielername, thisModel.Score);
            thisModel.Highscore = thisModel.Score;


            var answer = await DisplayAlert($"Jetzt bist du Millionär ;) Pkt: {thisModel.Score}", "Erneut Spielen?", "Ja", "Nein");
            if (answer)
            {
                await Navigation.PopModalAsync();
                await Navigation.PushModalAsync(new GamePage(new QuizViewModel(thisModel.thisPlayer)));
            }
            else
                await Navigation.PopModalAsync();
            //await Navigation.PushModalAsync(new MainPage());  
        }

        public async void OnTimePassed()
        {

            Task.Delay(3000);
            DependencyService.Get<IAudioService>().PlayFaildSound();

            var answer = await DisplayAlert($"Zeit ist abgelaufen Pkt: {thisModel.Score}", "Erneut Spielen?", "Ja", "Nein");
            if (answer)
            {
                await Navigation.PopModalAsync();
                await Navigation.PushModalAsync(new GamePage(new QuizViewModel(thisModel.thisPlayer)));
            }
            else
                await Navigation.PopModalAsync();
            //await Navigation.PushModalAsync(new MainPage());  
        }

        public async void OnNextPressed(object sender, EventArgs e)
        {
            //Lädt neue Frage mit entsprechendem Level
            //Navigation.PopModalAsync();
            await Navigation.PushModalAsync(new GamePage(thisModel));
        }

        public async void OnCancelPressed(object sender, EventArgs e)
        {
            time = false;
            var answer = await DisplayAlert("Beenden", $" {thisModel.Spielername}, Runde wirklich Beenden?", "Ja", "Nein");
            if (answer)
            //    await Navigation.PopModalAsync();
            await Navigation.PushModalAsync(new MainPage());  
        }
    }
}
