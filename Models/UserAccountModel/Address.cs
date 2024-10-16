using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgriNov.Models
{
    [Owned]
    public class Address
    {
        [MaxLength(50, ErrorMessage = "30 charactères maximum")]
        [DisplayName("Complément")]
        public string Line1 { get; set; }
        [MaxLength(50, ErrorMessage = "30 charactères maximum")]
        [DisplayName("Numéro et Rue")]
        public string Line2 { get; set; }
        [DisplayName("Ville")]
        [MaxLength(50, ErrorMessage = "30 charactères maximum")]
        public string City { get; set; }
        [DisplayName("Code Postal")]
        [MaxLength(8, ErrorMessage = "8 charactères maximum")]
        public string PostCode { get; set; }
    }
}