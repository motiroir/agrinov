using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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