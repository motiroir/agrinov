namespace AgriNov.Models

{
    public interface IServiceActivity : IDisposable
    {
        void CreateDeleteDatabase();
        void InitializeTable();
        List<Activity> GetAllActivities();
        Activity GetActivity(int id);
        void InsertActivity(Activity activity);
        void UpdateActivity(Activity activity);
        void DeleteActivity(int id);
        void Save();
    }
}
