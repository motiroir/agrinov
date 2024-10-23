using AgriNov.Models;
using AgriNov.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;



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
        
        public IActionResult Index()
        {
            return View();
        }

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
                using (ServiceProduction sP = new ServiceProduction())
                {
                    
                    sP.InsertProduction(viewModel.Production);
                    return RedirectToAction("SupShowAllProductions", "Production");
                }
            }

            return View(viewModel);
        }

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

        public IActionResult SupShowAllProductions()
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

        [HttpPost]
        public IActionResult SupShowAllProductions(ProductionViewModel viewModel, string action, int selectedProductionId)
        {

            switch (action)
            {
                case "add":
                    return RedirectToAction("CreateProduction", "Production");

                
                case "update":
                    return UpdateProduction(selectedProductionId);

                default:
                    return View(viewModel);
            }
        }
    }
}
