using CutyOthello.Commn;
using CutyOthello.Models;
using CutyOthello.Services;
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

        private Character _SelectedChar;
        public Character SelectedChar
        {
            get { return this._SelectedChar; }
            set { this.SetProperty(ref this._SelectedChar, value); }
        }

        public Command TapBackButton { get; }
        public Command TapCreaterNewCharacter { get; }
        public Command TapEditCharacter { get; }  

        public GC01ViewModel()
        {
            testList = characterDataStore.GetDisplayItem();

            TapBackButton = new Command(() =>
            {
                Application.Current.MainPage = new GZ102();
            });

            TapEditCharacter = new Command(() =>
            {
                if (SelectedChar != null)
                {
                    //遷移方法を保管する
                    userDataStore.WaytoG02Status = UserDataStore.EditOrCreaterCharaStatus.EditChara;
                    //選択したキャラクター情報を保管する。
                    characterDataStore.TempEditCharacter = SelectedChar;
                    Application.Current.MainPage = new GC02();
                }
                else
                {
                    DependencyService.Get<IAlertService>().ShowDialog(
                        "けいこく", "キャラクターをせんたくしてください。", "OK");
                }
            });
            TapCreaterNewCharacter = new Command(() =>
            {
                //遷移方法を保管する
                userDataStore.WaytoG02Status = UserDataStore.EditOrCreaterCharaStatus.CreateNewChara;
                Application.Current.MainPage = new GC02();
            });
        }
    }
}
