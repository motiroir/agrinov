using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgriNov.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public UserAccount UserAccount { get; set; }
        
        public CompanyDetails CompanyDetails { get; set;}
        public ContactDetails ContactDetails { get; set;}
        public Address Address { get; set; }
        public byte[] ProofDocument { get; set; }
        public DateTime DateProofLastReviewed { get; set; }
    }
}