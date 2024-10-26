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

        void Save();


    }
}
