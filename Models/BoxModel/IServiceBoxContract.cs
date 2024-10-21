using AgriNov.Models.ProductionModel;

namespace AgriNov.Models
{
	public interface IServiceBoxContract : IDisposable
	{
        void CreateDeleteDatabase();
        void InitializeTable();
        List<BoxContract> GetAllBoxContracts();
        BoxContract GetBoxContractById(int id);
        void InsertBoxContract(BoxContract boxContract);
        void UpdateBoxContract(BoxContract boxContract);
        void DeleteBoxContract(int id);
        bool ContractExist(int Id, ProductType productType, Seasons season, Years year);
        bool ContractExist(ProductType productType, Seasons season, Years year);
        void Save();
    }
}
