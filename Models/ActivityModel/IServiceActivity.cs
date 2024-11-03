namespace AgriNov.Models

{
    public interface IServiceActivity : IDisposable
    {
        void CreateDeleteDatabase();
        void InitializeTable();
        List<Activity> GetAllActivities();
        Activity GetActivity(int id);
        List<Activity> GetActivitiesByOrganizer(int organizerId);
        List<Activity> GetActivitiesByUserBooking(int userId);
        void InsertActivity(Activity activity);
        void UpdateActivity(Activity activity);
        void DeleteActivity(int id);
        void Save();
    }
}