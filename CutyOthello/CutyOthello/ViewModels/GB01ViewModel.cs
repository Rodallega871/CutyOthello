using CutyOthello.Models;
using CutyOthello.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace CutyOthello.ViewModels
{
    class GB01ViewModel:BaseViewModel
    {
        private ObservableCollection<Character> _testList;
        public ObservableCollection<Character> testList
        {
            get { return this._testList; }
            set { this.SetProperty(ref this._testList, value); }
        }

        public Command TapBackButton { get; }
        public Command TapOKButton { get; }

        public GB01ViewModel()
        {            
            testList = characterDataStore.GetItemsAsync(true);

            TapBackButton = new Command(() =>
            {
                Application.Current.MainPage = new GZ102();
            });

            TapOKButton = new Command(() =>
            {
                Application.Current.MainPage = new GZ201();
            });
        }


    }
}
