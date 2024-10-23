using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgriNov.Models
{
    public class MemberShipFee
    {
        public int Id {get; set;}
        public DateTime StartDate {get; set;}
        public DateTime EndDate {get; set;}
        public decimal Price {get; set;}
        public int UserAccountId {get; set;}

        public bool WasPaid  {get; set;} //If true, the membership fee has been paid

        public MemberShipFee()
        {
            Price = 10;
            StartDate = DateTime.Now;
            EndDate = StartDate.AddYears(1);
            WasPaid = false;
        }
    }
    
}