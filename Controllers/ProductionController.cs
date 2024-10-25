using AgriNov.Models;
using AgriNov.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;



namespace AgriNov.Controllers
{
    public class ProductionController : Controller
    {
        private readonly ServiceProduction sP = new ServiceProduction();
        
        private List<SelectListItem> GetEnumSelectListString<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)(object)e).ToString(),
                    Text = e.GetDisplayName()
                }).ToList();
        }

        [Authorize(Roles = "SUPPLIER")]
        [HttpGet]
        public IActionResult CreateProduction()
        {
            ProductionViewModel viewModel = new ProductionViewModel
            {
                YearOptions = GetEnumSelectListString<Years>(),
                ProductOptions = GetEnumSelectListString<ProductType>(),
                SeasonOptions = GetEnumSelectListString<Seasons>(),
                DeliveryFrequencyOptions = GetEnumSelectListString<DeliveryFrequency>(),
                ValidationStatusOptions = GetEnumSelectListString<ValidationStatus>(),
                Production = new Production()
            };

            return View(viewModel);
        }

        [Authorize(Roles = "SUPPLIER")]
        [HttpPost]
        public IActionResult CreateProduction(ProductionViewModel viewModel)
        {
            viewModel.YearOptions = GetEnumSelectListString<Years>();
            viewModel.ProductOptions = GetEnumSelectListString<ProductType>();
            viewModel.SeasonOptions = GetEnumSelectListString<Seasons>();
            viewModel.DeliveryFrequencyOptions = GetEnumSelectListString<DeliveryFrequency>();
            viewModel.ValidationStatusOptions = GetEnumSelectListString<ValidationStatus>();
           

            if (ModelState.IsValid)
            {
                int userId = int.Parse(HttpContext.User.Identity.Name);
                viewModel.Production.SupplierId = userId;
                using (ServiceProduction sP = new ServiceProduction())
                {
                   
                    
                    sP.InsertProduction(viewModel.Production);
                    return RedirectToAction("SupShowAllProductions", "Production");
                }
            }

            return View(viewModel);
        }

        [Authorize(Roles = "SUPPLIER,ADMIN")]
        public IActionResult UpdateProduction(int id)
        {
            
            if (id > 0)
            {
                Production oldProduction = sP.GetProductionByID(id);

                if (oldProduction != null)
                {
                    ProductionViewModel viewModel = new ProductionViewModel
                    {
                        Production = oldProduction,
                        YearOptions = GetEnumSelectListString<Years>(),
                        ProductOptions = GetEnumSelectListString<ProductType>(),
                        SeasonOptions = GetEnumSelectListString<Seasons>(),
                        DeliveryFrequencyOptions = GetEnumSelectListString<DeliveryFrequency>()
                    };

              

                    return View(viewModel);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }

        }

        [Authorize(Roles = "SUPPLIER,ADMIN")]
        [HttpPost]
        public IActionResult UpdateProduction(int id, ProductionViewModel viewModel)
        {
            

            viewModel.Production.Id = id;

            viewModel.YearOptions = GetEnumSelectListString<Years>();
            viewModel.ProductOptions = GetEnumSelectListString<ProductType>();
            viewModel.SeasonOptions = GetEnumSelectListString<Seasons>();
            viewModel.DeliveryFrequencyOptions = GetEnumSelectListString<DeliveryFrequency>();


            if (ModelState.IsValid)
            {
                int userId = int.Parse(HttpContext.User.Identity.Name);
                viewModel.Production.SupplierId = userId;

                if (id > 0)
                {
                    using (ServiceProduction sP = new ServiceProduction())
                    {
                        viewModel.Production.DateLastModified = DateTime.Now;
                      
                        sP.UpdateProduction(viewModel.Production);

                        return RedirectToAction("SupShowAllProductions", "Production");
                    }
                }
                return View(viewModel);
            }

            return View(viewModel);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult UpdateProductionValidation(int id)
        {
            
            if (id <= 0)
            {
                return NotFound();
            }

            
            var oldProduction = sP.GetProductionByID(id);

            
            if (oldProduction == null)
            {
                return NotFound();
            }

           
            var viewModel = new ProductionViewModel
            {
                Production = oldProduction,
                YearOptions = GetEnumSelectListString<Years>(),
                ProductOptions = GetEnumSelectListString<ProductType>(),
                SeasonOptions = GetEnumSelectListString<Seasons>(),
                DeliveryFrequencyOptions = GetEnumSelectListString<DeliveryFrequency>(),
                ValidationStatusOptions = GetEnumSelectListString<ValidationStatus>()
            };

          
            return View(viewModel);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public IActionResult UpdateProductionValidation(int id, ProductionViewModel viewModel)
        {
          
            if (viewModel == null || viewModel.Production == null)
            {
                return BadRequest("Données de production invalides.");
            }

           
            var productionToUpdate = sP.GetProductionByID(id);
            if (productionToUpdate == null)
            {
                return NotFound();
            }

            
             
            productionToUpdate.ValidationStatus = viewModel.Production.ValidationStatus;
            productionToUpdate.DateLastModified = DateTime.Now;

            sP.UpdateProduction(productionToUpdate);

            return RedirectToAction("ShowAllProductions", "Production");
        }

        [Authorize(Roles = "ADMIN")]
        public IActionResult ShowAllProductions()
        {
                using (ServiceProduction sP = new ServiceProduction())
                {
                   
                    List<Production> productions = sP.GetProductions();

                    
                    List<ProductionViewModel> viewModelList = productions.Select(p => new ProductionViewModel
                    {
                        Production = p,
                        YearOptions = GetEnumSelectListString<Years>(),
                        ProductOptions = GetEnumSelectListString<ProductType>(),
                        SeasonOptions = GetEnumSelectListString<Seasons>(),
                        DeliveryFrequencyOptions = GetEnumSelectListString<DeliveryFrequency>(),
                        ValidationStatusOptions = GetEnumSelectListString<ValidationStatus>(),
                        ValidationStatus = p.ValidationStatus

                    }).ToList();

                    return View(viewModelList); 
                }
            
        }

        [Authorize(Roles = "SUPPLIER")]
        public IActionResult SupShowAllProductions(int userid)
        {
            ProductionViewModel pVM= new ProductionViewModel();
            int userId = int.Parse(HttpContext.User.Identity.Name);
            using (ServiceProduction sP = new ServiceProduction())
            {
                Production production = sP.GetProductionByID(userId);
                pVM.Production = production;

                using (ServiceUserAccount sUA = new ServiceUserAccount())
                {
                    UserAccount supplier = sUA.GetUserAccountByIDEager(production.SupplierId);
                    pVM.SupplierName = sUA.GetUserFullName(supplier);
                }
            
                
                List<Production> productions = sP.GetProductions();
                pVM.ProductionsBySupplier = sP.GetProductionsBySupplier(userId);
                
                
               
            }
            return View(pVM);
        }

        [Authorize(Roles = "SUPPLIER")]
        [HttpPost]
        public IActionResult SupShowAllProductions(ProductionViewModel pVM, string action, int selectedProductionId)
        {
            
            switch (action)
            {
                case "add":
                    return RedirectToAction("CreateProduction", "Production");

                
                case "update":
                    
                        return UpdateProduction(selectedProductionId);
                    
               

                default:
                    return View(pVM);
            }
        }
    }
}
