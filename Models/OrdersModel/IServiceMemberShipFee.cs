using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgriNov.Models
{
    public interface IServiceMemberShipFee : IDisposable
    {
        void InitializeTable();
        void InsertMembershipFee(MemberShipFee memberShipFee);
        void Save();
    }
}