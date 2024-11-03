using AgriNov.Models;
using AgriNov.Models.ActivityModel;
using AgriNov.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AgriNov.Controllers
{
    [Authorize]
    public class ActivityController : Controller
    {

        [Authorize(Roles = "USER,CORPORATE,SUPPLIER,VOLUNTEER,ADMIN")]
        public IActionResult CreateActivity(ActivityViewModel aVM)
        {
            return View(aVM);
        }

        [Authorize(Roles = "USER,CORPORATE,SUPPLIER,VOLUNTEER,ADMIN")]
        [HttpPost]
        public IActionResult CreateActivity(FileUploadActivity fileObj)
        {
            Activity activity = JsonConvert.DeserializeObject<Activity>(fileObj.Activity);
            if (ModelState.IsValid)
            {
                using (ServiceActivity sA = new ServiceActivity())
                {
                    activity.OrganizerId = Int32.Parse(HttpContext.User.Identity.Name);
                    //add img to activity
                    if (fileObj.file.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            fileObj.file.CopyTo(ms);
                            byte[] fileBytes = ms.ToArray();
                            activity.ImgActivity = fileBytes;
                        }
                    }
                    sA.InsertActivity(activity);
                    //isn't read, redirection is defined in ajax 
                    return RedirectToAction("ActivityDashboard", "Activity", new { activeTab = "ShowMyActivities" });
                }
            }

            //update the activity info to aVM that will be given to ActivityDashboard so that errors will be displayed
            ActivityViewModel aVM = new ActivityViewModel();
            aVM.Activity = activity;
            ViewData["ActiveTab"] = "CreateActivity";
            return View("ActivityDashboard", aVM);
        }

        [Authorize(Roles = "USER,CORPORATE,SUPPLIER,VOLUNTEER,ADMIN")]
        public IActionResult UpdateActivity(int id)
        {
            if (id > 0)
            {
                using (ServiceActivity sA = new ServiceActivity())
                {
                    Activity oldActivity = sA.GetActivity(id);
                    if (oldActivity != null)
                    {
                        return View(oldActivity);
                    }
                }
            }
            return View("Error");
        }

        [Authorize(Roles = "USER,CORPORATE,SUPPLIER,VOLUNTEER,ADMIN")]
        [HttpPost]
        public IActionResult UpdateActivity(Activity activity)
        {
            if (ModelState.IsValid)
            {
                if (activity.Id > 0)
                {
                    using (ServiceActivity sA = new ServiceActivity())
                    {
                        activity.OrganizerId = Int32.Parse(HttpContext.User.Identity.Name);
                        sA.UpdateActivity(activity);
                        return RedirectToAction("ActivityDashboard", "Activity", new { activeTab = "ShowMyActivities" });
                    }
                }
            }
            return View(activity);
        }

        [Authorize(Roles = "USER,CORPORATE,SUPPLIER,VOLUNTEER,ADMIN")]
        public IActionResult ActivityDashboard(string activeTab = "ShowAllActivities")
        {
            ActivityViewModel aVM = new ActivityViewModel();
            int userId = int.Parse(HttpContext.User.Identity.Name);

            using (ServiceActivity sA = new ServiceActivity())
            {
                // getting all activities in viewmodel
                aVM.AllActivities = sA.GetAllActivities();

                //get a list of activities organized by user
                aVM.ActivitiesByOrganizer = sA.GetActivitiesByOrganizer(userId);
                //get a list of activities booked by user
                aVM.ActivitiesBookedByUser = sA.GetActivitiesByUserBooking(userId);
            }

            ViewData["ActiveTab"] = activeTab;
            return View(aVM);
        }

        [Authorize(Roles = "USER,CORPORATE,SUPPLIER,VOLUNTEER,ADMIN")]
        public IActionResult ShowAllActivities(ActivityViewModel aVM)
        {
            return View(aVM);
        }

        [Authorize(Roles = "USER,CORPORATE,SUPPLIER,VOLUNTEER,ADMIN")]
        public IActionResult ShowMyActivities(ActivityViewModel aVM)
        {
            return View(aVM);
        }

        [Authorize(Roles = "USER,CORPORATE,SUPPLIER,VOLUNTEER,ADMIN")]
        public IActionResult ShowActivityDetails(int id, string returnUrl = null)
        {
            ActivityViewModel aVM = new ActivityViewModel();
            //getting the activity info + organizer name
            using (ServiceActivity sA = new ServiceActivity())
            {
                Activity activity = sA.GetActivity(id);
                aVM.Activity = activity;

                using (ServiceUserAccount sUA = new ServiceUserAccount())
                {
                    UserAccount organizer = sUA.GetUserAccountByIDFullDetails(activity.OrganizerId);
                    aVM.OrganizerName = sUA.GetUserFullName(organizer);
                }
            }
            // getting number booking left
            using (ServiceBooking sB = new ServiceBooking())
            {
                aVM.NbBookingsLeft = sB.NbBookingsLeft(id);
            }

            return View(aVM);
        }

        [Authorize(Roles = "USER,CORPORATE,SUPPLIER,VOLUNTEER,ADMIN")]
        [HttpPost]
        public IActionResult BookActivity(Activity activity)
        {
            int userId = int.Parse(HttpContext.User.Identity.Name);
            using (ServiceBooking sB = new ServiceBooking())
            {
                if (!sB.CanUserBookActivity(userId, activity.Id))
                {
                    //put a message to indicate that actvity's already been booked by user ? 
                }
                if (!sB.IsActivityFull(activity.Id))
                {
                    //put a message to indicate that activity's already full ? Maybe juste make the button unclickable ? 
                }

                sB.InsertBooking(userId, activity.Id);
                //show message success on booking ! + redirect to my bookings
            }
            return RedirectToAction("ActivityDashboard", "Activity", new { activeTab = "ShowMyActivities" });
        }

        public IActionResult AddImgActivityExample()
        {
            return View();
        }

        [Authorize(Roles = "USER,CORPORATE,SUPPLIER,VOLUNTEER,ADMIN")]
        [HttpGet]
        public IActionResult DeleteActivity(int id)
        {
            using (ServiceActivity sA = new ServiceActivity())
            {
                Activity activity = sA.GetActivity(id);
                if (activity != null)
                {
                    return View(activity);
                }
            }
            return View("Error");
        }


        [HttpPost, ActionName("DeleteActivity")]
        [Authorize(Roles = "USER,CORPORATE,SUPPLIER,VOLUNTEER,ADMIN")]
        public IActionResult DeleteConfirmed(int id)
        {
            using (ServiceActivity sA = new ServiceActivity())
            {
                // Vérifiez si l'activité existe avant de tenter de la supprimer
                Activity activity = sA.GetActivity(id);
                if (activity != null)
                {
                    sA.DeleteActivity(id);
                    TempData["SuccessMessage"] = "L'activité a été supprimée avec succès.";
                    return RedirectToAction("ActivityDashboard", new { activeTab = "ShowMyActivities" });
                }
            }
            return View("Error");
        }
    }
}