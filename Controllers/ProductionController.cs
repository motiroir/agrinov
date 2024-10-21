using AgriNov.Models;
using AgriNov.Models.ProductionModel;
using AgriNov.Models.SharedStatus;
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
                    return RedirectToAction("Index", "Home");
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

                        return RedirectToAction("Index", "Home");
                    }
                }
                return View(viewModel);
            }

            return View(viewModel);
        }

        public IActionResult ShowAllProductions()
        {
            using (ServiceProduction sP = new ServiceProduction())
            {
                List<Production> productions = sP.GetProductions();
                return View(productions);
            }
        }

        public IActionResult UpdateProductionValidation(int id)
        {
            if (id > 0)
            {
                Production oldProduction = sP.GetProductionByID(id);

                if (oldProduction != null)
                {
                    ProductionViewModel viewModel = new ProductionViewModel
                    {
                        Production = oldProduction,
                        YearOptions = GetEnumSelectListInt<Years>(),
                        ProductOptions = GetEnumSelectListString<ProductType>(),
                        SeasonOptions = GetEnumSelectListString<Seasons>(),
                        DeliveryFrequencyOptions = GetEnumSelectListString<DeliveryFrequency>(),
                        ValidationStatusOptions = GetEnumSelectListString<ValidationStatus>()
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
        public IActionResult UpdateProductionValidation(int id, ProductionViewModel viewModel)
        {
            viewModel.Production.Id = id;

            Production newProduction = viewModel.Production;

            viewModel.YearOptions = GetEnumSelectListInt<Years>();
            viewModel.ProductOptions = GetEnumSelectListString<ProductType>();
            viewModel.SeasonOptions = GetEnumSelectListString<Seasons>();
            viewModel.DeliveryFrequencyOptions = GetEnumSelectListString<DeliveryFrequency>();
            viewModel.ValidationStatusOptions = GetEnumSelectListString<ValidationStatus>();


            if (ModelState.IsValid)
            {
                if (id > 0)
                {
                    using (ServiceProduction sP = new ServiceProduction())
                    {
                        viewModel.Production.DateLastModified = DateTime.Now;

                        sP.UpdateProduction(viewModel.Production);

                        return RedirectToAction("Index", "Home");
                    }
                }
                return View(viewModel);
            }

            return View(viewModel);
        }

    }
}
