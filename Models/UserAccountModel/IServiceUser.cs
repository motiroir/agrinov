namespace AgriNov.Models
{
    public interface IServiceUser : IDisposable
    {
        void CreateDeleteDatabase();
        void InitializeTable();
        List<User> GetUsers();
        User GetUserByID(int userID);
        User GetUserByID(string userIDStr);
        void InsertUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int userID);
        void Save();
    }
}