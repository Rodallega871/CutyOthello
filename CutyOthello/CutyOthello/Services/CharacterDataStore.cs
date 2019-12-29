using CutyOthello.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CutyOthello.Services
{
    public class CharacterDataStore : ModelDataStore<Character>
    {
        //表示用キャラクター保管リスト
        ObservableCollection<Character> tmpcharacters;
        //キャラクターの画像情報保管リスト
        List<String> CharacterImages;
        //エディット用キャラクター保管
        public Character TempEditCharacter { get; set; }
        //対戦用キャラクター保管
        public Character PlayerOne { get; set; }
        //対戦用キャラクター保管
        public Character PlayerTwo { get; set; }
        //対戦用キャラクター取得石数
        public int PlayerOneCount { get; set; }
        //対戦用キャラクター取得石数
        public int PlayerTwoCount { get; set; }
        //CPUの対戦レベル
        public int CPULevelInfo { get; set; }

        public Enum SurrenderJudge { get; set; }

        //降参者確認用
        public enum SurrenderStatus
        {
            DidntSurrernder,SurrenderPlayerOne,SurrenderPlayerTwo
        }

        public CharacterDataStore() : base()
        {
            tmpcharacters = new ObservableCollection<Character> { };
        }

        public override Task<bool> Initial()
        {
            models = new ObservableCollection<Character>
            {
                new Character{Id = 0,DogType = "A",DogColor = "0",DogName = "Taro",DogOwnerName = "A",DogPicture = "shiba.png",IsDisplay = false},
                new Character{Id = 1,DogType = "B",DogColor = "0",DogName = "Hanako",DogOwnerName = "B",DogPicture = "poodle.png",IsDisplay = false},
                new Character{Id = 2,DogType = "C",DogColor = "0",DogName = "Suguru",DogOwnerName = "C",DogPicture = "buldogSample.png",IsDisplay = false},
                new Character{Id = 3,DogType = "D",DogColor = "0",DogName = "Piglet",DogOwnerName = "D",DogPicture = "PIG.png",IsDisplay = false},
                //new Character{Id = 4,DogType = "E",DogColor = "0",DogName = "Kirin",DogOwnerName = "E",DogPicture = "Giraffe.png",IsDisplay = false},
                new Character{Id = 5,DogType = "F",DogColor = "0",DogName = "Ele",DogOwnerName = "F",DogPicture = "Eephant.png",IsDisplay = false},
                //new Character{Id = 6,DogType = "G",DogColor = "0",DogName = "CowCow",DogOwnerName = "G",DogPicture = "Cow.png",IsDisplay = false},
                //new Character{Id = 8,DogType = "H",DogColor = "0",DogName = "Robo",DogOwnerName = "I",DogPicture = "Robo.png",IsDisplay = false},
                //new Character{Id = 9,DogType = "I",DogColor = "0",DogName = "Master",DogOwnerName = "J",DogPicture = "OthlloMaster.png",IsDisplay = false}
                //テスト用
               //,new Character(id:10,dogType:"I",dogColor:"0",dogName:"ZEN",dogOwnerName:"J",dogPicture:"shiba.png",true)
               //,new Character(id:11,dogType:"I",dogColor:"0",dogName:"Cotton",dogOwnerName:"J",dogPicture:"poodle.png",true)
            };
            return Task.FromResult(true);
        }

        public List<string> GetCharacterImages()
        {
            CharacterImages = new List<string>
            {
                "shiba.png","poodle.png","buldogSample.png","PIG.png",
                /*"Giraffe.png" ,*/ "Eephant.png","Cow.png"
                ,/*"Robo.png","OthlloMaster.png"*/
            };

            return CharacterImages;
        }

        public ObservableCollection<Character> GetDisplayItem()
        {
            tmpcharacters.Clear();
            models.ToList().ForEach((model) => { if (model.IsDisplay == true) tmpcharacters.Add(model); });
            return tmpcharacters;
        }


        public override Task<bool> SelectAllFromDB()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM Character");

            models = connextSql.SelectCharacter(sb, null);

            return Task.FromResult(true);
        }

        /// <summary>
        /// キャラクター削除処理
        /// </summary>
        public async Task<bool> DeleteCharacter()
        {
            models.Remove(TempEditCharacter);
            return await DeleteDB(TempEditCharacter);
        }


        public async Task<bool> CreateCharacter(string characteImage, string registerName, string registerOwnerName)
        {
            if (!ValidationRegister(registerName, registerOwnerName))
            {
                TempEditCharacter = new Character
                {
                    DogName = registerName,
                    DogOwnerName = registerOwnerName,
                    DogPicture = characteImage,
                    IsDisplay = true,
                    Id = 0,
                    DogColor = "",
                    DogType = ""
                };
                await InsertToDB(TempEditCharacter);
                models.Add(TempEditCharacter);
                TempEditCharacter = null;
                return true;
            }
            else
            {
                return false;
            }
        }

        internal async Task<bool> UpdateCharacter(string characteImage, string registerName, string registerOwnerName)
        {
            if (!ValidationRegister(registerName, registerOwnerName))
            {
                TempEditCharacter.DogName = registerName;
                TempEditCharacter.DogOwnerName = registerOwnerName;
                TempEditCharacter.DogPicture = characteImage;
                await UpdateDB(TempEditCharacter);
                UpdateItemAsync(TempEditCharacter);
                TempEditCharacter = null;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 登録名と登録オーナー名の入力チェックを行う。
        /// </summary>
        /// <param name="registerName"></param>
        /// <param name="registerOwnerName"></param>
        /// <returns></returns>
        public bool ValidationRegister(string registerName, string registerOwnerName)
        {
            //string checkPettern = @"^\p{IsHiragana}+$";
            //Match matchName = Regex.Match(registerName, checkPettern);
            //if (!matchName.Success) return false;
            //Match matchOwnerName = Regex.Match(registerOwnerName, checkPettern);
            //if (!matchOwnerName.Success) return false;

            return String.IsNullOrEmpty(registerName);
        }
    }
}
