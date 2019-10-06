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
    public partial class GB01 : ContentPage
    {
        GB01ViewModel viewmodel;

        public GB01()
        {
            InitializeComponent();

            BindingContext = viewmodel = new GB01ViewModel();
        }
    }
}