namespace AgriNov.Models
{
    public interface IServiceUserAccount : IDisposable
    {
        void CreateDeleteDatabase();
        void InitializeTable();
        List<UserAccount> GetUserAccounts();
        UserAccount GetUserAccountByID(int userAccountID);
        UserAccount GetUserAccountByID(string userAccountIDStr);
          UserAccount GetUserAccountByIDEager(int userAccountID);
        UserAccount GetUserAccountByIDEager(string userAccountIDStr);
        void InsertUserAccount(UserAccount userAccount);
        void UpdateUserAccountPassword(int userAccountID, string password);
        void UpdateUserAccountExceptPassword(UserAccount userAccount);
        void DeleteUserAccount(int userAccountID);
        public bool CheckIfMemberShipValid(int userAccountID);
        UserAccount Authenticate(string mail, string password);
        public List<UserAccount> GetUserAccountsFull();
        void Save();
    }
}
