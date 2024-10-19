using AgriNov.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace AgriNov
{
    public class ServiceUserAccount : IServiceUserAccount
    {
        private BDDContext _DBContext;

        public ServiceUserAccount()
        {
            _DBContext = new BDDContext();
        }
        public UserAccount Authenticate(string mail, string password)
        {
            String passwordEncrypted = EncodeMD5(password);
            UserAccount userAccount = this._DBContext.UserAccounts.FirstOrDefault(u => (u.Mail == mail) && (u.Password == passwordEncrypted));
            return userAccount;
        }

        public void CreateDeleteDatabase()
        {
            _DBContext.Database.EnsureDeleted();
            _DBContext.Database.EnsureCreated();
        }

        public void DeleteUserAccount(int userAccountID)
        {
            UserAccount userAccount = _DBContext.UserAccounts.Find(userAccountID);
            if (userAccount != null)
            {
                this._DBContext.UserAccounts.Remove(userAccount);
            }
            this.Save();
        }

        public void Dispose()
        {
            _DBContext.Dispose();
        }

        public UserAccount GetUserAccountByID(int userAccountID)
        {
            return this._DBContext.UserAccounts.Find(userAccountID);
        }

        public UserAccount GetUserAccountByIDEager(int userAccountID)
        {
            return this._DBContext.UserAccounts.Include("User").Include("CorporateUser").Include("Supplier").FirstOrDefault(u => u.Id == userAccountID);
        }

        public UserAccount GetUserAccountByID(string userAccountIDStr)
        {
            int id;
            if (int.TryParse(userAccountIDStr, out id))
            {
                return this.GetUserAccountByID(id);
            }
            return null;
        }

        public UserAccount GetUserAccountByIDEager(string userAccountIDStr)
        {
            int id;
            if (int.TryParse(userAccountIDStr, out id))
            {
                return this.GetUserAccountByIDEager(id);
            }
            return null;
        }

        public List<UserAccount> GetUserAccounts()
        {
            return _DBContext.UserAccounts.ToList();
        }

        public void InitializeTable()
        {
            UserAccount u1 = new UserAccount() { Mail = Environment.GetEnvironmentVariable("USER_MAIL_1"), Password = Environment.GetEnvironmentVariable("USER_PASSWORD_1") };
            InsertUserAccount(u1);
            UserAccount u2 = new UserAccount()
            {
                Mail = Environment.GetEnvironmentVariable("USER_MAIL_2"),
                Password = Environment.GetEnvironmentVariable("USER_PASSWORD_2")
            };
            InsertUserAccount(u2);

            UserAccount u3 = new UserAccount()
            {
                Mail = Environment.GetEnvironmentVariable("USER_MAIL_3"),
                Password = Environment.GetEnvironmentVariable("USER_PASSWORD_3")
            };
            InsertUserAccount(u3);

            UserAccount u4 = new UserAccount()
            {
                Mail = Environment.GetEnvironmentVariable("USER_MAIL_4"),
                Password = Environment.GetEnvironmentVariable("USER_PASSWORD_4")
            };
            InsertUserAccount(u4);

            UserAccount u5 = new UserAccount()
            {
                Mail = Environment.GetEnvironmentVariable("USER_MAIL_5"),
                Password = Environment.GetEnvironmentVariable("USER_PASSWORD_5")
            };
            InsertUserAccount(u5);

            UserAccount u6 = new UserAccount()
            {
                Mail = Environment.GetEnvironmentVariable("USER_MAIL_6"),
                Password = Environment.GetEnvironmentVariable("USER_PASSWORD_6")
            };
            InsertUserAccount(u6);

            UserAccount u7 = new UserAccount()
            {
                Mail = Environment.GetEnvironmentVariable("USER_MAIL_7"),
                Password = Environment.GetEnvironmentVariable("USER_PASSWORD_7")
            };
            InsertUserAccount(u7);

            UserAccount u8 = new UserAccount()
            {
                Mail = Environment.GetEnvironmentVariable("USER_MAIL_8"),
                Password = Environment.GetEnvironmentVariable("USER_PASSWORD_8")
            };
            InsertUserAccount(u8);

            UserAccount u9 = new UserAccount()
            {
                Mail = Environment.GetEnvironmentVariable("USER_MAIL_9"),
                Password = Environment.GetEnvironmentVariable("USER_PASSWORD_9")
            };
            InsertUserAccount(u9);

            UserAccount u10 = new UserAccount()
            {
                Mail = Environment.GetEnvironmentVariable("USER_MAIL_10"),
                Password = Environment.GetEnvironmentVariable("USER_PASSWORD_10")
            };
            InsertUserAccount(u10);
        }

        public void InsertUserAccount(UserAccount userAccount)
        {
            userAccount.Password = EncodeMD5(userAccount.Password);
            userAccount.UserAccountLevel = UserAccountLevel.DEFAULT;
            userAccount.DateCreated = DateTime.Now;
            userAccount.DateLastModified = DateTime.Now;
            _DBContext.UserAccounts.Add(userAccount);
            Save();
            //Each UserAccount need one ShoppingCart, but the UserAccountId needs to be generated first
            userAccount.ShoppingCart = new ShoppingCart();
            UpdateUserAccountExceptPassword(userAccount);
        }

        public void Save()
        {
            _DBContext.SaveChanges();
        }

        public void UpdateUserAccountPassword(int userAccountID, string password)
        {
            UserAccount oldUser = this.GetUserAccountByID(userAccountID);
            string newEncryptedPassword = EncodeMD5(password);
            if (oldUser == null)
            {
                return;
            }
            oldUser.Password = newEncryptedPassword;
            oldUser.DateLastModified = DateTime.Now;
            Save();

        }
        public void UpdateUserAccountExceptPassword(UserAccount newUserAccount)
        {
            UserAccount oldUser = this.GetUserAccountByID(newUserAccount.Id);
            if (oldUser == null)
            {
                return;
            }
            newUserAccount.DateLastModified = DateTime.Now;
            _DBContext.Entry(oldUser).CurrentValues.SetValues(newUserAccount);
            Save();
        }

        private string EncodeMD5(string password)
        {
            string passwordWithSalt = "Authentication" + password + "Hash";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(passwordWithSalt)));
        }

        public string GetUserFullName(UserAccount user)
        {
            if (user.UserAccountLevel.ToString() == "USER")
            {
                string lastName = user.User.ContactDetails.Name;
                string firstName = user.User.ContactDetails.FirstName;
                return firstName + " " + lastName;
            }
            if (user.UserAccountLevel.ToString() == "CORPORATE")
            {
                string lastName = user.CorporateUser.ContactDetails.Name;
                string firstName = user.CorporateUser.ContactDetails.FirstName;
                return firstName + " " + lastName;
            }
            if (user.UserAccountLevel.ToString() == "SUPPLIER")
            {
                string lastName = user.Supplier.ContactDetails.Name;
                string firstName = user.Supplier.ContactDetails.FirstName;
                return firstName + " " + lastName;
            }
            /*Activate when volunteer and admin class created
             * if (user.UserAccountLevel.ToString() == "VOLUNTEER")
            {
                string lastName = user.Volunteer.ContactDetails.Name;
                string firstName = user.Volunteer.ContactDetails.FirstName;
                return firstName + lastName;
            }
            if (user.UserAccountLevel.ToString() == "ADMIN")
            {
                string lastName = user.Admin.ContactDetails.Name;
                string firstName = user.Admin.ContactDetails.FirstName;
                return firstName + lastName;
            }*/
            return null;
        }
    }
}