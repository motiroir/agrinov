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
    public class ContactDetails
    {
        [DisplayName("Nom")]
        [Required(ErrorMessage = "Nom obligatoire")]
        [MaxLength(40, ErrorMessage = "Le nom doit contenir 40 charactères au maximum")]
        public string Name { get; set; }

        [DisplayName("Prénom")]
        [Required(ErrorMessage = "Prénom obligatoire")]
        [MaxLength(40, ErrorMessage = "Le prénom doit contenir 40 charactères au maximum")]
        public string FirstName { get; set; }
        [DisplayName("Téléphone")]
        [Required(ErrorMessage = "Numéro de téléphone obligatoire")]
        [RegularExpression(@"^\+?[0-9]{10,14}$", ErrorMessage = "Numéro de téléphone invalide")]
        public string PhoneNumber { get; set; }
    }
}