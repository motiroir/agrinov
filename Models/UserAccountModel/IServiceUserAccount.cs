namespace AgriNov.Models
{
    public interface IServiceUserAccount : IDisposable
    {
        void CreateDeleteDatabase();
        void InitializeTable();
        List<UserAccount> GetUserAccounts();
        UserAccount GetUserAccountByID(int userAccountID);
        UserAccount GetUserAccountByID(string userAccountIDStr);
        void InsertUserAccount(UserAccount userAccount);
        void UpdateUserAccountPassword(int userAccountID, string password);
        void UpdateUserAccountExceptPassword(UserAccount userAccount);
        void DeleteUserAccount(int userAccountID);
        UserAccount Authenticate(string mail, string password);
        void Save();
    }
}
