using AgriNov.Models;

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
            Activity activity = _DBContext.Activities.Find(id);
            if (activity != null)
            {
                _DBContext.Activities.Remove(activity);
                Save();
            }
        }

        public Activity GetActivity(int id)
        {
            return this._DBContext.Activities.Find(id);
        }

        public List<Activity> GetAllActivities()
        {
            return _DBContext.Activities.OrderBy(activity => activity.Datetime).ToList();
        }

        public List<Activity> GetActivitiesByOrganizer(int organizerId)
        {
            return _DBContext.Activities.Where(activity => activity.OrganizerId == organizerId).OrderBy(activity => activity.Datetime).ToList();
        }

        public List<Activity> GetActivitiesByUserBooking(int userId)
        {
            using (ServiceBooking sB =  new ServiceBooking())
            {
                List<int> activitiesId = sB.GetActivitiesIdByUserId(userId);
                List<Activity> activities = new List<Activity>();
                foreach (int activityId in activitiesId)
                {
                    Activity activity = GetActivity(activityId);
                    activities.Add(activity);
                }
                return activities.OrderBy(activity => activity.Datetime).ToList();
            }
        }
       
    
    public void InitializeTable()
        {
            string dirPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "activities");
            Activity a1 = new Activity() { Name = "Fabrication de pain au levain", OrganizerId = 10, Location = "Saint-Hilaire", Duration = 2, MaxParticipants = 10, MaxInvitesPerPerson = 2, Datetime = new DateTime(2024, 11, 12, 10, 0, 0), Description = "Apprenez à faire votre propre pain au levain avec des ingrédients bio locaux. Repartez avec votre pâte à levain et les bases pour continuer chez vous.", ImgActivity=File.ReadAllBytes(Path.Combine(dirPath,"a1.jpg")) };
            Activity a2 = new Activity() { Name = "Atelier permaculture", OrganizerId = 9, Location = "Clisson", Duration = 4, MaxParticipants = 15, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 12, 02, 14, 0, 0), Description = "Initiez-vous aux techniques de permaculture en milieu urbain et rural. Apprenez à concevoir un potager autonome et durable.", ImgActivity = File.ReadAllBytes(Path.Combine(dirPath, "a2.jpg")) };
            Activity a3 = new Activity() { Name = "Cuisiner les légumes d'hiver", OrganizerId = 8, Location = "Gorges", Duration = 1, MaxParticipants = 8, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 11, 20, 16, 0, 0), Description = "Découvrez des recettes créatives et simples pour valoriser vos légumes d'hiver : courges, betteraves, et choux, pour une cuisine de saison.", ImgActivity = File.ReadAllBytes(Path.Combine(dirPath, "a3.jpg")) };
            Activity a4 = new Activity() { Name = "Fabrication de savons naturels", OrganizerId = 7, Location = "Monnières", Duration = 2, MaxParticipants = 12, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 12, 10, 11, 0, 0), Description = "Participez à cet atelier pour fabriquer des savons à partir d'huiles végétales locales et d'ingrédients naturels, sans additifs chimiques.", ImgActivity = File.ReadAllBytes(Path.Combine(dirPath, "a4.jpg")) };
            Activity a5 = new Activity() { Name = "Atelier conserves de légumes", OrganizerId =  6, Location = "Clisson", Duration = 1, MaxParticipants = 10, MaxInvitesPerPerson = 2, Datetime = new DateTime(2024, 12, 05, 9, 0, 0), Description = "Apprenez les techniques de conservation de légumes de saison : stérilisation, pickles et fermentations pour prolonger vos récoltes.", ImgActivity = File.ReadAllBytes(Path.Combine(dirPath, "a5.jpg")) };
            Activity a6 = new Activity() { Name = "Taille des arbres fruitiers", OrganizerId = 5, Location = "Getigné", Duration = 5, MaxParticipants = 12, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 11, 18, 13, 0, 0), Description = "Venez découvrir comment tailler correctement vos arbres fruitiers pour assurer leur bonne santé et une production abondante.", ImgActivity = File.ReadAllBytes(Path.Combine(dirPath, "a6.jpg")) };
            Activity a7 = new Activity() { Name = "Fabrication de fromage fermier", OrganizerId = 4, Location = "Vallet", Duration = 8, MaxParticipants = 6, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 12, 15, 15, 0, 0), Description = "Apprenez à fabriquer du fromage à partir de lait cru local. Repartez avec vos créations à savourer chez vous.", ImgActivity = File.ReadAllBytes(Path.Combine(dirPath, "a7.jpg")) };
            Activity a8 = new Activity() { Name = "Initiation au jardinage bio", OrganizerId = 3, Location = "Clisson", Duration = 1, MaxParticipants = 20, MaxInvitesPerPerson = 2, Datetime = new DateTime(2024, 11, 30, 10, 0, 0), Description = "Découvrez les bases du jardinage biologique, en particulier la gestion des sols, les engrais naturels et la rotation des cultures.", ImgActivity = File.ReadAllBytes(Path.Combine(dirPath, "a8.jpg")) };
            Activity a9 = new Activity() { Name = "Atelier fabrication de cosmétiques naturels", OrganizerId = 2, Location = "Gétigné", Duration = 2, MaxParticipants = 8, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 11, 22, 14, 0, 0), Description = "Créez vos propres produits cosmétiques à base d’ingrédients naturels, sans conservateurs nocifs, dans cet atelier accessible à tous.", ImgActivity = File.ReadAllBytes(Path.Combine(dirPath, "a9.jpg")) };
            Activity a10 = new Activity() { Name = "Atelier greffe d’arbres", OrganizerId = 1, Location = "Saint-Lumine", Duration = 1, MaxParticipants = 10, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 12, 07, 11, 0, 0), Description = "Apprenez à greffer vos arbres fruitiers pour obtenir de nouvelles variétés et optimiser vos récoltes dans cet atelier pratique.", ImgActivity = File.ReadAllBytes(Path.Combine(dirPath, "a10.jpg")) };
            Activity a11 = new Activity() { Name = "Cueillette sauvage et cuisine", OrganizerId = 10, Location = "Pallet", Duration = 8, MaxParticipants = 12, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 11, 16, 9, 0, 0), Description = "Partez à la découverte des plantes comestibles locales et apprenez à les cuisiner dans des recettes simples et savoureuses.", ImgActivity = File.ReadAllBytes(Path.Combine(dirPath, "a11.jpg")) };
            Activity a12 = new Activity() { Name = "Atelier compostage et lombricompostage", OrganizerId = 3, Location = "Clisson", Duration = 3, MaxParticipants = 18, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 12, 12, 14, 0, 0), Description = "Apprenez à recycler vos déchets organiques pour obtenir un compost riche, et découvrez les secrets du lombricompostage pour un potager productif.", ImgActivity = File.ReadAllBytes(Path.Combine(dirPath, "a12.jpg")) };
            Activity a13 = new Activity() { Name = "Atelier lactofermentation", OrganizerId = 4, Location = "Gorges", Duration = 2, MaxParticipants = 10, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 11, 28, 16, 0, 0), Description = "Apprenez la technique de la lactofermentation pour conserver et transformer vos légumes en recettes savoureuses et probiotiques.", ImgActivity = File.ReadAllBytes(Path.Combine(dirPath, "a13.jpg")) };
            Activity a14 = new Activity() { Name = "Atelier vannerie", OrganizerId = 6, Location = "Clisson", Duration = 3, MaxParticipants = 8, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 12, 03, 11, 0, 0), Description = "Découvrez les techniques de vannerie pour fabriquer vos propres paniers et objets en osier dans une ambiance conviviale.", ImgActivity = File.ReadAllBytes(Path.Combine(dirPath, "a14.jpg")) };
            Activity a15 = new Activity() { Name = "Initiation à l'apiculture", OrganizerId = 4, Location = "Monnières", Duration = 4, MaxParticipants = 5, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 11, 25, 10, 0, 0), Description = "Participez à cet atelier pour comprendre les bases de l'apiculture : gestion des ruches, soins des abeilles et extraction de miel.", ImgActivity = File.ReadAllBytes(Path.Combine(dirPath, "a15.jpg")) };
            Activity a16 = new Activity() { Name = "Cuisiner avec des plantes sauvages", OrganizerId = 1, Location = "Vallet", Duration = 1, MaxParticipants = 10, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 11, 10, 14, 0, 0), Description = "Apprenez à identifier et cuisiner les plantes sauvages de votre région pour enrichir votre cuisine avec des produits naturels et locaux.", ImgActivity = File.ReadAllBytes(Path.Combine(dirPath, "a16.jpg")) };
            Activity a17 = new Activity() { Name = "Atelier bois et éco-construction", OrganizerId = 8, Location = "Saint-Hilaire", Duration = 3, MaxParticipants = 15, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 12, 13, 10, 0, 0), Description = "Découvrez les bases de l’éco-construction en bois, avec des techniques simples pour bâtir des structures légères dans votre jardin.", ImgActivity = File.ReadAllBytes(Path.Combine(dirPath, "a17.jpg")) };
            Activity a18 = new Activity() { Name = "Atelier fabrication de sirops et infusions", OrganizerId = 4, Location = "Gétigné", Duration = 4, MaxParticipants = 12, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 11, 27, 15, 0, 0), Description = "Apprenez à préparer vos propres sirops et infusions à partir de plantes locales pour des boissons saines et savoureuses.", ImgActivity = File.ReadAllBytes(Path.Combine(dirPath, "a18.jpg")) };
            Activity a19 = new Activity() { Name = "Préparation des fêtes : paniers gourmands", OrganizerId = 1, Location = "Clisson", Duration = 2, MaxParticipants = 20, MaxInvitesPerPerson = 2, Datetime = new DateTime(2024, 12, 17, 14, 0, 0), Description = "Participez à cet atelier festif où vous apprendrez à confectionner des paniers gourmands à offrir pour les fêtes, avec des produits locaux.", ImgActivity = File.ReadAllBytes(Path.Combine(dirPath, "a19.jpg")) };
            Activity a20 = new Activity() { Name = "Tissage de laine locale", OrganizerId = 4, Location = "Monnières", Duration = 4, MaxParticipants = 8, MaxInvitesPerPerson = 1, Datetime = new DateTime(2024, 12, 20, 11, 0, 0), Description = "Initiez-vous au tissage avec de la laine locale pour créer vos propres pièces uniques, tout en découvrant le processus de fabrication.", ImgActivity = File.ReadAllBytes(Path.Combine(dirPath, "a20.jpg")) };


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
            activity.ValidationStatus = ValidationStatus.WAITING;
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
