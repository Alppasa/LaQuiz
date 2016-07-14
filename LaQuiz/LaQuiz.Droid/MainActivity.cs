using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace LaQuiz.Droid
{
    [Activity(Label = "LaQuiz", Icon = "@drawable/LaQuizLogo", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        public bool AllowBack = false;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        public override void OnBackPressed()
        {
            // This prevents a user from being able to hit the back button and leave the login page.  
            if (AllowBack)
            {
                base.OnBackPressed();
            }
        }
    }
}

