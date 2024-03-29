﻿using CutyOthello.Commn;
using CutyOthello.Models;
using CutyOthello.Services;
using CutyOthello.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace CutyOthello.ViewModels
{
    class GC02ViewModel : BaseViewModel
    {
        private Character _DisplayCharacterInfo;
        public Character DisplayCharacterInfo
        {
            get { return this._DisplayCharacterInfo; }
            set { this.SetProperty(ref this._DisplayCharacterInfo, value); }
        }

        private string _TitleName;
        public string TitleName
        {
            get { return this._TitleName; }
            set { this.SetProperty(ref this._TitleName, value); }
        }

        private string _CharacteImage;
        public string CharacteImage
        {
            get { return this._CharacteImage; }
            set { this.SetProperty(ref this._CharacteImage, value); }
        }

        private string _RegisterName;
        public string RegisterName
        {
            get { return this._RegisterName; }
            set { this.SetProperty(ref this._RegisterName, value); }
        }

        private string _RegisterOwnerName;
        public string RegisterOwnerName
        {
            get { return this._RegisterOwnerName; }
            set { this.SetProperty(ref this._RegisterOwnerName, value); }
        }

        private bool _CanDeleteChara;
        public bool CanDeleteChara
        {
            get { return this._CanDeleteChara; }
            set { this.SetProperty(ref this._CanDeleteChara, value); }
        }
        private bool _CanTapDialogButton;
        public bool CanTapDialogButton
        {
            get { return this._CanTapDialogButton; }
            set { this.SetProperty(ref this._CanTapDialogButton, value); }
        }
       
        private bool _isDisiplay;
        public bool isDisiplay
        {
            get { return this._isDisiplay; }
            set { this.SetProperty(ref this._isDisiplay, value); }
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

        private string _DialogCancelButton;
        public string DialogCancelButton
        {
            get { return this._DialogCancelButton; }
            set { this.SetProperty(ref this._DialogCancelButton, value); }
        }
        
        //Delete : true / Register :false
        public bool DeleteOrRegister;

        public List<string> CaharacterImages;

        public Command TapBackButton { get; }
        public Command TapRegisterButton { get; }
        public Command TapDeleteButton { get; }        
        public Command TapLeft { get; }
        public Command TapRight { get; }
        public Command TapDialogButton { get; }
        public Command TapDialogCancelButton { get; }
                   
        public GC02ViewModel()
        {
            CaharacterImages = characterDataStore.GetCharacterImages();
            CharacteImage = CaharacterImages.ElementAt(0);

            //遷移方法が(編集か新規登録を確認する)
            switch (userDataStore.WaytoG02Status)
            {
                case UserDataStore.EditOrCreaterCharaStatus.EditChara:
                    TitleName = "Edit Character (New Create)";
                    CharacteImage = characterDataStore.TempEditCharacter.DogPicture;
                    RegisterOwnerName = characterDataStore.TempEditCharacter.DogOwnerName;
                    RegisterName = characterDataStore.TempEditCharacter.DogName;
                    CanDeleteChara = true;
                    break;
                case UserDataStore.EditOrCreaterCharaStatus.CreateNewChara:
                    TitleName = "Edit Character (Edit)";
                    CanDeleteChara = false;
                    break;
                default:
                    break;
            }

            TapBackButton = new Command(() =>
            {
                Application.Current.MainPage = new GC01();
            });

            TapDeleteButton = new Command(() =>
            {
                DeleteOrRegister = true;
                isDisiplay = true;
                DialogSentence = "このデータをさくじょしますか？";
                DialogButton = "YES";
                DialogCancelButton = "No";
                DialogImage = "Keikoku.png";
                CanTapDialogButton = true;

                //var result = await DependencyService.Get<IAlertService>().ShowYesNoDialog(
                //        "けいこく", "データをほんとうにさくじょしますか？。", "OK", "Cancel");

                //if (result.isYes)
                //{
                //    await characterDataStore.DeleteCharacter();
                //    Application.Current.MainPage = new GC01();
                //}
            });

            TapRegisterButton = new Command(() =>
            {
                DeleteOrRegister = false;
                isDisiplay = true;
                DialogSentence = "このデータをとうろくしますか？";
                DialogButton = "YES";
                DialogCancelButton = "No";
                DialogImage = "Keikoku.png";
                CanTapDialogButton = true;

                //var result = await DependencyService.Get<IAlertService>().ShowYesNoDialog(
                //    "けいこく", "データをほんとうにとうろくしますか？。", "OK", "Cancel");

            });

            TapDialogButton = new Command(async() =>
            {                
                isDisiplay = false;

                if (DeleteOrRegister)
                {
                    await characterDataStore.DeleteCharacter();
                    Application.Current.MainPage = new GC01();
                }
                else
                {
                    //遷移方法が(編集か新規登録を確認する)
                    switch (userDataStore.WaytoG02Status)
                    {
                        case UserDataStore.EditOrCreaterCharaStatus.EditChara:
                            var result2 = await characterDataStore.UpdateCharacter(CharacteImage, RegisterName, RegisterOwnerName);
                            if (result2)
                            {
                                Application.Current.MainPage = new GC01();
                            }
                            else
                            {
                                isDisiplay = true;
                                DialogSentence = "NAMEをにゅうりょくしてください。";
                                DialogCancelButton = "OK";
                                DialogImage = "Keikoku.png";
                                CanTapDialogButton = false;
                            }
                            break;
                        case UserDataStore.EditOrCreaterCharaStatus.CreateNewChara:
                            var result3 = await characterDataStore.CreateCharacter(CharacteImage, RegisterName, RegisterOwnerName);
                            if (result3)
                            {
                                Application.Current.MainPage = new GC01();
                            }
                            else
                            {
                                isDisiplay = true;
                                DialogSentence = "NAMEをにゅうりょくしてください。";
                                DialogCancelButton = "OK";
                                DialogImage = "Keikoku.png";
                                CanTapDialogButton = false;
                            }
                            break;
                        default:
                            break;
                    }
                }
            });

            TapDialogCancelButton = new Command(() =>
            {
                isDisiplay = false;
            });

            TapLeft = new Command(() =>
            {
                foreach (var x in CaharacterImages.Select((character, index) => new { character, index }))
                {
                    if (x.character.Equals(CharacteImage))
                        if (x.index == 0)
                        {
                            CharacteImage = CaharacterImages.ElementAt(CaharacterImages.Count - 1);
                            break;
                        }
                        else
                        {
                            CharacteImage = CaharacterImages.ElementAt(x.index - 1);
                            break;
                        }
                }
            });

            TapRight = new Command(() =>
            {
                foreach (var x in CaharacterImages.Select((character, index) => new { character, index }))
                {
                    if (x.character.Equals(CharacteImage))
                        if (x.index == CaharacterImages.Count() -1)
                        {
                            CharacteImage = CaharacterImages.ElementAt(0);
                            break;
                        }
                        else
                        {
                            CharacteImage = CaharacterImages.ElementAt(x.index + 1);
                            break;
                        }
                }
            });
        }
    }
}
