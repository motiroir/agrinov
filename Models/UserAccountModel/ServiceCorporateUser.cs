using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgriNov.Models
{
    public class ServiceCorporateUser : IServiceCorporateUser
    {
        private BDDContext _DBContext;

        public ServiceCorporateUser()
        {
            _DBContext = new BDDContext();
        }
        public void CreateDeleteDatabase()
        {
            _DBContext.Database.EnsureDeleted();
            _DBContext.Database.EnsureCreated();
        }

        public void DeleteCorporateUser(int corporateUserID)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _DBContext.Dispose();
        }

        public CorporateUser GetCorporateUserByID(int corporateUserID)
        {
            return _DBContext.CorporateUsers.Find(corporateUserID);
        }

        public CorporateUser GetCorporateUserByID(string corporateUserIDStr)
        {
            int id;
            if (int.TryParse(corporateUserIDStr, out id))
            {
                return GetCorporateUserByID(id);
            }
            return null;
        }

        public List<CorporateUser> GetCorporateUsers()
        {
            return _DBContext.CorporateUsers.ToList();
        }

        public void InitializeTable()
        {
            using (IServiceUserAccount serviceUserAccount = new ServiceUserAccount())
            {
                // Corporate User c9
                Address a9 = new Address() { Line1 = "Etage 27", Line2 = "Avenue Thiers", City = "Nantes", PostCode = "44000" };
                ContactDetails c9 = new ContactDetails() { Name = "Dupont", FirstName = "Sacha", PhoneNumber = "0674453281" };
                CompanyDetails cd9 = new CompanyDetails() { CompanyName = "Total Energies", SiretNumber = "0145675432075411" };
                CorporateUser cu9 = new CorporateUser() { UserAccount = serviceUserAccount.GetUserAccountByID(9), Address = a9, ContactDetails = c9, CompanyDetails = cd9 };
                InsertCorporateUser(cu9);

                // Corporate User cu10
                Address a10 = new Address()
                {
                    Line1 = "Bureau 15",
                    Line2 = "Boulevard Victor Hugo",
                    City = "Paris",
                    PostCode = "75008"
                };
                ContactDetails c10 = new ContactDetails()
                {
                    Name = "Perrin",
                    FirstName = "François",
                    PhoneNumber = "0654321890"
                };
                CompanyDetails cd10 = new CompanyDetails()
                {
                    CompanyName = "BNP Paribas",
                    SiretNumber = "98765432100123"
                };
                CorporateUser cu10 = new CorporateUser()
                {
                    UserAccount = serviceUserAccount.GetUserAccountByID(10),
                    Address = a10,
                    ContactDetails = c10,
                    CompanyDetails = cd10
                };
                InsertCorporateUser(cu10);
            }
        }

        public void InsertCorporateUser(CorporateUser corporateUser)
        {
            corporateUser.UserAccount.DateLastModified = DateTime.Now;
            corporateUser.UserAccount.UserAccountLevel = UserAccountLevel.CORPORATE;
            //_Update and track the user account that is part of user
            _DBContext.UserAccounts.Update(corporateUser.UserAccount);
            _DBContext.CorporateUsers.Add(corporateUser);
            Save();
        }

        public void Save()
        {
            _DBContext.SaveChanges();
        }

        public void UpdateCorporateUser(CorporateUser corporateUser)
        {
            corporateUser.UserAccount.DateLastModified = DateTime.Now;
            _DBContext.UserAccounts.Update(corporateUser.UserAccount);
            _DBContext.CorporateUsers.Update(corporateUser);
            Save();
        }
    }
}