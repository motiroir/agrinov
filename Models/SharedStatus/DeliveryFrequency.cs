<<<<<<< HEAD
using System.ComponentModel.DataAnnotations;

namespace AgriNov.Models
=======
﻿using System.ComponentModel.DataAnnotations;

namespace AgriNov.Models.ProductionModel
>>>>>>> main
{
    public enum DeliveryFrequency
    {
        [Display(Name = "Hebdomadaire")]
        WEEKLY,

        [Display(Name = "Bihebdomadaire")]
        BIWEEKLY,

        [Display(Name = "Mensuelle")]
        MONTHLY
    }
}
