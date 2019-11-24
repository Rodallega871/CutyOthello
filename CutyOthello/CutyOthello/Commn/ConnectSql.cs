using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using CutyOthello.Models;
using CutyOthello.ViewModels;
using SQLite;

namespace CutyOthello.Commn
{
    public class ConnectSql<T> where T : Model
    {
        public string DbPath { get; private set; }

        public ConnectSql()
        {
            DbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CutyOthello.db");
        }

        public void CreateTable()
        {
            using (var db = new SQLiteConnection(DbPath))
            {
                db.CreateTable<T>();
            }
        }

        public void Insert(T model)
        {
            using (var db = new SQLite.SQLiteConnection(DbPath))
            {
                db.Insert(model);
            }
        }

        public void InsertAll(ObservableCollection<T> model)
        {
            using (var db = new SQLite.SQLiteConnection(DbPath))
            {
                db.InsertAll(model);
            }
        }

        public void Update(Model model)
        {
            using (var db = new SQLite.SQLiteConnection(DbPath))
            {
                db.Update(model);
            }
        }

        public void Delete(Model model)
        {
            using (var db = new SQLite.SQLiteConnection(DbPath))
            {
                db.Delete(model);
            }
        }

        public ObservableCollection<User> SelectUser(StringBuilder builder, string[] StringHiretu)
        {
            ObservableCollection<User> model = new ObservableCollection<User>();

            using (var db = new SQLite.SQLiteConnection(DbPath))
            {
                SQLiteCommand command = new SQLiteCommand(db);
                command.CommandText = builder.ToString();
                command.ExecuteQuery<User>().ForEach(user => { model.Add(user); });
            }
            return model;
        }

        public ObservableCollection<Character> SelectCharacter(StringBuilder builder, string[] StringHiretu)
        {
            ObservableCollection<Character> model = new ObservableCollection<Character>();

            using (var db = new SQLite.SQLiteConnection(DbPath))

            {
                SQLiteCommand command = new SQLiteCommand(db);
                command.CommandText = builder.ToString();
                command.ExecuteQuery<Character>().ForEach(character => { model.Add(character); });
            }
            return model;
        }
    }
}
