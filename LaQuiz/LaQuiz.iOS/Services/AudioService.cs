using AudioToolbox;
using Foundation;
using LaQuiz.iOS.Services;
using LaQuiz.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(AudioService))]

namespace LaQuiz.iOS.Services
{
    class AudioService : IAudioService
    {
        public bool PlayFaildSound()
        {
            NSUrl url;
            SystemSound systemSound;

            url = NSUrl.FromFilename("Sounds/FaildSound.mp3");
            systemSound = new SystemSound(url);
            systemSound.PlayAlertSound();
            return true;
        }

        public bool PlayCorrectSound()
        {
            NSUrl url;
            SystemSound systemSound;

            url = NSUrl.FromFilename("Sounds/applause.mp3");
            systemSound = new SystemSound(url);
            systemSound.PlayAlertSound();
            return true;
        }

        public bool PlayHighScore()
        {
            NSUrl url;
            SystemSound systemSound;

            url = NSUrl.FromFilename("Sounds/HighScore.mp3");
            systemSound = new SystemSound(url);
            systemSound.PlayAlertSound();
            return true;
        }

        public bool PlayWinSound()
        {
            NSUrl url;
            SystemSound systemSound;

            url = NSUrl.FromFilename("Sounds/HighScore.mp3");
            systemSound = new SystemSound(url);
            systemSound.PlayAlertSound();
            return true;
        }

        public bool PlayCountdown(bool on_off)
        {
            NSUrl url;
            SystemSound systemSound;

            url = NSUrl.FromFilename("Sounds/countdown.mp3");
            systemSound = new SystemSound(url);
            systemSound.PlayAlertSound();
            return true;
        }
    }
}
