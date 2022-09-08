using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace X_Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MCSubscriberPage : ContentPage
    {
        public MCSubscriberPage()
        {
            InitializeComponent();

            //Anmelden an MessaginCenter für eine Nachricht namens "Nachricht" von einem "Hauptseite"-Objekt.
            //Bei Ankunft wird die CallBack-Methode ausgeführt (hier: Übertragen des Nachrichten-Inhalts in das Label)
            MessagingCenter.Subscribe<MainPage, string>(this, "Nachricht", (sender, inhalt) => Lbl_Output.Text = inhalt);
        }
    }
}