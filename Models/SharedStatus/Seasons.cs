using System.ComponentModel.DataAnnotations;

namespace AgriNov.Models
{
    public enum Seasons
    {
        [Display(Name = "Sélectionnez une saison")]
        None = 0,

        [Display(Name ="Printemps")]
        SPRING = 1,

        [Display(Name = "Eté")]
        SUMMER = 2,

        [Display(Name = "Automne")]
        AUTUMN = 3,

        [Display(Name = "Hiver")]
        WINTER = 4,
    }
}
