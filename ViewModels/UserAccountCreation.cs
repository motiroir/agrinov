using System.ComponentModel;
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