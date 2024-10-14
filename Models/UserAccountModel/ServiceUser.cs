using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgriNov.Models.UserAccountModel;

namespace AgriNov.Models
{
    public class ServiceUser : IServiceUser
    {
        private BDDContext _DBContext;

        public ServiceUser()
        {
            _DBContext = new BDDContext();
        }
        public void CreateDeleteDatabase()
        {
            _DBContext.Database.EnsureDeleted();
            _DBContext.Database.EnsureCreated();
        }

        public void DeleteUserAccount(int userID)
        {
            User user = _DBContext.Users.Find(userID);
            if (user != null)
            {
                this._DBContext.Users.Remove(user);
            }
            this.Save();
        }

        public void Dispose()
        {
            _DBContext.Dispose();
        }

        public User GetUserByID(int userID)
        {
            return _DBContext.Users.Find(userID);
        }

        public User GetUserByID(string userIDStr)
        {
            int id;
            if (int.TryParse(userIDStr, out id))
            {
                return GetUserByID(id);
            }
            return null;
        }

        public List<User> GetUsers()
        {
            return _DBContext.Users.ToList();
        }

        public void InitializeTable()
        {
            throw new NotImplementedException();
        }

        public void InsertUser(User user)
        {
            user.UserAccount.DateLastModified = DateTime.Now;
            user.UserAccount.UserAccountLevel = UserAccountLevel.USER;
            _DBContext.Users.Add(user);
            Save();
        }

        public void Save()
        {
            _DBContext.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

     
    }
}