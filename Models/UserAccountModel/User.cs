namespace AgriNov.Models
{
    public class User
    {
        public int Id { get; set; }
        public UserAccount UserAccount { get; set; }
        public ContactDetails ContactDetails { get; set;}
        public Address Address { get; set; }
    }
}