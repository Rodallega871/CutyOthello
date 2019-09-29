using System;
using System.Collections.Generic;
using System.Text;

namespace CutyOthello.Models
{
    class Character : Model    
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime TimeStamp { get; set; }
        public decimal Value { get; set; }
    }
}
