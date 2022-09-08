using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace X_Forms.MVVM.ViewModel
{
    public class FahrzeugViewModel : INotifyPropertyChanged
    {
        public Page ContextPage { get; set; }

        public Command HinzufügenCmd { get; set; }
        public Command LöschenCmd { get; set; }

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

        public Model.Fahrzeug NeuesFahrzeug { get; set; }


        private Model.Fahrzeug selectedFahrzeug;
        public Model.Fahrzeug SelectedFahrzeug
        {
            get { return selectedFahrzeug; }
            set 
            { 
                selectedFahrzeug = value; 
            }
        }


        public ObservableCollection<Model.Fahrzeug> Fahrzeugliste 
        {
            get { return Model.Fahrzeug.Fahrzeugliste; }
            set { Model.Fahrzeug.Fahrzeugliste = value; } 
        }

        public FahrzeugViewModel()
        {
            NeuesFahrzeug = new Model.Fahrzeug();
            HinzufügenCmd = new Command(FügeFahrzeugHinzu, CanFügeFahrzeugHinzu);
            LöschenCmd = new Command(LöscheFahrzeug, CanLöscheFahrzeug);
        }


        private void FügeFahrzeugHinzu()
        {
            Fahrzeugliste.Add(NeuesFahrzeug);

            NeuesFahrzeug = new Model.Fahrzeug();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Fahrzeughersteller)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FahrzeugMaxGeschwindigkeit)));
        }

        private bool CanFügeFahrzeugHinzu()
        {
            return !String.IsNullOrEmpty(Fahrzeughersteller) && FahrzeugMaxGeschwindigkeit > 0;
        }

        private async void LöscheFahrzeug(object selectedFahrzeug)
        {
            if (await ContextPage.DisplayAlert("Lösche Fahrzeug", "Soll dieses Fahrzeug wirklich gelöscht werden?", "Ja", "Nein"))
            {
                Fahrzeugliste.Remove(selectedFahrzeug as Model.Fahrzeug);
                SelectedFahrzeug = null;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedFahrzeug)));
                LöschenCmd.ChangeCanExecute();
            }
        }

        private bool CanLöscheFahrzeug(object selectedFahrzeug)
        {
            return selectedFahrzeug is Model.Fahrzeug;
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
