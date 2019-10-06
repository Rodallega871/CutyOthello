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
    public partial class GA01 : ContentPage
    {
        GA01ViewModel viewmodel;

        public GA01()
        {
            InitializeComponent();

            BindingContext = viewmodel = new GA01ViewModel();

        }
    }
}