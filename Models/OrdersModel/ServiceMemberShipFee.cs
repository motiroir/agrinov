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
            MemberShipFee m1 = new MemberShipFee() { UserAccountId = 1, WasPaid = true };
            InsertMembershipFee(m1);
            MemberShipFee m2 = new MemberShipFee() { UserAccountId = 2, WasPaid = true };
            InsertMembershipFee(m2);

        }

        public void InsertMembershipFee(MemberShipFee memberShipFee)
        {
            _DBContext.MembershipFees.Add(memberShipFee);
            Save();
        }

        public void UpdateMemberShipFee(MemberShipFee memberShipFee)
        {
            MemberShipFee oldMemberShipFee = _DBContext.MembershipFees.FirstOrDefault(m => m.Id == memberShipFee.Id);
            if (oldMemberShipFee != null)
            {
                _DBContext.Entry(oldMemberShipFee).CurrentValues.SetValues(memberShipFee);
                Save();
            }
            return;
        }


        public void Save()
        {
            _DBContext.SaveChanges();
        }
    }
}