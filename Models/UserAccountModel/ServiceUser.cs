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
                ContactDetails c1 = new ContactDetails() 
                { 
                    Name = "Gaudel", 
                    FirstName = "Vincent", 
                    PhoneNumber = "0675453231" 
                };
                Address a1 = new Address() 
                { 
                    Line1 = "Apt B2", 
                    Line2 = "3 route de Paris", 
                    City = "Clisson", 
                    PostCode = "44190" 
                };
                User u1 = new User() 
                { 
                    UserAccount = serviceUserAccount.GetUserAccountByID(11), 
                    ContactDetails = c1, 
                    Address = a1 
                };
                u1.UserAccount.UserAccountLevel = UserAccountLevel.USER;
                InsertUser(u1);
                // User u2
                ContactDetails c2 = new ContactDetails() { Name = "Durand", FirstName = "Julie", PhoneNumber = "0659871234" };
                Address a2 = new Address()
                {
                    Line1 = "Building C",
                    Line2 = "10 rue des Fleurs",
                    City = "Nantes",
                    PostCode = "44000"
                };
                User u2 = new User()
                {
                    UserAccount = serviceUserAccount.GetUserAccountByID(12),
                    ContactDetails = c2,
                    Address = a2
                };
                u2.UserAccount.UserAccountLevel = UserAccountLevel.USER;
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
                    UserAccount = serviceUserAccount.GetUserAccountByID(13),
                    ContactDetails = c3,
                    Address = a3
                };
                u3.UserAccount.UserAccountLevel = UserAccountLevel.USER;
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
                    UserAccount = serviceUserAccount.GetUserAccountByID(14),
                    ContactDetails = c4,
                    Address = a4
                };
                u4.UserAccount.UserAccountLevel = UserAccountLevel.USER;
                InsertUser(u4);
            
                // Admin adm1
                ContactDetails c5 = new ContactDetails()
                {
                    Name = "CAYLUS",
                    FirstName = "Quentin",
                    PhoneNumber = "0675453231"
                };
                Address a5 = new Address()
                {
                    Line1 = "Apt B2",
                    Line2 = "3 route de Paris",
                    City = "Clisson",
                    PostCode = "44190"
                };
                User adm1 = new User()
                {
                    UserAccount = serviceUserAccount.GetUserAccountByID(1),
                    ContactDetails = c5,
                    Address = a5
                };
                adm1.UserAccount.UserAccountLevel = UserAccountLevel.ADMIN;
                InsertUser(adm1);

                // Admin adm2
                ContactDetails c6 = new ContactDetails()
                {
                    Name = "COSTABELLO",
                    FirstName = "Florent ",
                    PhoneNumber = "0659871234"
                };
                Address a6 = new Address()
                {
                    Line1 = "Building C",
                    Line2 = "10 rue des Fleurs",
                    City = "Nantes",
                    PostCode = "44000"
                };
                User adm2 = new User()
                {
                    UserAccount = serviceUserAccount.GetUserAccountByID(2),
                    ContactDetails = c6,
                    Address = a6
                };
                adm2.UserAccount.UserAccountLevel = UserAccountLevel.ADMIN;
                InsertUser(adm2);

                // Admin adm3
                ContactDetails c7 = new ContactDetails()
                {
                    Name = "Dupont", 
                    FirstName = "Prune",
                    PhoneNumber = "0789124567"
                };
                Address a7 = new Address()
                {
                    Line1 = "Villa Solange",
                    Line2 = "7 avenue de la Mer",
                    City = "La Baule",
                    PostCode = "44500"
                };
                User adm3 = new User()
                {
                    UserAccount = serviceUserAccount.GetUserAccountByID(3),
                    ContactDetails = c7,
                    Address = a7
                };
                adm3.UserAccount.UserAccountLevel = UserAccountLevel.ADMIN;
                InsertUser(adm3);

                // Admin adm4
                ContactDetails c8 = new ContactDetails()
                {
                    Name = "ROY",
                    FirstName = "Alexia",
                    PhoneNumber = "0634567890"
                };
                Address a8 = new Address()
                {
                    Line1 = "Maison Verte",
                    Line2 = "25 chemin des Vignes",
                    City = "Sautron",
                    PostCode = "44880"
                };
                User adm4 = new User()
                {
                    UserAccount = serviceUserAccount.GetUserAccountByID(4),
                    ContactDetails = c8,
                    Address = a8
                };
                adm4.UserAccount.UserAccountLevel = UserAccountLevel.ADMIN;
                InsertUser(adm4);

                // Admin adm5
                ContactDetails c9 = new ContactDetails()
                {
                    Name = "TIROIR",
                    FirstName = "Morgane",
                    PhoneNumber = "0634567890"
                };
                Address a9 = new Address()
                {
                    Line1 = "Maison Verte",
                    Line2 = "25 chemin des Vignes",
                    City = "Sautron",
                    PostCode = "44880"
                };
                User adm5 = new User()
                {
                    UserAccount = serviceUserAccount.GetUserAccountByID(15),
                    ContactDetails = c9,
                    Address = a9
                };
                adm5.UserAccount.UserAccountLevel = UserAccountLevel.ADMIN;
                InsertUser(adm5);

                // Volunteer v1
                ContactDetails vc1 = new ContactDetails()
                {
                    Name = "BOUTIN",
                    FirstName = "Clara",
                    PhoneNumber = "0671234567"
                };
                Address va1 = new Address()
                {
                    Line1 = "Résidence des Roses",
                    Line2 = "15 boulevard des Jardins",
                    City = "Nantes",
                    PostCode = "44000"
                };
                User v1 = new User()
                {
                    UserAccount = serviceUserAccount.GetUserAccountByID(16),
                    ContactDetails = vc1,
                    Address = va1
                };
                v1.UserAccount.UserAccountLevel = UserAccountLevel.VOLUNTEER;
                InsertUser(v1);

                // Volunteer v2
                ContactDetails vc2 = new ContactDetails()
                {
                    Name = "DUPUIS",
                    FirstName = "Maxime",
                    PhoneNumber = "0645671234"
                };
                Address va2 = new Address()
                {
                    Line1 = "Appartement 7B",
                    Line2 = "3 rue du Château",
                    City = "Clisson",
                    PostCode = "44190"
                };
                User v2 = new User()
                {
                    UserAccount = serviceUserAccount.GetUserAccountByID(17),
                    ContactDetails = vc2,
                    Address = va2
                };
                v2.UserAccount.UserAccountLevel = UserAccountLevel.VOLUNTEER;
                InsertUser(v2);
            }
        }

        public void InsertUser(User user)
        {
            // Since a User can have three roles, the UserAccount.UserAccountLevel needs to be set before calling this
            user.UserAccount.DateLastModified = DateTime.Now;
            user.ContactDetails.Name = user.ContactDetails.Name.ToUpper();
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