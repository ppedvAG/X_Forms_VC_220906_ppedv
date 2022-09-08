using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace X_Forms.MVVM.ViewModel
{
    //Im ViewModel-Teil eines MVVM-Programms werden Klassen definiert, welche als Verbindungsstück zwischen den Views und den Modelklassen fungieren.
    //Diese Klassen sind die einzigen Programmteile, welche Referenzen auf Model-Klassen beinhalten. Sie selbst sind jeweils einem View zugrordnet,
    //mit welchem sie (nur) über den BindingContext des jeweiligen Views verbunden sind.
    //INotifyPropertyChanged informiert die GUI über Veränderungen in den Daten
    public class FahrzeugViewModel : INotifyPropertyChanged
    {
        //View-Property für z.B. DisplayAlerts
        public Page ContextPage { get; set; }

        //Command-Properties (angebunden an Command-Eigenschaften der Buttons)
        public Command HinzufügenCmd { get; set; }
        public Command LöschenCmd { get; set; }

        //Properties für Anbindung an die Controls (verweisen auf NeuesFahrzeug). Nötig, um Hinzufügen.CanExecute() neu zu prüfen.
        public string Fahrzeughersteller 
        {
            get { return NeuesFahrzeug.Hersteller; }
            set { NeuesFahrzeug.Hersteller = value; HinzufügenCmd.ChangeCanExecute(); } 
        }
        public int FahrzeugMaxGeschwindigkeit
        {
            get { return NeuesFahrzeug.MaxGeschwindigkeit; }
            set { NeuesFahrzeug.MaxGeschwindigkeit = value; HinzufügenCmd.ChangeCanExecute(); }
        }

        //Property für Neues Fahrzeug
        public Model.Fahrzeug NeuesFahrzeug { get; set; }

        //Property zur Repräsentation der geladenen Personen in ListView (verweist an die Model-Klasse)
        public ObservableCollection<Model.Fahrzeug> Fahrzeugliste 
        {
            get { return Model.Fahrzeug.Fahrzeugliste; }
            set { Model.Fahrzeug.Fahrzeugliste = value; } 
        }

        //Konstruktor
        public FahrzeugViewModel()
        {
            //Propertiyinitialisierung
            NeuesFahrzeug = new Model.Fahrzeug();
            //Commandinitialisierung
            HinzufügenCmd = new Command(FügeFahrzeugHinzu, CanFügeFahrzeugHinzu);
            LöschenCmd = new Command(LöscheFahrzeug, CanLöscheFahrzeug);
        }

        //Command-Methoden
        private void FügeFahrzeugHinzu()
        {
            //Hinzufügen des neuen Fahrzeugs
            Fahrzeugliste.Add(NeuesFahrzeug);
            //Initialisieren des nächsten neuen Fahrzeugs
            NeuesFahrzeug = new Model.Fahrzeug();
            //Informieren der GUI, dass sich die Eigenschaften veränder haben -> Entries werden geleert
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Fahrzeughersteller)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FahrzeugMaxGeschwindigkeit)));
        }

        private bool CanFügeFahrzeugHinzu()
        {
            //Prüfung, ob in die Entries etwas eingetragen wurde
            return !String.IsNullOrEmpty(Fahrzeughersteller) && FahrzeugMaxGeschwindigkeit > 0;
        }

        private async void LöscheFahrzeug(object selectedFahrzeug)
        {
            //Fragen, ob das Fahrzeug wirklich gelöscht werden soll (DisplayAlert über ContextPage-Eigenschaft)
            if (await ContextPage.DisplayAlert("Lösche Fahrzeug", "Soll dieses Fahrzeug wirklich gelöscht werden?", "Ja", "Nein"))
            {
                //Löschen des Fahrzeugs aus der Liste (CommandParameter enthält ListView.SelectedItem, vgl. View/FahrzeugView.xaml)
                Fahrzeugliste.Remove(selectedFahrzeug as Model.Fahrzeug);
                //Aktualisierung der CanExecute-Methode des Events (damit der Button wieder ausgegraut wird)
                LöschenCmd.ChangeCanExecute();
            }
        }

        private bool CanLöscheFahrzeug(object selectedFahrzeug)
        {
            //Prüfung, ob ein ausgewählter ListView-Eintrag in der Fahrzeugliste vorhanden ist
            return Fahrzeugliste.Contains(selectedFahrzeug as Model.Fahrzeug);
        }

        //Event des Interfaces
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
