﻿using CutyOthello.Commn;
using CutyOthello.Services;
using CutyOthello.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CutyOthello.ViewModels
{
    public class GZ102ViewModel : BaseViewModel
    {
        public Command ClickButtonGA01 { get; }
        public Command ClickButtonGB01 { get; }
        public Command ClickButtonGC01 { get; }
        public Command FirstScreenTap  { get; }

        private bool _IsEnabledButtonGA01;
        public bool IsEnabledButtonGA01
        {
            get { return this._IsEnabledButtonGA01; }
            set { this.SetProperty(ref this._IsEnabledButtonGA01, value); }
        }

        private bool _IsEnabledButtonGB01;
        public bool IsEnabledButtonGB01
        {
            get { return this._IsEnabledButtonGB01; }
            set { this.SetProperty(ref this._IsEnabledButtonGB01, value); }
        }

        private bool _IsEnabledButtonGC01;
        public bool IsEnabledButtonGC01
        {
            get { return this._IsEnabledButtonGC01; }
            set { this.SetProperty(ref this._IsEnabledButtonGC01, value); }
        }

        public GZ102ViewModel()
        {
            IsEnabledButtonGA01 = true;
            IsEnabledButtonGB01 = true;
            IsEnabledButtonGC01 = true;

            ClickButtonGA01 = new Command(() =>
            {
                Application.Current.MainPage = new GA01();
            });

            ClickButtonGB01 = new Command(() =>
            {
                //遷移方法を保管する
                userDataStore.WaytoG02Status = UserDataStore.EditOrCreaterCharaStatus.BattleModeSelect;
                Application.Current.MainPage = new GB02();
            });

            ClickButtonGC01 = new Command(() =>
            {
                Application.Current.MainPage = new GC01();
            });

        }
    }
}
