using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgriNov.Models
{
    public class Volunteer : User
    {
        public string Availabilities { get; set;}
        public double HoursWorkDone { get; set;}
    }
}