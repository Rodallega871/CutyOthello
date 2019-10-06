using CutyOthello.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace CutyOthello.Services
{
    public class UserDataStore : ModelDataStore<User>
    {

        public UserDataStore() : base()
        {
        }

        public override Task<bool> Initial()
        {
            models = new ObservableCollection<User>{new User()};

            return Task.FromResult(true);
        }

        public override Task<bool> SelectAllFromDB()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM User");

            models = connextSql.SelectUser(sb,null);

            return Task.FromResult(true);
        }
    }
}
