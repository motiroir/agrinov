using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgriNov.Models
{
    public class CorporateUser
    {
      public int Id {get; set;}
       public UserAccount UserAccount { get; set; }
        public ContactDetails ContactDetails { get; set;}
        public Address Address { get; set; }
      public CompanyDetails CompanyDetails { get; set;}
      [DisplayName("Nombre de souscriptions maximum à des contrats paniers souhaité")]
      public int MaxBoxContractSubscription { get; set;}
      [DisplayName("Nombre d'inscriptions maximum par activité souhaité")]
      public int MaxActivitiesSignUp { get; set;}
    }
}