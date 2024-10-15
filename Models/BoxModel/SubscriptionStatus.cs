using System.ComponentModel.DataAnnotations;

namespace AgriNov.Models.BoxModel
{
	public enum SubscriptionStatus
	{
		[Display(Name = "En attente")]
		WAITING,

		[Display(Name = "Signé")]
		OK
	}
}
