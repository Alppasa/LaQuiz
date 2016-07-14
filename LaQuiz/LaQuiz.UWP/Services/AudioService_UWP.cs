using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Playback;
using LaQuiz.Interfaces;
using LaQuiz.UWP.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(AudioService_UWP))]
namespace LaQuiz.UWP.Services
{
   public class AudioService_UWP : IAudioService
   {
       public AudioService_UWP()
       {
            if (player == null)
                player = BackgroundMediaPlayer.Current;
        }
       private MediaPlayer player;
        public bool PlayCorrectSound()
        {
            player.SetUriSource(new Uri(String.Format("ms-appx:///Assets/Sounds/applause.mp3", UriKind.Absolute)));
            player.Play();
            return true;
        }

        public bool PlayCountdown(bool on_off)
        {
            if(on_off)
            player.SetUriSource(new Uri(String.Format("ms-appx:///Assets/Sounds/countdown.mp3", UriKind.Absolute)));
            if (on_off)
                player.Play();
            return true;
        }

        public bool PlayFaildSound()
        {
            player.SetUriSource(new Uri(String.Format("ms-appx:///Assets/Sounds/FaildSound.mp3", UriKind.Absolute)));
            player.Play();
            return true;
        }

        public bool PlayHighScore()
        {
            player.SetUriSource(new Uri(String.Format("ms-appx:///Assets/Sounds/HighScore.mp3", UriKind.Absolute)));
            player.Play();
            return true;
        }

        public bool PlayWinSound()
        {
            player.SetUriSource(new Uri(String.Format("ms-appx:///Assets/Sounds/billionaire.mp3", UriKind.Absolute)));
            player.Play();
            return true;
        }
    }
}
