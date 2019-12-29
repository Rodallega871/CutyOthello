using CutyOthello.Services;
using CutyOthello.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CutyOthello.ViewModels
{
    class GZ202ViewModel : BaseViewModel
    {
        public Command TapTopMenuButton { get; }
        public Command TapNextButton { get; }

        private string _DisplayWinnerName;
        public string DisplayWinnerName
        {
            get { return this._DisplayWinnerName; }
            set { this.SetProperty(ref this._DisplayWinnerName, value); }
        }

        private string _Player1DogImage;
        public string Player1DogImage
        {
            get { return this._Player1DogImage; }
            set { this.SetProperty(ref this._Player1DogImage, value); }
        }

        private string _Player1GotStoneCount;
        public string Player1GotStoneCount
        {
            get { return this._Player1GotStoneCount; }
            set { this.SetProperty(ref this._Player1GotStoneCount, value); }
        }

        private string _Player1Dogname;
        public string Player1Dogname
        {
            get { return this._Player1Dogname; }
            set { this.SetProperty(ref this._Player1Dogname, value); }
        }

        private string _Player2DogImage;
        public string Player2DogImage
        {
            get { return this._Player2DogImage; }
            set { this.SetProperty(ref this._Player2DogImage, value); }
        }

        private string _Player2GotStoneCount;
        public string Player2GotStoneCount
        {
            get { return this._Player2GotStoneCount; }
            set { this.SetProperty(ref this._Player2GotStoneCount, value); }
        }

        private string _Player2Dogname;
        public string Player2Dogname
        {
            get { return this._Player2Dogname; }
            set { this.SetProperty(ref this._Player2Dogname, value); }
        }

        private bool _CanNextStage;
        public bool CanNextStage
        {
            get { return this._CanNextStage; }
            set { this.SetProperty(ref this._CanNextStage, value); }
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

        private string _PlayerOneOutLineColor;
        public string PlayerOneOutLineColor
        {
            get { return this._PlayerOneOutLineColor; }
            set { this.SetProperty(ref this._PlayerOneOutLineColor, value); }
        }

        private string _PlayerTwoOutLineColor;
        public string PlayerTwoOutLineColor
        {
            get { return this._PlayerTwoOutLineColor; }
            set { this.SetProperty(ref this._PlayerTwoOutLineColor, value); }
        }

        public GZ202ViewModel()
        {
            int allStoneCount = characterDataStore.PlayerOneCount + characterDataStore.PlayerTwoCount;
            Player1Dogname = characterDataStore.PlayerOne.DogName;
            Player1DogImage = characterDataStore.PlayerOne.DogPicture; 
            Player1GotStoneCount = characterDataStore.PlayerOneCount.ToString() + "  /  " + allStoneCount;
            Player2Dogname = characterDataStore.PlayerTwo.DogName; 
            Player2DogImage = characterDataStore.PlayerTwo.DogPicture;
            Player2GotStoneCount = characterDataStore.PlayerTwoCount.ToString() + "  /  " + allStoneCount;

            //勝者を確定させる。
            if(characterDataStore.SurrenderJudge.Equals(CharacterDataStore.SurrenderStatus.SurrenderPlayerOne))
            {
                DisplayWinnerName = characterDataStore.PlayerTwo.DogName + Environment.NewLine + "Winner(こうさん)";
                PlayerOneTextColor = "RoyalBlue";
                PlayerTwoTextColor = "Red";
                PlayerOneOutLineColor = "Pink";
                PlayerTwoOutLineColor = "Red";
            }
            else if (characterDataStore.SurrenderJudge.Equals(CharacterDataStore.SurrenderStatus.SurrenderPlayerTwo))
            {
                DisplayWinnerName = characterDataStore.PlayerOne.DogName + Environment.NewLine + "Winner(こうさん)";
                PlayerOneTextColor = "Red";
                PlayerTwoTextColor = "RoyalBlue";
                PlayerOneOutLineColor = "Red";
                PlayerTwoOutLineColor = "Pink";

            }
            else
            {
                if (characterDataStore.PlayerOneCount > characterDataStore.PlayerTwoCount)
                {
                    DisplayWinnerName = characterDataStore.PlayerOne.DogName + Environment.NewLine + "Winner";
                    PlayerOneTextColor = "Red";
                    PlayerTwoTextColor = "RoyalBlue";
                    PlayerOneOutLineColor = "Red";
                    PlayerTwoOutLineColor = "Pink";
                }
                else if (characterDataStore.PlayerOneCount < characterDataStore.PlayerTwoCount)
                {
                    DisplayWinnerName = characterDataStore.PlayerTwo.DogName + Environment.NewLine + "Winner";
                    PlayerOneTextColor = "RoyalBlue";
                    PlayerTwoTextColor = "Red";
                    PlayerOneOutLineColor = "Pink";
                    PlayerTwoOutLineColor = "Red";
                }
                else if (characterDataStore.PlayerOneCount == characterDataStore.PlayerTwoCount)
                    DisplayWinnerName = "Draw";
            }

            CanNextStage = false;

            TapTopMenuButton = new Command(() =>
            {
                ExecInitial();
                Application.Current.MainPage = new GZ101();
            });

            TapNextButton = new Command(() =>
            {
                ExecInitial();
                Application.Current.MainPage = new GA01();
            });

        }

        private void ExecInitial()
        {
            characterDataStore.PlayerOne = null;
            characterDataStore.PlayerTwo = null;
            characterDataStore.PlayerOneCount = 0;
            characterDataStore.PlayerTwoCount = 0;
        }
    }
}
