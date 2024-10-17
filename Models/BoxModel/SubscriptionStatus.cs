using System.ComponentModel.DataAnnotations;

namespace AgriNov.Models
{
	public enum SubscriptionStatus
	{
        [Display(Name = "À venir")]
        FUTURE,

        [Display(Name = "En cours")]
		IN_PROGRESS,

        [Display(Name = "Terminé")]
        FINISHED
    }
}
