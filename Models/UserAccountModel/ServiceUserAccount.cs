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

        public List<UserAccount> GetUserAccountsFull()
        {
            return _DBContext.UserAccounts.Include("User").Include("CorporateUser").Include("Supplier").ToList();
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
            UserAccount u11 = new UserAccount()
            {
                Mail = Environment.GetEnvironmentVariable("USER_MAIL_11"),
                Password = Environment.GetEnvironmentVariable("USER_PASSWORD_11")
            };
            InsertUserAccount(u11);

            UserAccount u12 = new UserAccount()
            {
                Mail = Environment.GetEnvironmentVariable("USER_MAIL_12"),
                Password = Environment.GetEnvironmentVariable("USER_PASSWORD_12")
            };
            InsertUserAccount(u12);

            UserAccount u13 = new UserAccount()
            {
                Mail = Environment.GetEnvironmentVariable("USER_MAIL_13"),
                Password = Environment.GetEnvironmentVariable("USER_PASSWORD_13")
            };
            InsertUserAccount(u13);

            UserAccount u14 = new UserAccount()
            {
                Mail = Environment.GetEnvironmentVariable("USER_MAIL_14"),
                Password = Environment.GetEnvironmentVariable("USER_PASSWORD_14")
            };
            InsertUserAccount(u14);

            UserAccount u15 = new UserAccount()
            {
                Mail = Environment.GetEnvironmentVariable("USER_MAIL_15"),
                Password = Environment.GetEnvironmentVariable("USER_PASSWORD_15")
            };
            InsertUserAccount(u15);

            UserAccount u16 = new UserAccount()
            {
                Mail = Environment.GetEnvironmentVariable("USER_MAIL_16"),
                Password = Environment.GetEnvironmentVariable("USER_PASSWORD_16")
            };
            InsertUserAccount(u16);

            UserAccount u17 = new UserAccount()
            {
                Mail = Environment.GetEnvironmentVariable("USER_MAIL_17"),
                Password = Environment.GetEnvironmentVariable("USER_PASSWORD_17")
            };
            InsertUserAccount(u17);

            UserAccount u18 = new UserAccount()
            {
                Mail = Environment.GetEnvironmentVariable("USER_MAIL_18"),
                Password = Environment.GetEnvironmentVariable("USER_PASSWORD_18")
            };
            InsertUserAccount(u18);

            UserAccount u19 = new UserAccount()
            {
                Mail = Environment.GetEnvironmentVariable("USER_MAIL_19"),
                Password = Environment.GetEnvironmentVariable("USER_PASSWORD_19")
            };
            InsertUserAccount(u19);

            UserAccount u20 = new UserAccount()
            {
                Mail = Environment.GetEnvironmentVariable("USER_MAIL_20"),
                Password = Environment.GetEnvironmentVariable("USER_PASSWORD_20")
            };
            InsertUserAccount(u20);
        }

        public void InsertUserAccount(UserAccount userAccount)
        {
            // random picture
            Random random = new Random();
            int randomNumber = random.Next(1, 19);
            userAccount.ProfilePic = "pic" + randomNumber + ".PNG";

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

        public bool CheckIfMemberShipValid(int userAccountID)
        {
            UserAccount userAccount = _DBContext.UserAccounts.FirstOrDefault(u => u.Id == userAccountID);
            if(userAccount != null &&( userAccount.UserAccountLevel == UserAccountLevel.ADMIN || userAccount.UserAccountLevel == UserAccountLevel.SUPPLIER || userAccount.UserAccountLevel == UserAccountLevel.VOLUNTEER))
            {
                return true;
            }
            DateTime currentDate = DateTime.Now;
            MemberShipFee memberShipFee = _DBContext.MembershipFees.Where(fee => (fee.UserAccountId == userAccountID && fee.WasPaid == true)).OrderByDescending(fee => fee.EndDate).FirstOrDefault();
            if(memberShipFee == null || DateTime.Compare(memberShipFee.EndDate, currentDate) < 0){
                return false;
            }
            return true;
        }

        public string GetUserFullName(UserAccount user)
        {
            if (user.UserAccountLevel.ToString() == "USER" || user.UserAccountLevel.ToString() == "VOLUNTEER" || user.UserAccountLevel.ToString() == "ADMIN")
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