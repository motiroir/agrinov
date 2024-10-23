using System.ComponentModel.DataAnnotations;

namespace AgriNov.Models
{
    public enum UserAccountLevel
    {
        [Display(Name = "En cours d'inscription")]
        DEFAULT,
        [Display(Name = "Fournisseur")]
        SUPPLIER,
        [Display(Name = "Adh�rant")]
        USER,
        [Display(Name = "Entreprise")]
        CORPORATE,
        [Display(Name = "B�n�vole")]
        VOLUNTEER,
        [Display(Name = "Administrateur")]
        ADMIN
    }
}