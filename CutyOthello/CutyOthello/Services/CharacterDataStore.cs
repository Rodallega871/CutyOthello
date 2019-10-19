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
               new Character(id:0,dogType:"A",dogColor:"0",dogName:"Hanako",dogOwnerName:"A"),
               new Character(id:1,dogType:"B",dogColor:"0",dogName:"Taro",dogOwnerName:"A"),
               new Character(id:2,dogType:"C",dogColor:"0",dogName:"Suguru",dogOwnerName:"B"),
               new Character(id:3,dogType:"D",dogColor:"0",dogName:"Inumi",dogOwnerName:"B"),
               new Character(id:4,dogType:"E",dogColor:"0",dogName:"Inuro",dogOwnerName:"C"),
               new Character(id:5,dogType:"E",dogColor:"0",dogName:"Inuro2",dogOwnerName:"C"),
               new Character(id:6,dogType:"E",dogColor:"0",dogName:"Inuro3",dogOwnerName:"C"),
               new Character(id:7,dogType:"E",dogColor:"0",dogName:"Inuro4",dogOwnerName:"C"),
               new Character(id:8,dogType:"E",dogColor:"0",dogName:"Inuro5",dogOwnerName:"C"),
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
