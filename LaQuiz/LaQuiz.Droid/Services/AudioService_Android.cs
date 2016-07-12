using System;
using Android.Media;
using LaQuiz.Droid.Services;
using LaQuiz.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(AudioService_Android))]

namespace LaQuiz.Droid.Services
{
   public  class AudioService_Android : IAudioService
   {
       private MediaPlayer _mediaPlayer;

        public bool PlayCorrectSound()
        {
            _mediaPlayer = MediaPlayer.Create(global::Android.App.Application.Context, Resource.Raw.applause);
            _mediaPlayer.Start();
            return true;
        }

        public bool PlayFaildSound()
       {
           _mediaPlayer = MediaPlayer.Create(global::Android.App.Application.Context, Resource.Raw.FaildSound);
            _mediaPlayer.Start();
           return true;
       }

        public bool PlayHighScore()
        {
            _mediaPlayer = MediaPlayer.Create(global::Android.App.Application.Context, Resource.Raw.HighScore);
            _mediaPlayer.Start();
            return true;
        }

        public bool PlayWinSound()
        {
            throw new NotImplementedException();
        }

       public bool PlayCountdown(bool on_off)
       {
            _mediaPlayer = MediaPlayer.Create(global::Android.App.Application.Context, Resource.Raw.countdown);
            if(on_off)
            _mediaPlayer.Start();
            else                
            _mediaPlayer.Pause();
            return true;
        }
   }
}