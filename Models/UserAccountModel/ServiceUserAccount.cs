using AgriNov.Models;
using System.Security.Cryptography;
using System.Text;
using DotNetEnv;

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

        public UserAccount GetUserAccountByID(string userAccountIDStr)
        {
            int id;
            if (int.TryParse(userAccountIDStr, out id))
            {
                return this.GetUserAccountByID(id);
            }
            return null;
        }

        public List<UserAccount> GetUserAccounts()
        {
            return _DBContext.UserAccounts.ToList();
        }

        public void InitializeTable()
        {
            UserAccount u1 = new UserAccount() { Mail = Environment.GetEnvironmentVariable("USER_MAIL_1"), Password = Environment.GetEnvironmentVariable("USER_PASSWORD_1")};
            InsertUserAccount(u1);
        }

        public void InsertUserAccount(UserAccount userAccount)
        {
            userAccount.Password = EncodeMD5(userAccount.Password);
            userAccount.UserAccountLevel = UserAccountLevel.DEFAULT;
            userAccount.DateCreated = DateTime.Now;
            userAccount.DateLastModified = DateTime.Now;
            _DBContext.UserAccounts.Add(userAccount);
            Save();
        }

        public void Save()
        {
            _DBContext.SaveChanges();
        }

        public void UpdateUserAccountPassword(int userAccountID, string password){
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
    }
}