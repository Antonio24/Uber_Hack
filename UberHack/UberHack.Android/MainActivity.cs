using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace UberHack.Droid
{
    [Activity(Label = "UberHack", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        TextView _emailText;
        TextView _senhaText;
        TextView _erroText;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            Button button = FindViewById<Button>(Resource.Id.entrarBtn) as Button;
            _emailText = FindViewById<TextView >(Resource.Id.emailText) as TextView;
            _senhaText = FindViewById<TextView>(Resource.Id.passText) as TextView;

            while(button == null)
            {
                button = FindViewById<Button>(Resource.Id.entrarBtn) as Button;
                _emailText = FindViewById<TextView>(Resource.Id.emailText) as TextView;
                _senhaText = FindViewById<TextView>(Resource.Id.passText) as TextView;
                _erroText = FindViewById<TextView>(Resource.Id.erroText) as TextView;
            }

            button.Click += Button_Click;

            StartActivity(typeof(MapPins));
            StartActivity(typeof(GPS));
        }

        void Button_Click(object sender, EventArgs e)
        {
            if(Model.Connect.VerificaUsuario(_emailText.Text, _senhaText.Text))
            {
                StartActivity(typeof(MapPins));
                StartActivity(typeof(GPS));
            }
            else
            {
                _erroText.Text = "Usuário ou senha inválidos.";
            }
        }
    }
}

