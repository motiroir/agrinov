using System.ComponentModel.DataAnnotations;

namespace AgriNov.Models
{
	public enum SubscriptionStatus
	{
		[Display(Name = "En attente")]
		WAITING,

		[Display(Name = "Signé")]
		OK
	}
}
