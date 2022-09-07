using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace X_Forms
{
    public partial class MainPage : ContentPage
    {
        //Konstruktor
        public MainPage()
        {
            //Initialisierung der UI (Xaml-Datei). Sollte immer erste Aktion des Konstruktors sein
            InitializeComponent();

            //Neuzuweisung einer Ressource (nur DynamicResource-Bindungen reagieren auf die Veränderung
            this.Resources["BtnString"] = "Ich bin eine neue Resource";

            //Neuzuweisung einer Eigenschaft eines Controls
            Stl_Main.BackgroundColor = Color.LightGreen;
        }

        //EventHandler eines Button-Click-Events (reagiert auf Button-Klick oder -Tab)
        private void Btn_KlickMich_Clicked(object sender, EventArgs e)
        {
            //Neuzuweisung einer UI-Property über die x:Name-Property des Steuerelements
            Btn_KlickMich.Text = "Ich wurde angeklickt";

            //Neuzuweisung einer Property des Eventauslösenden Steuerelements
            if (sender is Button)
                (sender as Button).BackgroundColor = Color.HotPink;

            //Zugriff auf in Picker ausgewählten Eintrag
            Lbl_Output.Text = Pkr_Monkeys.SelectedItem?.ToString();
        }

        //Event, welches durch Text-Veränderung im Entry ausgelöst wird
        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Übertrag des Entry-Texts ins Label
            Lbl_Output.Text = Ent_Input.Text;
        }

        //EventHandler eines Slider-ValueChanged-Events (reagiert auf Wert-Veränderung in Slider.Value)
        //Dieser EventHandler word auch für den Stepper verwendet
        private void Sdr_Wert_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (sender is Slider)
                Lbl_Output.Text = (sender as Slider).Value.ToString();
            else
                Lbl_Output.Text = (sender as Stepper).Value.ToString();
        }

        private void Btn_Show_Clicked(object sender, EventArgs e)
        {
            //Zugriff auf Person-Objekt in BindingContext des StackLayouts
            Person person = Sly_DataBinding.BindingContext as Person;
            //Anzeigen eines DisplayAlerts (MessageBox-Äquivalent)
            DisplayAlert("Person", $"{person.Name} {person.Alter}", "ok");
        }

        private void Btn_Altern_Clicked(object sender, EventArgs e)
        {
            //Zugriff auf Person-Objekt in BindingContext des StackLayouts
            Person person = Sly_DataBinding.BindingContext as Person;
            //Erhöhung des Alters (INotifyPropertyChanged informiert die GUI, vgl. Person.cs)
            person.Alter++;
        }

        private void Btn_Add_Clicked(object sender, EventArgs e)
        {
            //Zugriff auf Person-Objekt in BindingContext des StackLayouts
            //und Hinzufügen eines neuen Objekts zu der Liste (ObservableCollection informiert die GUI, vgl. Person.cs)
            (Sly_DataBinding.BindingContext as Person).WichtigeTage.Add(Dpr_Datum.Date);
        }

        private void Btn_Delete_Clicked(object sender, EventArgs e)
        {
            //Prüfung, ob in ListView ein Item ausgewählt wurde
            if (LstV_Tage.SelectedItem != null)
                //Löschen des ausgwählten Items
                (Sly_DataBinding.BindingContext as Person).WichtigeTage.Remove((DateTime)LstV_Tage.SelectedItem);
        }

        private void Btn_Delete_02_Clicked(object sender, EventArgs e)
        {
            //Erfragen des zu löschenden Items über den Event-Sender
            DateTime wichtigerTag = sender is Button ? (DateTime)(sender as Button).CommandParameter : (DateTime)(sender as MenuItem).CommandParameter;
            //Löschen des Items
            (Sly_DataBinding.BindingContext as Person).WichtigeTage.Remove(wichtigerTag);
        }
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            //Löschen der kompletten Liste
            (Sly_DataBinding.BindingContext as Person).WichtigeTage.Clear();
        }

        //Navigationsbeispiele
        private void Btn_NavigateToGridU_Clicked(object sender, EventArgs e)
        {
            //Aufruf einer neuen Seite innerhalb der aktuellen NavigationPage 
            Navigation.PushAsync(new Übungen.U_GridLayout());
        }

        private void Btn_NavigateToRelativeLU_Clicked(object sender, EventArgs e)
        {
            //Aufruf einer neuen Seite innerhalb der aktuellen NavigationPage, welche aber keine Navigationsleiste anzeigt
            Navigation.PushModalAsync(new Übungen.U_RelativeLayout());
        }

        private void Btn_NavigateToTabbedP_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NavigationBsp.TabbedPageBsp());
        }

        private void Btn_NavigateToCarouselP_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NavigationBsp.CarouselPageBsp());
        }
    }
}
