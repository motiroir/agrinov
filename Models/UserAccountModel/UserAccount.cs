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
        [DisplayName("Mot de passe")]
        [Required(ErrorMessage = "Mot de passe obligatoire")]
        [MinLength(8, ErrorMessage = "Le mot de passe doit contenir au moins 8 caractères")]
        public string Password { get; set; }
        [DisplayName("Type de compte")]
        public UserAccountLevel UserAccountLevel { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }
        //Optional One-To-One RelationShip w/ navigation
        public int? UserId { get; set; }
        public User User { get; set; }
        //Optional One-To-One RelationShip w/ navigation
        public int? CorporateUserId { get; set; }
        public CorporateUser CorporateUser { get; set; }
        //Optional One-To-One RelationShip w/navigation
        public int? SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        //One-To-One RelationShip
        public ShoppingCart ShoppingCart {get; set;}
        //One-To-Many RelationShip
        public ICollection<MemberShipFee> MembershipFees { get; } = new List<MemberShipFee>();
        //One-To-Many Relationship
        public ICollection<Order> Orders {get; set;} = new List<Order>();

        public string ProfilePic { get; set; }
    }
}