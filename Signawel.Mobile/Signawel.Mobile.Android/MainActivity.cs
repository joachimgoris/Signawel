
using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Plugin.CurrentActivity;
using Android.Content.Res;

namespace Signawel.Mobile.Droid
{
    [Activity(Label = "Signawel.Mobile", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static readonly int PickImageId = 1000;
        internal static MainActivity Instance { get; private set; }

        public TaskCompletionSource<Stream> PickImageTaskCompletionSource { set; get; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            // TODO: acquire legit certificate
            System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(savedInstanceState);
            Xamarin.FormsMaps.Init(this, savedInstanceState);
            Instance = this;

            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            global::Xamarin.Forms.FormsMaterial.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == PickImageId)
            {
                if ((resultCode == Result.Ok) && (data != null))
                {
                    Android.Net.Uri uri = data.Data;
                    Stream stream = ContentResolver.OpenInputStream(uri);

                    // Set the Stream as the completion of the Task
                    PickImageTaskCompletionSource.SetResult(stream);
                }
                else
                {
                    PickImageTaskCompletionSource.SetResult(null);
                }
            }
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            RequestedOrientation = ScreenOrientation.Portrait;
        }
    }
}