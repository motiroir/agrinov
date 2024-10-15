using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AgriNov.Models
{
    [Owned]
    public class CompanyDetails
    {
        [DisplayName("Nom de l'entreprise")]
        [Required(ErrorMessage = "Nom de l'entreprise obligatoire")]
        [MaxLength(70, ErrorMessage = "Le nom de l'entreprise doit contenir 70 charactères au maximum")]
        public string CompanyName { get; set; }
        [DisplayName("Numéro SIRET")]
        [Required(ErrorMessage = "Numéro SIRET obligatoire")]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "Le numéro SIRET est invalide.")]
        public string SiretNumber { get; set; }
    }
}