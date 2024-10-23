namespace AgriNov.Models
{
    public interface IServiceAdmin : IDisposable
    {
        void CreateDeleteDatabase();
        void InitializeTable();
        List<Admin> GetAdmins();
        User GetAdminByID(int adminID);
        User GetAdminByID(string adminrIDStr);
        void InsertAdmin(Admin admin);
        void UpdateAdmin(Admin admin);
        void DeleteAdmin(int adminID);
        void Save();
    }

}
