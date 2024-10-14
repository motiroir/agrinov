using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgriNov.Models;

namespace AgriNov.ViewModels
{
    public class UserAccountConnexion
    {
        public UserAccount UserAccount {get; set;}
        public bool IsAuthenticated {get; set;}
    }
}