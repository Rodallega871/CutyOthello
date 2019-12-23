using CutyOthello.Services;
using CutyOthello.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CutyOthello.ViewModels
{
    class GB02ViewModel : BaseViewModel
    {
        private string _CPULevelImage2;
        public string CPULevelImage2
        {
            get { return this._CPULevelImage2; }
            set { this.SetProperty(ref this._CPULevelImage2, value); }
        }
        private string _CPULevelImage3;
        public string CPULevelImage3
        {
            get { return this._CPULevelImage3; }
            set { this.SetProperty(ref this._CPULevelImage3, value); }
        }
        private string _CPULevelImage4;
        public string CPULevelImage4
        {
            get { return this._CPULevelImage4; }
            set { this.SetProperty(ref this._CPULevelImage4, value); }
        }
        private string _CPULevelImage5;
        public string CPULevelImage5
        {
            get { return this._CPULevelImage5; }
            set { this.SetProperty(ref this._CPULevelImage5, value); }
        }

        public Command TapPlayerVSPlayerButton { get; }
        public Command TapCPUVSPlayerButton { get; }
        public Command TapPlayerVSCPUButton { get; }
        public Command TapLevelSelectButton { get; }
        public Command TapBackButton { get; }

        private int LevelInfo = 1;

        public GB02ViewModel()
        {
            TapPlayerVSPlayerButton = new Command(() =>
            {
                userDataStore.WaytoG02Status = UserDataStore.EditOrCreaterCharaStatus.SelectPlayerOne;
                userDataStore.BattleModeStatus = UserDataStore.BattleMode.PlayerVSPlayer;
                characterDataStore.CPULevelInfo = LevelInfo;
                Application.Current.MainPage = new GB01();
            });

            TapCPUVSPlayerButton = new Command(() =>
            {
                userDataStore.WaytoG02Status = UserDataStore.EditOrCreaterCharaStatus.SelectPlayerOne;
                userDataStore.BattleModeStatus = UserDataStore.BattleMode.CPUVSPlayer;
                characterDataStore.CPULevelInfo = LevelInfo;
                Application.Current.MainPage = new GB01();
            });

            TapPlayerVSCPUButton = new Command(() =>
            {
                userDataStore.WaytoG02Status = UserDataStore.EditOrCreaterCharaStatus.SelectPlayerOne;
                userDataStore.BattleModeStatus = UserDataStore.BattleMode.PlayerVSCPU;
                characterDataStore.CPULevelInfo = LevelInfo;
                Application.Current.MainPage = new GB01();
            });

            TapLevelSelectButton = new Command(() =>
            {
                LevelInfo++;
                if (LevelInfo > 5) LevelInfo = 1;

                switch (LevelInfo)
                {
                    case 1:
                        CPULevelImage2 = null;
                        CPULevelImage3 = null;
                        CPULevelImage4 = null;
                        CPULevelImage5 = null ;
                        break;
                    case 2:
                        CPULevelImage2 = "Star.png";
                        break;
                    case 3:
                        CPULevelImage3 = "Star.png";
                        break;
                    case 4:
                        CPULevelImage4 = "Star.png";
                        break;
                    case 5:
                        CPULevelImage5 = "Star.png";
                        break;
                }
            });

            TapBackButton = new Command(() =>
            {
                userDataStore.WaytoG02Status = UserDataStore.EditOrCreaterCharaStatus.MainTitle;
                Application.Current.MainPage = new GZ102();
            });

        }
    }

}
