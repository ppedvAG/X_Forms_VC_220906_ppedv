using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace X_Forms
{
    public partial class App : Application
    {
        //Die App-Klasse beinhaltet eine grundlegen Initialisierung der App sowie die MainPage-Property, welche defniert,
        //welche Page gerade in der App angezeigt wird. Diese Property wird auch als Einstiegspunkt verwendet.
        public App()
        {
            InitializeComponent();

            //Zuweisung einer Page zu der MainPage-Property (Startseite)
            //MainPage = new MainPage();

            //NavigationPages ermöglichen den Aufruf der Navigation.PusAsync-Methode aus beinhalteten ContentPages
            //MainPage = new NavigationPage(new MainPage());

            MainPage = new NavigationBsp.FlyoutBsp.FlyoutNavigationBsp();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            Preferences.Set("timestamp", DateTime.Now);
        }

        protected override void OnResume()
        {
            MainPage.DisplayAlert("Geschlafene Zeit", DateTime.Now.Subtract(Preferences.Get("timestamp", DateTime.Now)).ToString(), "ok");
        }
    }
}
