using Android.App;
using Android.Content;
using Android.OS;

namespace Signawel.Mobile.Droid
{
    [Activity(Theme="@style/Theme.Splash",
        MainLauncher =true,
        NoHistory =true)]
    public class SplashActivity : Activity
    {
        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
        }

        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }


    }
}