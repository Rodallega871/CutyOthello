using CutyOthello.Commn;
using CutyOthello.Models;
using CutyOthello.Services;
using CutyOthello.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CutyOthello.ViewModels
{
    class GZ201ViewModel : BaseViewModel
    {
        //public Command TapPASSButton { get; }
        //public Command TapSurrenderButton { get; }
        //public Command TapTopMenuButton { get; }
        public Command TapDialogButton { get; }


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

        private string _PlayerOneTextColor;
        public string PlayerOneTextColor
        {
            get { return this._PlayerOneTextColor; }
            set { this.SetProperty(ref this._PlayerOneTextColor, value); }
        }

        private string _PlayerTwoTextColor;
        public string PlayerTwoTextColor
        {
            get { return this._PlayerTwoTextColor; }
            set { this.SetProperty(ref this._PlayerTwoTextColor, value); }
        }

        private string _PlayerOneOutlineColor;
        public string PlayerOneOutlineColor
        {
            get { return this._PlayerOneOutlineColor; }
            set { this.SetProperty(ref this._PlayerOneOutlineColor, value); }
        }

        private string _PlayerTwoOutlineColor;
        public string PlayerTwoOutlineColor
        {
            get { return this._PlayerTwoOutlineColor; }
            set { this.SetProperty(ref this._PlayerTwoOutlineColor, value); }
        }

        private bool _CanFinishGame;
        public bool CanFinishGame
        {
            get { return this._CanFinishGame; }
            set { this.SetProperty(ref this._CanFinishGame, value); }
        }

        private bool _CanSurrender;
        public bool CanSurrender
        {
            get { return this._CanSurrender; }
            set { this.SetProperty(ref this._CanSurrender, value); }
        }

        private bool _CantoTopMenu;
        public bool CantoTopMenu
        {
            get { return this._CantoTopMenu; }
            set { this.SetProperty(ref this._CantoTopMenu, value); }
        }

        private bool _testBool;
        public bool testBool
        {
            get { return this._testBool; }
            set { this.SetProperty(ref this._testBool, value); }
        }

        private bool _testBool2;
        public bool testBool2
        {
            get { return this._testBool2; }
            set { this.SetProperty(ref this._testBool2, value); }
        }

        private string _DialogImage;
        public string DialogImage
        {
            get { return this._DialogImage; }
            set { this.SetProperty(ref this._DialogImage, value); }
        }

        private string _DialogSentence;
        public string DialogSentence
        {
            get { return this._DialogSentence; }
            set { this.SetProperty(ref this._DialogSentence, value); }
        }

        private string _DialogButton;
        public string DialogButton
        {
            get { return this._DialogButton; }
            set { this.SetProperty(ref this._DialogButton, value); }
        }

        OthelloMain othelloMain;

        public GZ201ViewModel()
        {
            PlayerOneTextColor = "Red";
            PlayerOneOutlineColor = "Red";
            Player1NameAndCount = characterDataStore.PlayerOne.DogName;
            Player1Picture = characterDataStore.PlayerOne.DogPicture;
            Player2NameAndCount = characterDataStore.PlayerTwo.DogName;
            Player2Picture = characterDataStore.PlayerTwo.DogPicture;
            CanFinishGame = false;
            CanSurrender = true;
            CantoTopMenu = true;

            othelloMain = new OthelloMain();
            othelloMain.SetCpuLevel(characterDataStore.CPULevelInfo);

            TapDialogButton = new Command(() =>
            {
                testBool2 = false;
            });

            //購読解除がうまく設定できないからバインディング挫折
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
            userDataStore.WaytoG02Status = UserDataStore.EditOrCreaterCharaStatus.MainTitle;
            Application.Current.MainPage = new GZ102();
        }

        public void ViewModelTapNextGamen()
        {
            userDataStore.WaytoG02Status = UserDataStore.EditOrCreaterCharaStatus.BattleResult;
            characterDataStore.PlayerOneCount = GetBlackStoneList()[0].Count;
            characterDataStore.PlayerTwoCount = GetWhiteStoneList()[0].Count;
            Application.Current.MainPage = new GZ202();
        }

        public void ViewModelTapSurrenderButton()
        {
            //ポップアップを表示する。(タップした場所が適切でない場合)
            DependencyService.Get<IAlertService>().ShowYesNoDialog(
                "とちゅうしゅうりょう", "こうさんしました。", "OK", "Cancel");

            userDataStore.WaytoG02Status = UserDataStore.EditOrCreaterCharaStatus.BattleResult;
            characterDataStore.PlayerOneCount = GetBlackStoneList()[0].Count;
            characterDataStore.PlayerTwoCount = GetWhiteStoneList()[0].Count;
            Application.Current.MainPage = new GZ202();
        }

        public async Task<bool> DammyModel(int row,int col)
        {
            var reslut = await Task.Run(() =>
            {
                if (!othelloMain.PutStone(row, col))
                {
                    //ポップアップを表示する。(タップした場所が適切でない場合)
                    //DependencyService.Get<IAlertService>().ShowYesNoDialog(
                    //    "けいこく", "そこにいしはおけません。", "OK", "Cancel");
                    testBool2 = true;
                    DialogImage = "Keikoku.png";
                    DialogSentence = "そこにいしはおけません";
                    DialogButton = "OK";
                    return false;
                }

                //パスチェック
                if (othelloMain.IsPass())
                {
                    //ポップアップを表示する。(タップした場所が適切でない場合)
                    //DependencyService.Get<IAlertService>().ShowYesNoDialog(
                    //    "けいこく", "おけるばしょがないので、パスします。", "OK", "Cancel");
                    testBool2 = true;
                    DialogImage = "Keikoku.png";
                    DialogSentence = "おけるばしょがないので、パスします。";
                    DialogButton = "OK";

                    return false;
                }
                else
                {
                    PlayerOneTextColor = PlayerOneTextColor == "Red" ? "RoyalBlue" : "Red";
                    PlayerOneOutlineColor = PlayerOneOutlineColor == "Red" ? "Pink" : "Red";
                    PlayerTwoTextColor = PlayerTwoTextColor == "Red" ? "RoyalBlue" : "Red";
                    PlayerTwoOutlineColor = PlayerTwoOutlineColor == "Red" ? "Pink" : "Red";
                }

                //ゲーム終了チェック
                if (othelloMain.IsGameFinished())
                {
                    //ポップアップを表示する。(タップした場所が適切でない場合)
                    //DependencyService.Get<IAlertService>().ShowYesNoDialog(
                    //    "ゲームしゅうりょう", "けっかしゅうけいちゅうです。GameFinishボタンをおしてください。", "OK", "Cancel");
                    testBool2 = true;
                    DialogImage = "AshiAto.png";
                    DialogSentence = "けっかしゅうけいちゅうです。GameFinishボタンをおしてください。";
                    DialogButton = "OK";

                    CanFinishGame = true;
                    CanSurrender = false;
                }

                //表示用の石獲得数を表示
                Player1NameAndCount = characterDataStore.PlayerOne.DogName + "   " + othelloMain.GetBlackStoneCount();
                Player2NameAndCount = characterDataStore.PlayerTwo.DogName + "   " + othelloMain.GetWhiteStoneCount();

                return true;
            });
            return reslut;
        }

        public async Task<bool> DammyModelCPU()
        {
            await Task.Run(() =>
            {

                othelloMain.PutStoneCPU();

                //パスチェック
                if (othelloMain.IsPass())
                {
                    ////ポップアップを表示する。(タップした場所が適切でない場合)
                    //DependencyService.Get<IAlertService>().ShowYesNoDialog(
                    //    "けいこく", "おけるばしょがないので、パスします。", "OK", "Cancel");
                    testBool2 = true;
                    DialogImage = "Keikoku.png";
                    DialogSentence = "おけるばしょがないので、パスします。";
                    DialogButton = "OK";
                    Task.Run(async() => await DammyModelCPU()); 
                }
                else
                {
                    PlayerOneTextColor = PlayerOneTextColor == "Red" ? "RoyalBlue" : "Red";
                    PlayerOneOutlineColor = PlayerOneOutlineColor == "Red" ? "Pink" : "Red";
                    PlayerTwoTextColor = PlayerTwoTextColor == "Red" ? "RoyalBlue" : "Red";
                    PlayerTwoOutlineColor = PlayerTwoOutlineColor == "Red" ? "Pink" : "Red";
                }

                //ゲーム終了チェック
                if (othelloMain.IsGameFinished())
                {
                    ////ポップアップを表示する。(タップした場所が適切でない場合)
                    //DependencyService.Get<IAlertService>().ShowYesNoDialog(
                    //    "ゲームしゅうりょう", "けっかしゅうけいちゅうです。GameFinishボタンをおしてください。", "OK", "Cancel");
                    testBool2 = true;
                    DialogImage = "AshiAto.png";
                    DialogSentence = "けっかしゅうけいちゅうです。GameFinishボタンをおしてください。";
                    DialogButton = "OK";

                    CanFinishGame = true;
                    CanSurrender = false;
                }

                //表示用の石獲得数を表示
                Player1NameAndCount = characterDataStore.PlayerOne.DogName + "   " + othelloMain.GetBlackStoneCount();
                Player2NameAndCount = characterDataStore.PlayerTwo.DogName + "   " + othelloMain.GetWhiteStoneCount();

                return true;
            });
            return true;
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

        public List<List<int>> GetBlankList()
        {
            return othelloMain.GetBlankList();
        }

    }
}
