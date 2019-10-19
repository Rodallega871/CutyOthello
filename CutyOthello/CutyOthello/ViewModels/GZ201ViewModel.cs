using CutyOthello.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CutyOthello.ViewModels
{
    class GZ201ViewModel : BaseViewModel
    {
        public Command TapPASSButton { get; }
        public Command TapSurrenderButton { get; }
        public Command TapTopMenuButton { get; }


        public GZ201ViewModel()
        {
            TapTopMenuButton = new Command(() =>
            {
                Application.Current.MainPage = new GZ102();
            });
        }
    }
}
