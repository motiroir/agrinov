using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace AgriNov.Models
{
    public class UserAccount
    {
        public int Id { get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage = "Email obligatoire")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Email invalide")]
        public string Mail { get; set; }
        [Required(ErrorMessage = "Mot de passe obligatoire")]
        [MinLength(8, ErrorMessage = "Le mot de passe doit contenir au moins 8 caractères")]
        public string Password { get; set; }
        [DisplayName("Type de compte")]
        public UserAccountLevel UserAccountLevel { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }

        public int? UserId {get; set;}
        public User User {get; set;}
        public int? CorporateUserId {get; set;}
        public CorporateUser CorporateUser {get; set;}
        public int? SupplierId {get; set;}
        public Supplier Supplier {get; set;}

    }
}