
namespace AgriNov.Models
{
	public interface IServiceBoxContract : IDisposable
	{
        void CreateDeleteDatabase();
        void InitializeTable();
        List<BoxContract> GetAllBoxContracts();
        List<BoxContract> GetAllBoxContractsForSale();
        BoxContract GetBoxContractById(int id);
        void InsertBoxContract(BoxContract boxContract);
        void UpdateBoxContract(BoxContract boxContract);
        void DeleteBoxContract(int id);
        bool ContractExist(int Id, ProductType productType, Seasons season, Years year);
        bool ContractExist(ProductType productType, Seasons season, Years year);
        List<DateTime> ComputeSeasonStartAndEnd(Years year, Seasons season);
        bool IsInCurrentSeason(int boxContractId);
        List<BoxContractSubscription> GetCurrentBoxContractsForUser(int userAccountId);
        List<BoxContractSubscription> GetCurrentBoxContractsForUser(string userAccountIdStr);
        List<BoxContract> GetAllAvailableBoxContracts(int userAccountId);
        void Save();
    }
}
