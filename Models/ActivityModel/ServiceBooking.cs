namespace AgriNov.Models
{
    public class ServiceBooking : IServiceBooking
    {
        private BDDContext _DBContext;

        public ServiceBooking()
        {
            _DBContext = new BDDContext();
        }

        public void InitializeTable()
        {
            InsertBooking(1,1);
            InsertBooking(1,2);
            InsertBooking(2,3);
            InsertBooking(4,1);
            InsertBooking(4,3);
            InsertBooking(5,1);
            InsertBooking(5,2);
            InsertBooking(2,5);
            InsertBooking(2,4);
            InsertBooking(2,12);
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

        public List<int> GetActivitiesIdByUserId(int userId)
        {
            return _DBContext.Bookings
                .Where(booking => booking.UserAccountId == userId)
                .Select(booking => booking.ActivityId)
                .ToList();
        }

        public void Dispose()
        {
            _DBContext.Dispose();
        }
    }
}
