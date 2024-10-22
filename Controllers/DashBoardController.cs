using AgriNov.Models;
using AgriNov.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgriNov.Controllers
{
    public class DashBoard : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            UserAccountInfoUpdate viewModel = new UserAccountInfoUpdate();
            using (ServiceUserAccount serviceUserAccount = new ServiceUserAccount())
            {
                UserAccount currentAccount = serviceUserAccount.GetUserAccountByIDEager(HttpContext.User.Identity.Name);

                viewModel.UserAccountId = currentAccount.Id;
                viewModel.UserAccountLevel = currentAccount.UserAccountLevel;
                // Assign data to viewModel depending on the type of account
                if (currentAccount.UserAccountLevel == UserAccountLevel.USER || currentAccount.UserAccountLevel == UserAccountLevel.VOLUNTEER || currentAccount.UserAccountLevel == UserAccountLevel.ADMIN)
                {
                    viewModel.User = currentAccount.User;
                    viewModel.Address = currentAccount.User.Address;
                    viewModel.ContactDetails = currentAccount.User.ContactDetails;
                }
                if (currentAccount.UserAccountLevel == UserAccountLevel.CORPORATE)
                {
                    viewModel.CorporateUser = currentAccount.CorporateUser;
                    viewModel.Address = currentAccount.CorporateUser.Address;
                    viewModel.ContactDetails = currentAccount.CorporateUser.ContactDetails;
                    viewModel.CompanyDetails = currentAccount.CorporateUser.CompanyDetails;
                }
                if (currentAccount.UserAccountLevel == UserAccountLevel.SUPPLIER)
                {
                    viewModel.Supplier = currentAccount.Supplier;
                    viewModel.Address = currentAccount.Supplier.Address;
                    viewModel.ContactDetails = currentAccount.Supplier.ContactDetails;
                    viewModel.CompanyDetails = currentAccount.Supplier.CompanyDetails;
                }
            }
            return View(viewModel);
        }
       
}
}
