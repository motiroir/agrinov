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


        [HttpGet]
        public IActionResult UpdateProduction(int id)
        {
            // Vérifier si l'ID est valide
            if (id <= 0)
            {
                return NotFound();
            }

            // Récupérer la production existante par son ID
            var oldProduction = sP.GetProductionByID(id);

            // Vérifier si la production existe
            if (oldProduction == null)
            {
                return NotFound();
            }

            // Créer le ViewModel avec les données de la production existante
            var viewModel = new ProductionViewModel
            {
                Production = oldProduction,
                YearOptions = GetEnumSelectListString<Years>(),
                ProductOptions = GetEnumSelectListString<ProductType>(),
                SeasonOptions = GetEnumSelectListString<Seasons>(),
                DeliveryFrequencyOptions = GetEnumSelectListString<DeliveryFrequency>(),
                ValidationStatusOptions = GetEnumSelectListString<ValidationStatus>()
            };

            // Retourner la vue avec le ViewModel
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult UpdateProduction(int id, ProductionViewModel viewModel)
        {
            // Vérifier si le modèle et la production sont valides
            if (viewModel == null || viewModel.Production == null)
            {
                return BadRequest("Données de production invalides.");
            }

            // Vérifier si l'ID correspond bien à une production existante
            var productionToUpdate = sP.GetProductionByID(id);
            if (productionToUpdate == null)
            {
                return NotFound();
            }

            // Mettre à jour les champs de la production
            productionToUpdate.ValidationStatus = viewModel.Production.ValidationStatus;
            productionToUpdate.DateLastModified = DateTime.Now;

            // Sauvegarder les modifications dans la base de données
            sP.UpdateProduction(productionToUpdate);

            return RedirectToAction("ShowAllProductions", "Production");
        }


        public IActionResult ShowAllProductions()
        {
                using (ServiceProduction sP = new ServiceProduction())
                {
                    // Récupérer la liste des productions
                    List<Production> productions = sP.GetProductions();

                    // Convertir chaque Production en ProductionViewModel
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

                    return View(viewModelList); // Passer la liste des ProductionViewModel à la vue
                }
            
        }

    }
}
