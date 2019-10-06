using System;
using System.Collections.Generic;
using System.Text;

namespace CutyOthello.Models
{
    public class Character : Model    
    {
        public string DogType { get;  set; }
        public string DogColor { get;  set; }
        public string DogName { get;  set; }
        public string DogOwnerName { get;  set; }

        public Character()
        {
            //SQLite使用時に必要
        }

        public Character(int id, string dogType, string dogColor, string dogName, string dogOwnerName)
        {
            Id = id;
            DogType = dogType;
            DogColor = dogColor;
            DogName = dogName;
            DogOwnerName = dogOwnerName;
        }
    }
}
