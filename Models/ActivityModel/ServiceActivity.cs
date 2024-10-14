
namespace AgriNov.Models
{
    public class ServiceActivity : IServiceActivity
    {
        private BDDContext _DBContext;

        public ServiceActivity()
        {
            _DBContext = new BDDContext();
        }

        public void CreateDeleteDatabase()
        {
            _DBContext.Database.EnsureDeleted();
            _DBContext.Database.EnsureCreated();
        }

        public void DeleteActivity(int id)
        {
            throw new NotImplementedException();
        }

        public Activity GetActivity(int id)
        {
            throw new NotImplementedException();
        }

        public List<Activity> GetAllActivities()
        {
            throw new NotImplementedException();
        }

        public void InitializeTable()
        {
            Activity a1 = new Activity() { Name = "Fabrication de pain au levain", Location = "Saint-Hilaire", MaxParticipants = 10, MaxInvitesPerPerson = 2, Datetime = new DateTime(2024, 11, 12), Description = "Apprenez à faire votre propre pain au levain avec des ingrédients bio locaux. Repartez avec votre pâte à levain et les bases pour continuer chez vous." };
            Activity a2 = new Activity() { Name = "Atelier permaculture", Location = "Clisson", MaxParticipants = 15, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 12, 02), Description = "Initiez-vous aux techniques de permaculture en milieu urbain et rural. Apprenez à concevoir un potager autonome et durable." };
            Activity a3 = new Activity() { Name = "Cuisiner les légumes d'hiver", Location = "Gorges", MaxParticipants = 8, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 11, 20), Description = "Découvrez des recettes créatives et simples pour valoriser vos légumes d'hiver : courges, betteraves, et choux, pour une cuisine de saison." };
            Activity a4 = new Activity() { Name = "Fabrication de savons naturels", Location = "Monnières", MaxParticipants = 12, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 12, 10), Description = "Participez à cet atelier pour fabriquer des savons à partir d'huiles végétales locales et d'ingrédients naturels, sans additifs chimiques." };
            Activity a5 = new Activity() { Name = "Atelier conserves de légumes", Location = "Clisson", MaxParticipants = 10, MaxInvitesPerPerson = 2, Datetime = new DateTime(2024, 12, 05), Description = "Apprenez les techniques de conservation de légumes de saison : stérilisation, pickles et fermentations pour prolonger vos récoltes." };
            Activity a6 = new Activity() { Name = "Taille des arbres fruitiers", Location = "Getigné", MaxParticipants = 12, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 11, 18), Description = "Venez découvrir comment tailler correctement vos arbres fruitiers pour assurer leur bonne santé et une production abondante." };
            Activity a7 = new Activity() { Name = "Fabrication de fromage fermier", Location = "Vallet", MaxParticipants = 6, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 12, 15), Description = "Apprenez à fabriquer du fromage à partir de lait cru local. Repartez avec vos créations à savourer chez vous." };
            Activity a8 = new Activity() { Name = "Initiation au jardinage bio", Location = "Clisson", MaxParticipants = 20, MaxInvitesPerPerson = 2, Datetime = new DateTime(2024, 11, 30), Description = "Découvrez les bases du jardinage biologique, en particulier la gestion des sols, les engrais naturels et la rotation des cultures." };
            Activity a9 = new Activity() { Name = "Atelier fabrication de cosmétiques naturels", Location = "Gétigné", MaxParticipants = 8, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 11, 22), Description = "Créez vos propres produits cosmétiques à base d’ingrédients naturels, sans conservateurs nocifs, dans cet atelier accessible à tous." };
            Activity a10 = new Activity() { Name = "Atelier greffe d’arbres", Location = "Saint-Lumine", MaxParticipants = 10, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 12, 07), Description = "Apprenez à greffer vos arbres fruitiers pour obtenir de nouvelles variétés et optimiser vos récoltes dans cet atelier pratique." };
            Activity a11 = new Activity() { Name = "Cueillette sauvage et cuisine", Location = "Pallet", MaxParticipants = 12, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 11, 16), Description = "Partez à la découverte des plantes comestibles locales et apprenez à les cuisiner dans des recettes simples et savoureuses." };
            Activity a12 = new Activity() { Name = "Atelier compostage et lombricompostage", Location = "Clisson", MaxParticipants = 18, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 12, 12), Description = "Apprenez à recycler vos déchets organiques pour obtenir un compost riche, et découvrez les secrets du lombricompostage pour un potager productif." };
            Activity a13 = new Activity() { Name = "Atelier lactofermentation", Location = "Gorges", MaxParticipants = 10, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 11, 28), Description = "Apprenez la technique de la lactofermentation pour conserver et transformer vos légumes en recettes savoureuses et probiotiques." };
            Activity a14 = new Activity() { Name = "Atelier vannerie", Location = "Clisson", MaxParticipants = 8, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 12, 03), Description = "Découvrez les techniques de vannerie pour fabriquer vos propres paniers et objets en osier dans une ambiance conviviale." };
            Activity a15 = new Activity() { Name = "Initiation à l'apiculture", Location = "Monnières", MaxParticipants = 5, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 11, 25), Description = "Participez à cet atelier pour comprendre les bases de l'apiculture : gestion des ruches, soins des abeilles et extraction de miel." };
            Activity a16 = new Activity() { Name = "Cuisiner avec des plantes sauvages", Location = "Vallet", MaxParticipants = 10, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 11, 10), Description = "Apprenez à identifier et cuisiner les plantes sauvages de votre région pour enrichir votre cuisine avec des produits naturels et locaux." };
            Activity a17 = new Activity() { Name = "Atelier bois et éco-construction", Location = "Saint-Hilaire", MaxParticipants = 15, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 12, 13), Description = "Découvrez les bases de l’éco-construction en bois, avec des techniques simples pour bâtir des structures légères dans votre jardin." };
            Activity a18 = new Activity() { Name = "Atelier fabrication de sirops et infusions", Location = "Gétigné", MaxParticipants = 12, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 11, 27), Description = "Apprenez à préparer vos propres sirops et infusions à partir de plantes locales pour des boissons saines et savoureuses." };
            Activity a19 = new Activity() { Name = "Préparation des fêtes : paniers gourmands", Location = "Clisson", MaxParticipants = 20, MaxInvitesPerPerson = 2, Datetime = new DateTime(2024, 12, 17), Description = "Participez à cet atelier festif où vous apprendrez à confectionner des paniers gourmands à offrir pour les fêtes, avec des produits locaux." };
            Activity a20 = new Activity() { Name = "Tissage de laine locale", Location = "Monnières", MaxParticipants = 8, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 12, 20), Description = "Apprenez à tisser des écharpes et couvertures à partir de laine locale, en utilisant des techniques artisanales traditionnelles." };
            
            InsertActivity(a1);
            InsertActivity(a2);
            InsertActivity(a3);
            InsertActivity(a4);
            InsertActivity(a5);
            InsertActivity(a6);
            InsertActivity(a7);
            InsertActivity(a8);
            InsertActivity(a9);
            InsertActivity(a10);
            InsertActivity(a11);
            InsertActivity(a12);
            InsertActivity(a13);
            InsertActivity(a14);
            InsertActivity(a15);
            InsertActivity(a16);
            InsertActivity(a17);
            InsertActivity(a18);
            InsertActivity(a19);
            InsertActivity(a20);
        }

        public void InsertActivity(Activity activity)
        {
            activity.ValidationStatus = SharedStatus.ValidationStatus.WAITING;
            activity.DateCreated = DateTime.Now;
            activity.DateLastModified = DateTime.Now;
            _DBContext.Activities.Add(activity);
            Save();
        }

        public void UpdateActivity(Activity activity)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _DBContext.SaveChanges();
        }

        public void Dispose()
        {
            _DBContext.Dispose();
        }
    }
}
