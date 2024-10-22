using AgriNov.Models;
using AgriNov.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgriNov.Controllers
{
    public class DashBoard : Controller
    {
        [Authorize]
        [HttpGet]
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

        [Authorize]
        [HttpPost]
        public IActionResult Index(UserAccountInfoUpdate viewModel)
        {
            // Assign data from viewModel depending on the type of account
            if (viewModel.UserAccountLevel == UserAccountLevel.USER || viewModel.UserAccountLevel == UserAccountLevel.VOLUNTEER || viewModel.UserAccountLevel == UserAccountLevel.ADMIN)
            {
                User updatedUser = null;
                using (IServiceUserAccount serviceUserAccount = new ServiceUserAccount())
                {
                    updatedUser = serviceUserAccount.GetUserAccountByIDEager(viewModel.UserAccountId).User;
                }
                updatedUser.Address = viewModel.Address;
                updatedUser.ContactDetails = viewModel.ContactDetails;
                using (IServiceUser serviceUser = new ServiceUser())
                {
                    serviceUser.UpdateUser(updatedUser);
                }
            }
            if (viewModel.UserAccountLevel == UserAccountLevel.CORPORATE)
            {
                CorporateUser updatedCorporateUser = null;
                using (IServiceUserAccount serviceUserAccount = new ServiceUserAccount())
                {
                    updatedCorporateUser = serviceUserAccount.GetUserAccountByIDEager(viewModel.UserAccountId).CorporateUser;
                }
                updatedCorporateUser.MaxBoxContractSubscription = viewModel.CorporateUser.MaxBoxContractSubscription;
                updatedCorporateUser.MaxActivitiesSignUp = viewModel.CorporateUser.MaxActivitiesSignUp;
                updatedCorporateUser.Address = viewModel.Address;
                updatedCorporateUser.ContactDetails = viewModel.ContactDetails;
                updatedCorporateUser.CompanyDetails = viewModel.CompanyDetails;
                using (IServiceCorporateUser serviceCorporateUser = new ServiceCorporateUser())
                {
                    serviceCorporateUser.UpdateCorporateUser(updatedCorporateUser);
                }
            }
            if (viewModel.UserAccountLevel == UserAccountLevel.SUPPLIER)
            {
                Supplier updatedSupplier = null;
                using (IServiceUserAccount serviceUserAccount = new ServiceUserAccount())
                {
                    updatedSupplier = serviceUserAccount.GetUserAccountByIDEager(viewModel.UserAccountId).Supplier;
                }
                if (viewModel.PdfFile != null)
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        viewModel.PdfFile.CopyTo(memoryStream);
                        updatedSupplier.ProofPdfDocument = memoryStream.ToArray();

                    }
                }
                updatedSupplier.Address = viewModel.Address;
                updatedSupplier.ContactDetails = viewModel.ContactDetails;
                updatedSupplier.CompanyDetails = viewModel.CompanyDetails;
                using (IServiceSupplier serviceSupplier = new ServiceSupplier())
                {
                    serviceSupplier.UpdateSupplier(updatedSupplier);
                }
            }
            return View(viewModel);
        }

    }
}
