using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaQuiz.Interfaces
{
    public interface IAudioService
    {
        bool PlayFaildSound();
        bool PlayCorrectSound();
        bool PlayHighScore();
        bool PlayWinSound();
        bool PlayCountdown(bool on_off);
    }
}
