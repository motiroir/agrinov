namespace AgriNov.Models
{
    public interface IServiceBooking : IDisposable
    {
        void InitializeTable();
        void InsertBooking(int userAccountId, int activityId);
        bool CanUserBookActivity(int userAccountId, int activityId);
        bool IsActivityFull(int activityId);
        int NbBookingsLeft(int activityId);
        List<int> GetActivitiesIdByUserId(int userId);

    }
}
