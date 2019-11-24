using CutyOthello.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CutyOthello.ViewModels
{
    class GZ101ViewModl : BaseViewModel
    {
        public Command TapScreen { get; }

        public GZ101ViewModl()
        {
            TapScreen = new Command(() =>
            {
                Application.Current.MainPage = new GZ102();
            });
        }
   }
}
