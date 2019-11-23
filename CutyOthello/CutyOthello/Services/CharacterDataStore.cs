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

        public CharacterDataStore() : base()
        {
            tmpcharacters = new ObservableCollection<Character> { };
        }

        public override Task<bool> Initial()
        {
            models = new ObservableCollection<Character>
            {
               new Character(id:0,dogType:"A",dogColor:"0",dogName:"Hanako",dogOwnerName:"A",dogPicture:"shiba.png",false),
               new Character(id:1,dogType:"B",dogColor:"0",dogName:"Taro",dogOwnerName:"B",dogPicture:"poodle.png",false),
               new Character(id:2,dogType:"C",dogColor:"0",dogName:"Suguru",dogOwnerName:"C",dogPicture:"buldogSample.png",false),
               new Character(id:3,dogType:"D",dogColor:"0",dogName:"Inumi",dogOwnerName:"D",dogPicture:"PIG.png",false),
               new Character(id:4,dogType:"E",dogColor:"0",dogName:"Kirin",dogOwnerName:"E",dogPicture:"Giraffe.png",false),
               new Character(id:5,dogType:"F",dogColor:"0",dogName:"Ele",dogOwnerName:"F",dogPicture:"Eephant.png",false),
               new Character(id:6,dogType:"G",dogColor:"0",dogName:"CowCow",dogOwnerName:"G",dogPicture:"Cow.png",false),
               new Character(id:8,dogType:"H",dogColor:"0",dogName:"Robo",dogOwnerName:"I",dogPicture:"Robo.png",false),
               new Character(id:9,dogType:"I",dogColor:"0",dogName:"Master",dogOwnerName:"J",dogPicture:"OthlloMaster.png",false)
               //テスト用
               ,new Character(id:10,dogType:"I",dogColor:"0",dogName:"ZEN",dogOwnerName:"J",dogPicture:"shiba.png",true)
               ,new Character(id:11,dogType:"I",dogColor:"0",dogName:"Cotton",dogOwnerName:"J",dogPicture:"poodle.png",true)
            };
            return Task.FromResult(true);
        }

        public List<string> GetCharacterImages()
        {
            CharacterImages = new List<string>
            {
                "shiba.png","poodle.png","buldogSample.png","PIG.png",
                "Giraffe.png","Eephant.png","Cow.png","Robo.png","OthlloMaster.png"
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
                TempEditCharacter = new Character(dogName: registerName, dogOwnerName: registerOwnerName, dogPicture: characteImage,
                    isDisplay: true,id: 0, dogColor: "", dogType: "");
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
            string checkPettern = @"^\p{IsHiragana}+$";

            Match matchName = Regex.Match(registerName, checkPettern);
            if (!matchName.Success) return false;

            Match matchOwnerName = Regex.Match(registerOwnerName, checkPettern);
            if (!matchOwnerName.Success) return false;

            return true;
        }
    }
}
