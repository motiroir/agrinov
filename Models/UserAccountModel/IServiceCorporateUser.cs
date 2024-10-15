using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgriNov.Models
{
    public interface IServiceCorporateUser : IDisposable
    {
        void CreateDeleteDatabase();
        void InitializeTable();
        List<CorporateUser> GetCorporateUsers();
        CorporateUser GetCorporateUserByID(int corporateUserID);
        CorporateUser GetCorporateUserByID(string corporateUserIDStr);
        void InsertCorporateUser(CorporateUser corporateUser);
        void UpdateCorporateUser(CorporateUser corporate);
        void DeleteCorporateUser(int corporateUserID);
        void Save();
    }
}