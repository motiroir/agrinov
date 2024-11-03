namespace AgriNov.Models
{
    public interface IServiceProduction : IDisposable
    {
        void CreateDeleteDatabase();
        void Initializetable();
        List<Production> GetProductions();
        Production GetProductionByID(int ProductionID);
        Production GetProductionByID(string ProductionID);
        void InsertProduction(Production production);
        void UpdateProduction(Production production);
        void DeleteProduction(int ProductionID);
        public List<Production> GetProductionsWithSupplierPdfProof();
        List<Production> GetProductionsBySupplier(int supplierID);
        int CalculateStock(ProductType productType, Seasons seasons, Years years);
        int GetNumberOfDelivery(DeliveryFrequency frequency);
        void Save();
    }
}
