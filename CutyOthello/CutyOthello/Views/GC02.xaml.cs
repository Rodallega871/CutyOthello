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
    public partial class GC02 : ContentPage
    {
        GC02ViewModel viewmodel;

        public GC02()
        {
            InitializeComponent();

            BindingContext = viewmodel = new GC02ViewModel();

        }
    }
}