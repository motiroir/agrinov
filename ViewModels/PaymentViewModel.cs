using AgriNov.Models;
using System.ComponentModel.DataAnnotations;

namespace AgriNov.ViewModels
{
    public class PaymentViewModel
    {
        public ShoppingCart ShoppingCart { get; set; }
        public Payment Payment { get; set; }

        [Required(ErrorMessage = "Le nom est requis.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Le numéro de carte est requis.")]
        [StringLength(20, MinimumLength = 16, ErrorMessage = "Le numéro de carte doit comporter 16 chiffres.")]
        [RegularExpression(@"^(\d{4}\s?){3}\d{4}$", ErrorMessage = "Le numéro de carte doit être valide.")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "La date d'expiration est requise.")]
        [RegularExpression(@"\d{2}/\d{2}", ErrorMessage = "Le format de la date d'expiration doit être MM/AA.")]
        [ExpirationDateValidation(ErrorMessage = "La date d'expiration doit être valide et non expirée.")]
        public string ExpirationDate { get; set; }

        [Required(ErrorMessage = "Le code de sécurité (CSV) est requis.")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Le code de sécurité doit comporter 3 chiffres.")]
        [RegularExpression(@"\d{3}", ErrorMessage = "Le code de sécurité doit être valide.")]
        public string SecurityCode { get; set; }
    }

    public class ExpirationDateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string expirationDate && expirationDate.Length == 5)
            {
                string[] parts = expirationDate.Split('/');
                if (parts.Length == 2 &&
                    int.TryParse(parts[0], out int month) &&
                    int.TryParse(parts[1], out int year))
                {
                    // Valider le mois et l'année
                    if (month >= 1 && month <= 12)
                    {
                        int currentYear = DateTime.Now.Year % 100;
                        int currentMonth = DateTime.Now.Month;
                        if (year > currentYear || (year == currentYear && month >= currentMonth))
                        {
                            return ValidationResult.Success;
                        }
                    }
                }
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}
