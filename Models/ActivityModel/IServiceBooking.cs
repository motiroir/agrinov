namespace AgriNov.Models.ActivityModel
{
    public interface IServiceBooking : IDisposable
    {
        void InsertBooking(int userAccountId, int activityId);
        bool CanUserBookActivity(int userAccountId, int activityId);
        bool IsActivityFull(int activityId);
        int NbBookingsLeft(int activityId);

    }
}
