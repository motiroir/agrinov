namespace AgriNov.Models
{
  public interface IServiceUserAccount : IDisposable
  {
    void CreateDeleteDatabase();
    void InitializeTable();
    List<UserAccount> GetUserAccounts();
    List<UserAccount> GetUserAccountsFull();
    UserAccount GetUserAccountByID(int userAccountID);
    UserAccount GetUserAccountByID(string userAccountIDStr);
    UserAccount GetUserAccountByIDFullDetails(int userAccountID);
    UserAccount GetUserAccountByIDFullDetails(string userAccountIDStr);
    void InsertUserAccount(UserAccount userAccount);
    void UpdateUserAccountPassword(int userAccountID, string password);
    void UpdateUserAccountExceptPassword(UserAccount userAccount);
    void DeleteUserAccount(int userAccountID);
    public bool CheckIfMemberShipValid(int userAccountID);
    UserAccount Authenticate(string mail, string password);
    string GetUserFullName(UserAccount user);
    void Save();
  }
}
