using CutyOthello.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CutyOthello.ViewModels
{
    class GA01ViewModel : BaseViewModel
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

        private string _StageNo;
        public string StageNo
        {
            get { return this._StageNo; }
            set { this.SetProperty(ref this._StageNo, value); }
        }

        private string _OpponentCharaImage;
        public string OpponentCharaImage
        {
            get { return this._OpponentCharaImage; }
            set { this.SetProperty(ref this._OpponentCharaImage, value); }
        }

        private string _OpponentCharaName;
        public string OpponentCharaName
        {
            get { return this._OpponentCharaName; }
            set { this.SetProperty(ref this._OpponentCharaName, value); }
        }
                
        private int LevelInfo = 1;

        public Command TapBackButton { get; }
        public Command TapOKButton { get; }

        public GA01ViewModel()
        {

            StageNo = "Stage 1";
            OpponentCharaName = "さくせいちゅう・・・・";
            OpponentCharaImage = "Giraffe.png";

            TapBackButton = new Command(() =>
            {
                Application.Current.MainPage = new GZ102();
            });

            TapOKButton = new Command(() =>
            {
                Application.Current.MainPage = new GZ102();
            });
        }
    }
}
