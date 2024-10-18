using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AgriNov.Models
{
    public class ServiceMemberShipFee : IServiceMemberShipFee
    {
         private BDDContext _DBContext;

        public ServiceMemberShipFee()
        {
            _DBContext = new BDDContext();
        }
        public void Dispose()
        {
            _DBContext.Dispose();
        }

        public void InitializeTable()
        {
            MemberShipFee m1 = new MemberShipFee() {UserAccountId = 1, Price = 10};
            InsertMembershipFee(m1);
            MemberShipFee m2 = new MemberShipFee() {UserAccountId = 2, Price = 10};
            InsertMembershipFee(m2);

        }

        public void InsertMembershipFee(MemberShipFee memberShipFee)
        {
            _DBContext.MembershipFees.Add(memberShipFee);
            Save();
        }

        public void Save()
        {
            _DBContext.SaveChanges();
        }
    }
}