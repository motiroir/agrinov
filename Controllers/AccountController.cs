using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AgriNov.Models;
using AgriNov.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AgriNov.Controllers
{
    public class AccountController : Controller
    {
        [Authorize(Roles = "DEFAULT")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "DEFAULT")]
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
                else
                {
                    return View("Error");
                }
            }
            if (!ModelState.IsValid)
            {
                using (IServiceUser serviceUser = new ServiceUser())
                {
                    serviceUser.InsertUser(user);
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
                    serviceCorporateUser.InsertCorporateUser(corporateUser);
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
                    serviceSupplier.InsertSupplier(viewModel.Supplier);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(viewModel);
        }

        [Authorize(Roles = "DEFAULT,SUPPLIER,USER,CORPORATE,ADMIN,VOLUNTEER")]
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

        [Authorize(Roles = "DEFAULT,SUPPLIER,USER,CORPORATE,ADMIN,VOLUNTEER")]
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
            return View(viewModel);
        }

        [Authorize(Roles = "SUPPLIER,USER,CORPORATE,ADMIN,VOLUNTEER")]
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

        [HttpGet]
        public IActionResult DownloadPdf(int supplierId)
        {
            byte[] pdfBinaryData;
            using(IServiceSupplier serviceSupplier = new ServiceSupplier())
            {
                pdfBinaryData = serviceSupplier.GetSupplierByID(supplierId).ProofPdfDocument;
            }
            if(pdfBinaryData == null || pdfBinaryData.Length == 0)
            {
                return NotFound();
            }
            return File(pdfBinaryData,"application/pdf","justificatif");
        }

    }
}
