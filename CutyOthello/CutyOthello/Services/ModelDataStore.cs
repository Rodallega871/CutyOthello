using CutyOthello.Commn;
using CutyOthello.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutyOthello.Services
{
    public abstract class ModelDataStore <T> where T : Model
    {
        public ObservableCollection<T> models;
        public ConnectSql<T> connextSql;

        public ModelDataStore()
        {
            connextSql = new Commn.ConnectSql<T>();
            CreateTable();
            SelectAllFromDB();
            if (models.Count == 0)
            {
                Initial();
                InsertAllToDB();
            }
        }


        abstract public Task<bool> Initial();

        public void AddItemAsync(T model)
        {
            models.Add(model);
        }

        public void UpdateItemAsync(T model)
        {
            var oldItem = models.Where((T arg) => arg.Id == model.Id).FirstOrDefault();
            models.Remove(oldItem);
            models.Add(model);
        }

        public void DeleteItemAsync(int id)
        {
            var oldItem = models.Where((T arg) => arg.Id == id).FirstOrDefault();
            models.Remove(oldItem);
        }

        public T GetItemAsync(int id)
        {
            return models.FirstOrDefault(s => s.Id == id);
        }

        public ObservableCollection<T> GetItemsAsync(bool forceRefresh = false)
        {
            return models;
        }


        //SQL接続関連
        abstract public Task<bool> SelectAllFromDB();

        public Task<bool> CreateTable()
        {
            connextSql.CreateTable();

            return Task.FromResult(true);
        }


        public Task<bool> InsertToDB(T model)
        {
            connextSql.Insert(model);

            return Task.FromResult(true);
        }

        public Task<bool> InsertAllToDB()
        {
            connextSql.InsertAll(models);
            return Task.FromResult(true);
        }

        public async Task<bool> UpdateDB(T model)
        {
            connextSql.Update(model);
            return await Task.FromResult(true);
        }

        public Task<bool> DeleteDB(T model)
        {
            connextSql.Delete(model);
            return Task.FromResult(true);
        }
    }
}
