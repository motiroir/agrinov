using System.ComponentModel.DataAnnotations;

namespace AgriNov.Models
{
    public enum UserAccountLevel
    {
        [Display(Name = "En cours d'inscription")]
        DEFAULT,
        [Display(Name = "Fournisseur")]
        SUPPLIER,
        [Display(Name = "Adhérant")]
        USER,
        [Display(Name = "Entreprise")]
        CORPORATE,
        [Display(Name = "Bénévole")]
        VOLUNTEER,
        [Display(Name = "Administrateur")]
        ADMIN
    }
}