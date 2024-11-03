using System.ComponentModel;

namespace AgriNov.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public UserAccount UserAccount { get; set; }
        public CompanyDetails CompanyDetails { get; set;}
        public ContactDetails ContactDetails { get; set;}
        public Address Address { get; set; }
        [DisplayName("Justificatif pdf")]
        public byte[] ProofPdfDocument { get; set; }
        public DateTime DateProofLastReviewed { get; set; }
    }
}