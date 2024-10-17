using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public void DeleteUser(int userID)
        {
            //Is cascade delete with useraccount ok?
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
            using (IServiceUserAccount serviceUserAccount = new ServiceUserAccount())
            {
                //User u1
                ContactDetails c1 = new ContactDetails() { Name = "Gaudel", FirstName = "Vincent", PhoneNumber = "0675453231" };
                Address a1 = new Address() { Line1 = "Apt B2", Line2 = "3 route de Paris", City = "Clissons", PostCode = "44190" };
                User u1 = new User() { UserAccount = serviceUserAccount.GetUserAccountByID(1), ContactDetails = c1, Address = a1 };
                InsertUser(u1);
                // User u2
                ContactDetails c2 = new ContactDetails() { Name = "Durand", FirstName = "Julie", PhoneNumber = "0659871234"};
                Address a2 = new Address()
                {
                    Line1 = "Building C",
                    Line2 = "10 rue des Fleurs",
                    City = "Nantes",
                    PostCode = "44000"
                };
                User u2 = new User()
                {
                    UserAccount = serviceUserAccount.GetUserAccountByID(2),
                    ContactDetails = c2,
                    Address = a2
                };
                InsertUser(u2);

                // User u3
                ContactDetails c3 = new ContactDetails()
                {
                    Name = "Martin",
                    FirstName = "Pierre",
                    PhoneNumber = "0789124567"
                };
                Address a3 = new Address()
                {
                    Line1 = "Villa Solange",
                    Line2 = "7 avenue de la Mer",
                    City = "La Baule",
                    PostCode = "44500"
                };
                User u3 = new User()
                {
                    UserAccount = serviceUserAccount.GetUserAccountByID(3),
                    ContactDetails = c3,
                    Address = a3
                };
                InsertUser(u3);

                // User u4
                ContactDetails c4 = new ContactDetails()
                {
                    Name = "Lefevre",
                    FirstName = "Marie",
                    PhoneNumber = "0634567890"
                };
                Address a4 = new Address()
                {
                    Line1 = "Maison Verte",
                    Line2 = "25 chemin des Vignes",
                    City = "Sautron",
                    PostCode = "44880"
                };
                User u4 = new User()
                {
                    UserAccount = serviceUserAccount.GetUserAccountByID(4),
                    ContactDetails = c4,
                    Address = a4
                };
                InsertUser(u4);
            }

        }

        public void InsertUser(User user)
        {
            user.UserAccount.DateLastModified = DateTime.Now;
            user.UserAccount.UserAccountLevel = UserAccountLevel.USER;
            //_Update and track the user account that is part of user
            _DBContext.UserAccounts.Update(user.UserAccount);
            _DBContext.Users.Add(user);
            Save();
        }

        public void Save()
        {
            _DBContext.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            user.UserAccount.DateLastModified = DateTime.Now;
            _DBContext.UserAccounts.Update(user.UserAccount);
            _DBContext.Users.Update(user);
            Save();
        }


    }
}