using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using AgriNov.Models;

namespace AgriNov.ViewModels
{
    public class UserAccountCreation
    {
        public UserAccount UserAccount {get;set;}
        [DisplayName("Confirmer le mot de passe")]
        public string ConfirmPassword {get; set;}
    }
}