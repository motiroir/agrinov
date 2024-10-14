namespace AgriNov.Models.ProductionModel
{
    public interface IServiceProduction
    {
        void CreateDeleteDatabase();
        void Initializetable();

        List<Production> GetProductions();

        Production GetProductionByID(int ProductionID);

        Production GetProductionByID(string ProductionID);

        void InsertProduction(Production production);

        void UpdateProduction(Production production);

        void DeleteProduction(int ProductionID);

        void Save();


    }
}
