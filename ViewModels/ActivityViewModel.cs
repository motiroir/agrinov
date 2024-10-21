using AgriNov.Models;

namespace AgriNov.ViewModels
{
    public class ActivityViewModel
    {
        public Activity Activity {  get; set; }
        public string OrganizerName { get; set; }
        public List<Activity> AllActivities { get; set; }
        public List<Activity> ActivitiesByOrganizer { get; set; }
        public List<Activity> ActivitiesBookedByUser { get; set; }
        public int NbBookingsLeft {  get; set; }
    }
}
