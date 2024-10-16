using System.ComponentModel.DataAnnotations;

namespace AgriNov.Models
{
	public enum SubscriptionStatus
	{
		[Display(Name = "En cours d'édition")]
		IN_PROGRESS,

		[Display(Name = "En vente")]
		FOR_SALE,

        [Display(Name = "Vendu")]
        SOLD,

        [Display(Name = "Terminé")]
        FINISHED
    }
}
