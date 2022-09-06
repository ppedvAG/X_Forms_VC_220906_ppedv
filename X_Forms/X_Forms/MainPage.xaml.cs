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
        public MainPage()
        {
            InitializeComponent();

            Stl_Main.BackgroundColor = Color.LightGreen;
        }

        private void Btn_KlickMich_Clicked(object sender, EventArgs e)
        {
            Btn_KlickMich.Text = "Ich wurde angeklickt";
        }
    }
}
