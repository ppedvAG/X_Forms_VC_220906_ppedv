using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using X_Forms.Übungen.GoogleBooks.Model;
using X_Forms.Übungen.GoogleBooks.Services;
using Xamarin.Forms;

//vgl. MVVMBsp/ViewModel
namespace X_Forms.Übungen.GoogleBooks.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        //Interface-Event
        public event PropertyChangedEventHandler PropertyChanged;

        //Properties für DataBinding
        public string SearchString { get; set; }

        private bool isRefreshing = false;
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { isRefreshing = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRefreshing")); }
        }
        public Command SearchCommand { get; set; }
        public ObservableCollection<Item> BookList
        {
            get => Collections.BookList;
            set => Collections.BookList = value;
        }

        //Command-Methode
        private async void SearchBooks()
        {
            if (!String.IsNullOrEmpty(SearchString))
                //Damit die View während des Suchprozesses nicht einfriert, muss diese Methode in einem seperaten Task laufen
                await Task.Run(() =>
                {
                    IsRefreshing = true;
                    BookService.FillBookList(SearchString);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BookList)));
                    IsRefreshing = false;
                });
            else IsRefreshing = false;
        }

        //Konstruktor
        public MainViewModel()
        {
            BookList = new ObservableCollection<Item>();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BookList)));
            SearchCommand = new Command(SearchBooks);
        }

    }
}
