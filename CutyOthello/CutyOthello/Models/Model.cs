using System;
using System.Collections.Generic;
using System.Text;

namespace CutyOthello.Models
{
    public class Model
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }
        
    }
}
