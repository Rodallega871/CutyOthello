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
    class GB01ViewModel:BaseViewModel
    {
        private ObservableCollection<Character> _testList;
        public ObservableCollection<Character> testList
        {
            get { return this._testList; }
            set { this.SetProperty(ref this._testList, value); }
        }

        private Character _SelectedChara;
        public Character SelectedChara
        {
            get { return this._SelectedChara; }
            set { this.SetProperty(ref this._SelectedChara, value); }
        }
       
        private string _Player1BoardColor;
        public string Player1BoardColor
        {
            get { return this._Player1BoardColor; }
            set { this.SetProperty(ref this._Player1BoardColor, value); }
        }

        private string _Player2BoardColor;
        public string Player2BoardColor
        {
            get { return this._Player2BoardColor; }
            set { this.SetProperty(ref this._Player2BoardColor, value); }
        }

        public Command TapBackButton { get; }
        public Command TapOKButton { get; }

        public GB01ViewModel()
        {
            //画面表記ボードの色を更新する。
            switch (userDataStore.WaytoG02Status)
            {
                case UserDataStore.EditOrCreaterCharaStatus.SelectPlayerOne:
                    Player1BoardColor = "Red";
                    Player2BoardColor = "RoyalBlue";
                    break;
                case UserDataStore.EditOrCreaterCharaStatus.SelectPlayerTwo:
                    Player1BoardColor = "RoyalBlue";
                    Player2BoardColor = "Red";
                    break;
                default:
                    break;
            }

            testList = characterDataStore.GetItemsAsync(true);

            TapBackButton = new Command(() =>
            {
                switch (userDataStore.WaytoG02Status)
                {
                    case UserDataStore.EditOrCreaterCharaStatus.SelectPlayerOne:
                        SelectedChara = null;
                        //遷移方法を保管する
                        userDataStore.WaytoG02Status = UserDataStore.EditOrCreaterCharaStatus.MainTitle;
                        Application.Current.MainPage = new GZ102();
                        break;
                    case UserDataStore.EditOrCreaterCharaStatus.SelectPlayerTwo:
                        SelectedChara = null;
                        //遷移方法を保管する
                        userDataStore.WaytoG02Status = UserDataStore.EditOrCreaterCharaStatus.SelectPlayerOne;
                        Application.Current.MainPage = new GB01();
                        break;
                    default:
                        break;
                }
            });

            TapOKButton = new Command(() =>
            {
                switch (userDataStore.WaytoG02Status)
                {
                    case UserDataStore.EditOrCreaterCharaStatus.SelectPlayerOne:
                        if (SelectedChara != null)
                        {
                            characterDataStore.PlayerOne = SelectedChara;
                            SelectedChara = null;
                            //遷移方法を保管する
                            userDataStore.WaytoG02Status = UserDataStore.EditOrCreaterCharaStatus.SelectPlayerTwo;
                            Application.Current.MainPage = new GB01();
                        }
                        break;
                    case UserDataStore.EditOrCreaterCharaStatus.SelectPlayerTwo:
                        if (SelectedChara != null)
                        {
                            characterDataStore.PlayerTwo = SelectedChara;
                            SelectedChara = null;
                            //遷移方法を保管する
                            userDataStore.WaytoG02Status = UserDataStore.EditOrCreaterCharaStatus.BattleDisp;
                            Application.Current.MainPage = new GZ201();
                        }
                        break;
                    default:
                        break;
                }
            });
        }
    }
}
