using Microsoft.EntityFrameworkCore;

namespace AgriNov.Models
{
    [Owned]
    public class Payment
    {
        public DateTime Date {get; set;}
        public PaymentType PaymentType {get; set;}
        public bool Received {get; set;}
    }
}