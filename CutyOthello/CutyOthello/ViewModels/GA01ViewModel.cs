using CutyOthello.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CutyOthello.ViewModels
{
    class GA01ViewModel : BaseViewModel
    {
        public Command TapCommentPlayerA { get; }
        public Command TapCommentPlayerB { get; }

        public GA01ViewModel()
        {

            TapCommentPlayerA = new Command(() =>
            {
                Application.Current.MainPage = new GZ102();
            });

            TapCommentPlayerB = new Command(() =>
            {
                Application.Current.MainPage = new GZ102();
            });
        }
    }
}
