
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
            return this._DBContext.Activities.Find(id);
        }

        public List<Activity> GetAllActivities()
        {
            return _DBContext.Activities.ToList();
        }

        public void InitializeTable()
        {
            Activity a1 = new Activity() { Name = "Fabrication de pain au levain", Location = "Saint-Hilaire", Duration = 2, MaxParticipants = 10, MaxInvitesPerPerson = 2, Datetime = new DateTime(2024, 11, 12, 10, 0, 0), Description = "Apprenez à faire votre propre pain au levain avec des ingrédients bio locaux. Repartez avec votre pâte à levain et les bases pour continuer chez vous." };
            Activity a2 = new Activity() { Name = "Atelier permaculture", Location = "Clisson", Duration = 4, MaxParticipants = 15, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 12, 02, 14, 0, 0), Description = "Initiez-vous aux techniques de permaculture en milieu urbain et rural. Apprenez à concevoir un potager autonome et durable." };
            Activity a3 = new Activity() { Name = "Cuisiner les légumes d'hiver", Location = "Gorges", Duration = 1, MaxParticipants = 8, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 11, 20, 16, 0, 0), Description = "Découvrez des recettes créatives et simples pour valoriser vos légumes d'hiver : courges, betteraves, et choux, pour une cuisine de saison." };
            Activity a4 = new Activity() { Name = "Fabrication de savons naturels", Location = "Monnières", Duration = 2, MaxParticipants = 12, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 12, 10, 11, 0, 0), Description = "Participez à cet atelier pour fabriquer des savons à partir d'huiles végétales locales et d'ingrédients naturels, sans additifs chimiques." };
            Activity a5 = new Activity() { Name = "Atelier conserves de légumes", Location = "Clisson", Duration = 1, MaxParticipants = 10, MaxInvitesPerPerson = 2, Datetime = new DateTime(2024, 12, 05, 9, 0, 0), Description = "Apprenez les techniques de conservation de légumes de saison : stérilisation, pickles et fermentations pour prolonger vos récoltes." };
            Activity a6 = new Activity() { Name = "Taille des arbres fruitiers", Location = "Getigné", Duration = 5, MaxParticipants = 12, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 11, 18, 13, 0, 0), Description = "Venez découvrir comment tailler correctement vos arbres fruitiers pour assurer leur bonne santé et une production abondante." };
            Activity a7 = new Activity() { Name = "Fabrication de fromage fermier", Location = "Vallet", Duration = 8, MaxParticipants = 6, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 12, 15, 15, 0, 0), Description = "Apprenez à fabriquer du fromage à partir de lait cru local. Repartez avec vos créations à savourer chez vous." };
            Activity a8 = new Activity() { Name = "Initiation au jardinage bio", Location = "Clisson", Duration = 1, MaxParticipants = 20, MaxInvitesPerPerson = 2, Datetime = new DateTime(2024, 11, 30, 10, 0, 0), Description = "Découvrez les bases du jardinage biologique, en particulier la gestion des sols, les engrais naturels et la rotation des cultures." };
            Activity a9 = new Activity() { Name = "Atelier fabrication de cosmétiques naturels", Location = "Gétigné", Duration = 2, MaxParticipants = 8, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 11, 22, 14, 0, 0), Description = "Créez vos propres produits cosmétiques à base d’ingrédients naturels, sans conservateurs nocifs, dans cet atelier accessible à tous." };
            Activity a10 = new Activity() { Name = "Atelier greffe d’arbres", Location = "Saint-Lumine", Duration = 1, MaxParticipants = 10, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 12, 07, 11, 0, 0), Description = "Apprenez à greffer vos arbres fruitiers pour obtenir de nouvelles variétés et optimiser vos récoltes dans cet atelier pratique." };
            Activity a11 = new Activity() { Name = "Cueillette sauvage et cuisine", Location = "Pallet", Duration = 8, MaxParticipants = 12, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 11, 16, 9, 0, 0), Description = "Partez à la découverte des plantes comestibles locales et apprenez à les cuisiner dans des recettes simples et savoureuses." };
            Activity a12 = new Activity() { Name = "Atelier compostage et lombricompostage", Location = "Clisson", Duration = 3, MaxParticipants = 18, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 12, 12, 14, 0, 0), Description = "Apprenez à recycler vos déchets organiques pour obtenir un compost riche, et découvrez les secrets du lombricompostage pour un potager productif." };
            Activity a13 = new Activity() { Name = "Atelier lactofermentation", Location = "Gorges", Duration = 2, MaxParticipants = 10, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 11, 28, 16, 0, 0), Description = "Apprenez la technique de la lactofermentation pour conserver et transformer vos légumes en recettes savoureuses et probiotiques." };
            Activity a14 = new Activity() { Name = "Atelier vannerie", Location = "Clisson", Duration = 3, MaxParticipants = 8, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 12, 03, 11, 0, 0), Description = "Découvrez les techniques de vannerie pour fabriquer vos propres paniers et objets en osier dans une ambiance conviviale." };
            Activity a15 = new Activity() { Name = "Initiation à l'apiculture", Location = "Monnières", Duration = 4, MaxParticipants = 5, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 11, 25, 10, 0, 0), Description = "Participez à cet atelier pour comprendre les bases de l'apiculture : gestion des ruches, soins des abeilles et extraction de miel." };
            Activity a16 = new Activity() { Name = "Cuisiner avec des plantes sauvages", Location = "Vallet", Duration = 1, MaxParticipants = 10, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 11, 10, 14, 0, 0), Description = "Apprenez à identifier et cuisiner les plantes sauvages de votre région pour enrichir votre cuisine avec des produits naturels et locaux." };
            Activity a17 = new Activity() { Name = "Atelier bois et éco-construction", Location = "Saint-Hilaire", Duration = 3, MaxParticipants = 15, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 12, 13, 10, 0, 0), Description = "Découvrez les bases de l’éco-construction en bois, avec des techniques simples pour bâtir des structures légères dans votre jardin." };
            Activity a18 = new Activity() { Name = "Atelier fabrication de sirops et infusions", Location = "Gétigné", Duration = 4, MaxParticipants = 12, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 11, 27, 15, 0, 0), Description = "Apprenez à préparer vos propres sirops et infusions à partir de plantes locales pour des boissons saines et savoureuses." };
            Activity a19 = new Activity() { Name = "Préparation des fêtes : paniers gourmands", Location = "Clisson", Duration = 2, MaxParticipants = 20, MaxInvitesPerPerson = 2, Datetime = new DateTime(2024, 12, 17, 14, 0, 0), Description = "Participez à cet atelier festif où vous apprendrez à confectionner des paniers gourmands à offrir pour les fêtes, avec des produits locaux." };
            Activity a20 = new Activity() { Name = "Tissage de laine locale", Location = "Monnières", Duration = 4, MaxParticipants = 8, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 12, 20, 11, 0, 0), Description = "Initiez-vous au tissage avec de la laine locale pour créer vos propres pièces uniques, tout en découvrant le processus de fabrication." };


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
            Activity oldActivity = this.GetActivity(activity.Id);
            if (oldActivity == null)
            {
                return;
            }
            activity.DateLastModified = DateTime.Now;
            _DBContext.Entry(oldActivity).CurrentValues.SetValues(activity);
            Save();
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
