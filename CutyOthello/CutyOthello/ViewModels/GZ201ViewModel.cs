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
    class GZ201ViewModel : BaseViewModel
    {
        public Command TapPASSButton { get; }
        public Command TapSurrenderButton { get; }
        //public Command TapTopMenuButton { get; }

        private ObservableCollection<Character> _testList;
        public ObservableCollection<Character> testList
        {
            get { return this._testList; }
            set { this.SetProperty(ref this._testList, value); }
        }

        private string _Player1NameAndCount;
        public string Player1NameAndCount
        {
            get { return this._Player1NameAndCount; }
            set { this.SetProperty(ref this._Player1NameAndCount, value); }
        }

        private string _Player2NameAndCount;
        public string Player2NameAndCount
        {
            get { return this._Player2NameAndCount; }
            set { this.SetProperty(ref this._Player2NameAndCount, value); }
        }

        private string _Player1Picture;
        public string Player1Picture
        {
            get { return this._Player1Picture; }
            set { this.SetProperty(ref this._Player1Picture, value); }
        }

        private string _Player2Picture;
        public string Player2Picture
        {
            get { return this._Player2Picture; }
            set { this.SetProperty(ref this._Player2Picture, value); }
        }

        OthelloMain othelloMain;

        public GZ201ViewModel()
        {
            Player1NameAndCount = characterDataStore.PlayerOne.DogName;
            Player1Picture = characterDataStore.PlayerOne.DogPicture;
            Player2NameAndCount = characterDataStore.PlayerTwo.DogName;
            Player2Picture = characterDataStore.PlayerTwo.DogPicture;

            othelloMain = new OthelloMain();

            //購読解除がうまく設定できない
            //TapTopMenuButton = new Command(() =>
            //{
            //    Player1NameAndCount = null;
            //    Player1Picture = null;
            //    Player2NameAndCount = null;
            //    Player2Picture = null;
            //    userDataStore.WaytoG02Status = UserDataStore.EditOrCreaterCharaStatus.MainTitle;                
            //    Application.Current.MainPage = new GZ102();
            //});
        }

        public void ViewModelTapTopMenuButton()
        {
            Player1NameAndCount = null;
            Player1Picture = null;
            Player2NameAndCount = null;
            Player2Picture = null;
            userDataStore.WaytoG02Status = UserDataStore.EditOrCreaterCharaStatus.MainTitle;
            Application.Current.MainPage = new GZ102();
        }



        public bool DammyModel(int row,int col)
        {
            if (!othelloMain.PutStone(row, col))
            {
                //ポップアップを表示する。(タップした場所が適切でない場合)
                DependencyService.Get<IAlertService>().ShowYesNoDialog(
                    "けいこく", "そこにいしはおけません。", "OK", "Cancel");
                return false;
            }
            othelloMain.Check();

            return true;

            ////入力
            //if (a == 0 && b == 0)
            //{
            //    //ポップアップを表示する。(タップした場所が適切でない場合)
            //    DependencyService.Get<IAlertService>().ShowYesNoDialog(                
            //        "けいこく", "そこにいしはおけません。", "OK", "Cancel");
            //    return new List<List<int>> { };
            //}
            //else if (a == 0)
            //{
            //    //ポップアップを表示する。(パスする)
            //    DependencyService.Get<IAlertService>().ShowYesNoDialog(
            //        "けいこく", "いしがおけないのでパスします", "OK", "Cancel");
            //    return new List<List<int>> { };
            //}
            //else
            //{
            //    return new List<List<int>> { };
            //}
        }

        public List<List<int>> GetBlackStoneList()
        {
             return othelloMain.GetBlackStoneList();
        }

        public List<List<int>> GetWhiteStoneList()
        {
            return othelloMain.GetWhiteStoneList();
        }

        public List<List<int>> GetNextStoneList()
        {
            return othelloMain.GetNextStoneList();
        }

    }
}
