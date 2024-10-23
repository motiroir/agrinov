namespace AgriNov.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int UserAccountId { get; set; }
        public UserAccount UserAccount { get; set; }

        public int ActivityId { get; set; }
        public Activity Activity { get; set; }

        public DateTime DateReserved { get; set; }
    }
}
