using CutyOthello.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CutyOthello.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GZ202 : ContentPage
    {
        GZ202ViewModel viewmodel;

        public GZ202()
        {
            InitializeComponent();

            BindingContext = viewmodel = new GZ202ViewModel();

        }
    }
}