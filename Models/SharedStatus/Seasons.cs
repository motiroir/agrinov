using System.ComponentModel.DataAnnotations;

namespace AgriNov.Models
{
    public enum Seasons
    {
        [Display(Name ="Printemps")]
        SPRING,

        [Display(Name = "Eté")]
        SUMMER,

        [Display(Name = "Automne")]
        AUTUMN,

        [Display(Name = "Hiver")]
        WINTER,
    }
}
