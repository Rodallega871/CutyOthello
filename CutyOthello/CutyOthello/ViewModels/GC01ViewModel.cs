using CutyOthello.Models;
using CutyOthello.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace CutyOthello.ViewModels
{
    class GC01ViewModel : BaseViewModel
    {
        private ObservableCollection<Character> _testList;
        public ObservableCollection<Character> testList
        {
            get { return this._testList; }
            set { this.SetProperty(ref this._testList, value); }
        }

        public Command TapBackButton { get; }
        public Command TapCreaterNewCharacter { get; }
        public Command SelectEditCharacter { get; }

        public GC01ViewModel()
        {
            testList = characterDataStore.GetItemsAsync(true);

            TapBackButton = new Command(() =>
            {
                Application.Current.MainPage = new GZ102();
            });

            SelectEditCharacter = new Command(() =>
            {
                Application.Current.MainPage = new GC02();
            });
            TapCreaterNewCharacter = new Command(() =>
            {
                Application.Current.MainPage = new GC02();
            });

        }
    }
}
