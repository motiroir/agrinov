using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AgriNov.Models;

namespace AgriNov.ViewModels
{
    public class UserAccountUpdate
    {
        public UserAccount UserAccount {get; set;}
        [DisplayName("Nouveau mot de passe")]
        [Required(ErrorMessage = "Nouveau mot de passe obligatoire")]
        [MinLength(8, ErrorMessage = "Le mot de passe doit contenir au moins 8 caractères")]
        public string NewPassword {get; set;}
        [DisplayName("Nouveau mot de passe")]
        [Required(ErrorMessage = "Nouveau mot de passe obligatoire")]
        [MinLength(8, ErrorMessage = "Le mot de passe doit contenir au moins 8 caractères")]
        public string ConfirmNewPassword {get; set;}
    }
}