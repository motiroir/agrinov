using AgriNov.Models.SharedStatus;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgriNov.Models
{
    public class Activity
    {
        public int Id { get; set; }
        [DisplayName("Jour de l'atelier")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime Datetime { get; set; }
        [Required]
        [DisplayName("Ville")]
        public string Location { get; set; }
        [Required]
        [DisplayName("Nombre de participants maximum")]
        public int MaxParticipants { get; set; }
        [Required]
        [DisplayName("Nombre d'invités par participant")]
        public int MaxInvitesPerPerson { get; set; }
        [Required]
        [DisplayName("Titre")]
        public string Name { get; set; }
        [Required]
        [MaxLength(500)]
        [MinLength(20, ErrorMessage ="Veuillez décrire l'atelier")]
        public string Description { get; set; }
        [DisplayName("Durée de l'atelier (en heure)")]
        public int Duration { get; set; }
        public ValidationStatus ValidationStatus { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }
        public int OrganizerId { get; set; }
    }
}
