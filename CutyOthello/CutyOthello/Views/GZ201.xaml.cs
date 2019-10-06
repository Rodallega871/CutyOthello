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
    public partial class GZ201 : ContentPage
    {
        GZ201ViewModel viewmodel;

        public GZ201()
        {
            InitializeComponent();

            BindingContext = viewmodel = new GZ201ViewModel();

        }
    }
}