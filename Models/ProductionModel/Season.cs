using AgriNov.Models.SharedStatus;

namespace AgriNov.Models.ProductionModel
{
    public class Season
    {
        public int Id { get; set; }
        public Years Year { get; set; }
        public Seasons Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<Stock> Stocks { get; set; }
        public ICollection<BoxSubscription> BoxSubscriptions { get; set; }

        public Season(int id, Years year, Seasons name)
        {
            Id = id;
            Year = year;
            Name = name;


            if (Name == Seasons.SPRING)
            {
                StartDate = new DateTime((int)year, 03, 21);
                EndDate = new DateTime((int)year, 06, 20);
            }

            if (Name == Seasons.SUMMER)
            {
                StartDate = new DateTime((int)year, 06, 21);
                EndDate = new DateTime((int)year, 09, 22);
            }

            if (Name == Seasons.AUTUMN)
            {
                StartDate = new DateTime((int)year, 09, 23);
                EndDate = new DateTime((int)year, 12, 21);
            }

            if (Name == Seasons.WINTER)
            {
                StartDate = new DateTime((int)year, 12, 22);
                EndDate = new DateTime((int)year, 03, 20);
            }

        }
    }
}
