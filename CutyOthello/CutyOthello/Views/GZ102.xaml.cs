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
    public partial class GZ102 : ContentPage
    {
        GZ102ViewModel viewmodel;

        public GZ102()
        {
            InitializeComponent();

            BindingContext = viewmodel = new GZ102ViewModel();

        }

        
    }
}