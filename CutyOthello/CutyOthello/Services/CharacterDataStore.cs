using CutyOthello.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace CutyOthello.Services
{
    public class CharacterDataStore : ModelDataStore<Character>
    {
        public CharacterDataStore() : base()
        {
        }

        public override Task<bool> Initial()
        {
            models = new ObservableCollection<Character>
            {
               new Character(id:0,dogType:"A",dogColor:"0",dogName:"花子",dogOwnerName:"飼い主A"),
               new Character(id:1,dogType:"B",dogColor:"0",dogName:"太郎",dogOwnerName:"飼い主A"),
               new Character(id:2,dogType:"C",dogColor:"0",dogName:"卓",dogOwnerName:"飼い主B"),
               new Character(id:3,dogType:"D",dogColor:"0",dogName:"犬美",dogOwnerName:"飼い主B"),
               new Character(id:4,dogType:"E",dogColor:"0",dogName:"犬郎",dogOwnerName:"飼い主C"),
            };
            return Task.FromResult(true);
        }

        public override Task<bool> SelectAllFromDB()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM Character");

            models = connextSql.SelectCharacter(sb, null);

            return Task.FromResult(true);
        }

    }
}
