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
        public string DogPicture { get; set; }
        public bool IsDisplay { get; set; }
    }
}
