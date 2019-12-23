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
    public partial class GB02 : ContentPage
    {
        GB02ViewModel viewmodel;

        public GB02()
        {
            InitializeComponent();

            BindingContext = viewmodel = new GB02ViewModel();

        }
    }
}