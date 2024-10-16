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
            throw new NotImplementedException();
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

        public void UpdateCorporateUser(CorporateUser corporate)
        {
            throw new NotImplementedException();
        }
    }
}