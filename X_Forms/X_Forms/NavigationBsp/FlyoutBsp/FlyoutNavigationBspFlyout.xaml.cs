﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace X_Forms.NavigationBsp.FlyoutBsp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutNavigationBspFlyout : ContentPage
    {
        public ListView ListView;

        public FlyoutNavigationBspFlyout()
        {
            InitializeComponent();

            //Zuordnung des BindingContexts (damit die Bindung der ListViews funktioniert)
            BindingContext = new FlyoutNavigationBspFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        //Genestete Klasse als Context-Objekt dieser ContextPage
        private class FlyoutNavigationBspFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<FlyoutNavigationBspFlyoutMenuItem> MenuItems { get; set; }

            public FlyoutNavigationBspFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<FlyoutNavigationBspFlyoutMenuItem>(new[]
                {
                    //Definition der beinhaltenden Menüitems, TargetType gibt die Klasse der 'neuen' Pages an
                    new FlyoutNavigationBspFlyoutMenuItem { Id = 0, Title = "MainPage", TargetType=typeof(MainPage) },
                    new FlyoutNavigationBspFlyoutMenuItem { Id = 1, Title = "TabbedPage", TargetType=typeof(NavigationBsp.TabbedPageBsp) },
                    new FlyoutNavigationBspFlyoutMenuItem { Id = 2, Title = "Grid-Übung", TargetType=typeof(Übungen.U_GridLayout) },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}