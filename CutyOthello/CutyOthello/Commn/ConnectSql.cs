using System;
using System.Collections.Generic;
using System.Text;
using CutyOthello.Models;
using CutyOthello.ViewModels;
using SQLite;

namespace CutyOthello.Commn
{
    class ConnectSql
    {
        public string DbPath { get; private set; }

        public ConnectSql()
        {
            DbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SQLiteDataBase.db");

            Character character = new Character();
            User user = new User();
            CreateTable(character);
            CreateTable(user);

        }

        public void CreateTable(Model obj)
        {
            using (var db = new SQLite.SQLiteConnection(DbPath))
            {   // Create Table
                db.CreateTable<object>();
            }
        }





    }
}
