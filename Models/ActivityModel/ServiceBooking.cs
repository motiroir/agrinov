namespace AgriNov.Models.ActivityModel
{
    public class ServiceBooking : IServiceBooking
    {
        private BDDContext _DBContext;

        public ServiceBooking()
        {
            _DBContext = new BDDContext();
        }

        public void InsertBooking(int userAccountId, int activityId)
        {
            Booking booking = new Booking() { ActivityId = activityId, UserAccountId = userAccountId, DateReserved = DateTime.Now};
            _DBContext.Add(booking);
            _DBContext.SaveChanges();
        }

        public bool CanUserBookActivity(int userAccountId, int activityId)
        {
            return !_DBContext.Bookings.Any(booking => booking.UserAccountId == userAccountId && booking.ActivityId == activityId);
        }

        public bool IsActivityFull(int activityId)
        {
            int nbCurrentBooking = _DBContext.Bookings.Count(booking => booking.ActivityId == activityId);
            using (ServiceActivity sA = new ServiceActivity())
            {
                Activity activity = sA.GetActivity(activityId); 
                if (activity.MaxParticipants > nbCurrentBooking)
                {
                    return true;
                }
                else { return false; }
            }
        }

        public int NbBookingsLeft(int activityId)
        {
            int nbCurrentBooking = _DBContext.Bookings.Count(booking => booking.ActivityId == activityId);
            using (ServiceActivity sA = new ServiceActivity())
            {
                Activity activity = sA.GetActivity(activityId);
                return activity.MaxParticipants-nbCurrentBooking;
            }
        }

        public void Dispose()
        {
            _DBContext.Dispose();
        }
    }
}
