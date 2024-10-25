using System.Security.Claims;
using AgriNov.Models;
using AgriNov.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AgriNov.Controllers
{
    public class AccountController : Controller
    {
        [Authorize(Roles = "DEFAULT")]
        [HttpGet]
        public IActionResult TypeSelection()
        {
            UserAccount currentUserAccount;
            using (IServiceUserAccount serviceUserAccount = new ServiceUserAccount())
            {
                currentUserAccount = serviceUserAccount.GetUserAccountByID(HttpContext.User.Identity.Name);
            }
            if (currentUserAccount.UserId != null || currentUserAccount.SupplierId != null || currentUserAccount.CorporateUserId != null)
            {
                return View("Error");
            }
            return View();
        }

        [Authorize(Roles = "DEFAULT")]
        [HttpPost]
        public IActionResult TypeSelection(UserAccount userAccount)
        {
            switch (userAccount.UserAccountLevel)
            {
                case UserAccountLevel.USER:
                    return RedirectToAction("AddRegularUser", "Account");
                case UserAccountLevel.CORPORATE:
                    return RedirectToAction("AddCorporateUser", "Account");
                case UserAccountLevel.SUPPLIER:
                    return RedirectToAction("AddSupplier", "Account");
                case UserAccountLevel.DEFAULT:
                    return View();
                default:
                    return View();
            }
        }

        [Authorize(Roles = "DEFAULT")]
        [HttpGet]
        public IActionResult AddRegularUser()
        {
            User user = new User();
            using (IServiceUserAccount serviceUserAccount = new ServiceUserAccount())
            {
                user.UserAccount = serviceUserAccount.GetUserAccountByID(HttpContext.User.Identity.Name);
            }
            if (user.UserAccount.UserId != null || user.UserAccount.SupplierId != null || user.UserAccount.CorporateUserId != null)
            {
                return View("Error");
            }
            return View(user);
        }

        [Authorize(Roles = "DEFAULT")]
        [HttpPost]
        public IActionResult AddRegularUser(User user)
        {
            // Check if logged user id was not modified, if not proceed otherwise error.
            using (IServiceUserAccount serviceUserAccount = new ServiceUserAccount())
            {
                UserAccount currentUserAccount = serviceUserAccount.GetUserAccountByID(HttpContext.User.Identity.Name);
                if (user.UserAccount.Id.Equals(currentUserAccount.Id))
                {
                    user.UserAccount = currentUserAccount;
                }
            }
            if (!ModelState.IsValid)
            {
                using (IServiceUser serviceUser = new ServiceUser())
                {
                    // Level = User unless changed bu admin
                    user.UserAccount.UserAccountLevel = UserAccountLevel.USER;
                    serviceUser.InsertUser(user);
                    // Rewrite cookie with new permission
                    UpdateRoleInAuthCookie(UserAccountLevel.USER);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(user);
        }

        [Authorize(Roles = "DEFAULT")]
        [HttpGet]
        public IActionResult AddCorporateUser()
        {
            CorporateUser corporateUser = new CorporateUser();
            using (IServiceUserAccount serviceUserAccount = new ServiceUserAccount())
            {
                corporateUser.UserAccount = serviceUserAccount.GetUserAccountByID(HttpContext.User.Identity.Name);
            }
            if (corporateUser.UserAccount.UserId != null || corporateUser.UserAccount.SupplierId != null || corporateUser.UserAccount.CorporateUserId != null)
            {
                return View("Error");
            }
            return View(corporateUser);
        }

        [Authorize(Roles = "DEFAULT")]
        [HttpPost]
        public IActionResult AddCorporateUser(CorporateUser corporateUser)
        {
            // Check if logged user id was not modified, if not proceed otherwise error.
            using (IServiceUserAccount serviceUserAccount = new ServiceUserAccount())
            {
                UserAccount currentUserAccount = serviceUserAccount.GetUserAccountByID(HttpContext.User.Identity.Name);
                if (corporateUser.UserAccount.Id.Equals(currentUserAccount.Id))
                {
                    corporateUser.UserAccount = currentUserAccount;
                }
                else
                {
                    return View("Error");
                }
            }
            if (!ModelState.IsValid)
            {
                using (IServiceCorporateUser serviceCorporateUser = new ServiceCorporateUser())
                {
                    // corporateUser.UserAccount.UserAccountLevel = UserAccountLevel.CORPORATE;
                    serviceCorporateUser.InsertCorporateUser(corporateUser);
                    // Rewrite cookie with new permission
                    UpdateRoleInAuthCookie(UserAccountLevel.CORPORATE);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(corporateUser);
        }

        [Authorize(Roles = "DEFAULT")]
        [HttpGet]
        public IActionResult AddSupplier()
        {
            SupplierWithProof viewModel = new SupplierWithProof() { Supplier = new Supplier() };
            using (IServiceUserAccount serviceUserAccount = new ServiceUserAccount())
            {
                viewModel.Supplier.UserAccount = serviceUserAccount.GetUserAccountByID(HttpContext.User.Identity.Name);
            }
            if (viewModel.Supplier.UserAccount.UserId != null || viewModel.Supplier.UserAccount.SupplierId != null || viewModel.Supplier.UserAccount.CorporateUserId != null)
            {
                return View("Error");
            }
            return View(viewModel);
        }

        [Authorize(Roles = "DEFAULT")]
        [HttpPost]
        public IActionResult AddSupplier(SupplierWithProof viewModel)
        {
            // Check if logged user id was not modified, if not proceed otherwise error.
            using (IServiceUserAccount serviceUserAccount = new ServiceUserAccount())
            {
                UserAccount currentUserAccount = serviceUserAccount.GetUserAccountByID(HttpContext.User.Identity.Name);
                if (viewModel.Supplier.UserAccount.Id.Equals(currentUserAccount.Id))
                {
                    viewModel.Supplier.UserAccount = currentUserAccount;
                }
                else
                {
                    return View("Error");
                }
            }
            if (!ModelState.IsValid)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    viewModel.PdfFile.CopyTo(memoryStream);
                    viewModel.Supplier.ProofPdfDocument = memoryStream.ToArray();

                }
                using (IServiceSupplier serviceSupplier = new ServiceSupplier())
                {
                    // viewModel.Supplier.UserAccount.UserAccountLevel = UserAccountLevel.SUPPLIER;
                    serviceSupplier.InsertSupplier(viewModel.Supplier);
                    // Rewrite cookie with new permission
                    UpdateRoleInAuthCookie(UserAccountLevel.SUPPLIER);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(viewModel);
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            UserAccountUpdate viewModel = new UserAccountUpdate();
            using (IServiceUserAccount serviceUserAccount = new ServiceUserAccount())
            {
                viewModel.UserAccount = serviceUserAccount.GetUserAccountByID(HttpContext.User.Identity.Name);
            }
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult ChangePassword(UserAccountUpdate viewModel)
        {
            if (viewModel.NewPassword == viewModel.ConfirmNewPassword)
            {
                if (ModelState.IsValid)
                {
                    using (IServiceUserAccount serviceUserAccount = new ServiceUserAccount())
                    {
                        //Check if user account id or mail was not modified in the form
                        UserAccount currentUserAccount = serviceUserAccount.GetUserAccountByID(HttpContext.User.Identity.Name);
                        if (!viewModel.UserAccount.Id.Equals(currentUserAccount.Id) || viewModel.UserAccount.Mail != currentUserAccount.Mail)
                        {
                            return View("Error");
                        }
                        //Check if login is correct
                        UserAccount userAccount = serviceUserAccount.Authenticate(currentUserAccount.Mail, viewModel.UserAccount.Password);
                        if (userAccount != null)
                        {
                            serviceUserAccount.UpdateUserAccountPassword(userAccount.Id, viewModel.NewPassword);
                        }
                        else
                        {
                            ModelState.AddModelError("UserAccount.Password", "Ancien mot de passe incorrect");
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError("NewPassword", "Les mots de passe ne correspondent pas");
            }
            return RedirectToAction("Index", "DashBoard");
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangeInfo()
        {
            UserAccountInfoUpdate viewModel = new UserAccountInfoUpdate();
            using (IServiceUserAccount serviceUserAccount = new ServiceUserAccount())
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
        public IActionResult ChangeInfo(UserAccountInfoUpdate viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
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
                return RedirectToAction("Index", "DashBoard");
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
                return RedirectToAction("Index", "DashBoard");
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
                return RedirectToAction("Index", "DashBoard");
            }
            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "SUPPLIER,ADMIN")]
        public IActionResult DownloadPdf(int supplierId)
        {
            byte[] pdfBinaryData = null;
            using (IServiceSupplier serviceSupplier = new ServiceSupplier())
            {
                Supplier currentSupplier = serviceSupplier.GetSupplierByID(supplierId);
                if (currentSupplier != null && currentSupplier.ProofPdfDocument != null)
                {
                    pdfBinaryData = currentSupplier.ProofPdfDocument;
                }
            }
            if (pdfBinaryData == null || pdfBinaryData.Length == 0)
            {
                return NotFound();
            }
            return File(pdfBinaryData, "application/pdf", "justificatif.pdf");
        }

        [Authorize(Roles = "ADMIN, VOLUNTEER")]
        public IActionResult ShowAllUserAccounts()
        {
            using (ServiceUserAccount sUA = new ServiceUserAccount())
            {
                List<UserAccount> userAccounts = sUA.GetUserAccountsFull();

                List<UserAccountViewModel> viewModels = userAccounts.Select(userAccount =>
                {
                    UserAccountViewModel viewModel = new UserAccountViewModel
                    {
                        UserAccount = userAccount
                    };

                    if (userAccount.UserAccountLevel == UserAccountLevel.USER ||
                        userAccount.UserAccountLevel == UserAccountLevel.VOLUNTEER ||
                        userAccount.UserAccountLevel == UserAccountLevel.ADMIN)
                    {
                        if (userAccount.User != null && userAccount.User.ContactDetails != null)
                        {
                            string contactName = userAccount.User.ContactDetails.Name ?? "";
                            string contactFirstName = userAccount.User.ContactDetails.FirstName ?? "";
                            viewModel.ContactName = $"{contactName} {contactFirstName}".Trim();
                        }
                    }
                    else if (userAccount.UserAccountLevel == UserAccountLevel.CORPORATE)
                    {
                        if (userAccount.CorporateUser != null && userAccount.CorporateUser.CompanyDetails != null)
                        {
                            viewModel.ContactName = userAccount.CorporateUser.CompanyDetails.CompanyName;
                        }
                    }
                    else if (userAccount.UserAccountLevel == UserAccountLevel.SUPPLIER)
                    {
                        if (userAccount.Supplier != null && userAccount.Supplier.CompanyDetails != null)
                        {
                            viewModel.ContactName = userAccount.Supplier.CompanyDetails.CompanyName;
                        }
                    }

                    return viewModel;
                }).ToList();
                return View(viewModels);
            }
        }

        [Authorize(Roles = "ADMIN,VOLUNTEER")]
        public IActionResult UserDetails(int id)
        {
            UserAccountInfoUpdate viewModel = new UserAccountInfoUpdate();
            using (IServiceUserAccount serviceUserAccount = new ServiceUserAccount())
            {
                viewModel.UserAccountId = id;
                UserAccount currentAccount = serviceUserAccount.GetUserAccountByIDEager(id);
                viewModel.Mail = currentAccount.Mail;
                viewModel.ProfilePic = currentAccount.ProfilePic;
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

        private void UpdateRoleInAuthCookie(UserAccountLevel newRole)
        {
            // Get former cookie info
            ClaimsIdentity identity = (ClaimsIdentity) HttpContext.User.Identity;
            Claim? oldRoleClaim = identity.FindFirst(ClaimTypes.Role);
            // Remove old role
            if(oldRoleClaim != null)
            {
                identity.RemoveClaim(oldRoleClaim);
            }
            // Add new role
            Claim newRoleClaim = new Claim(ClaimTypes.Role, newRole.ToString());
            identity.AddClaim(newRoleClaim);
            
            //Recreate the cookie
            ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);
            HttpContext.SignInAsync(userPrincipal);

        }

    }
}
