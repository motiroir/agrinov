using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgriNov.Models;

namespace AgriNov.ViewModels
{
    public class UserAccountInfoUpdate
    {
        public int UserAccountId {get; set;}
        public string Mail { get; set; }
        public string ProfilePic { get; set; }
        public UserAccountLevel UserAccountLevel {get; set;}
        public Address Address {get; set;}
        public ContactDetails ContactDetails {get; set;}
        public CompanyDetails CompanyDetails {get; set;}
        public IFormFile PdfFile {get; set;}
        public User User {get; set;}
        public CorporateUser CorporateUser {get; set;}
        public Supplier Supplier {get; set;}
    }
}