using AgriNov.Models;
using AgriNov.Models.ProductionModel;
using AgriNov.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgriNov.Controllers
{
    public class BoxContractController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateBoxContract()
        {
            BoxContractViewModel viewModel = new BoxContractViewModel
            {
                YearOptions = Enum.GetValues(typeof(Years))
                    .Cast<Years>()
                    .Select(e => new SelectListItem
                    {
                        Value = ((int)e).ToString(),
                        Text = ((int)e).ToString()
                    }).ToList(),
                ProductOptions = Enum.GetValues(typeof(ProductType))
                    .Cast<ProductType>()
                    .Select(e => new SelectListItem
                    {
                        Value = ((int)e).ToString(),
                        Text = e.GetDisplayName()
                    }).ToList(),
                SeasonOptions = Enum.GetValues(typeof(Seasons))
                    .Cast<Seasons>()
                    .Select(e => new SelectListItem
                    {
                        Value = e.ToString(),
                        Text = e.GetDisplayName()
                    }).ToList(),
                BoxContract = new BoxContract()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CreateBoxContract(BoxContractViewModel viewModel)
        {
            viewModel.YearOptions = Enum.GetValues(typeof(Years))
                .Cast<Years>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = ((int)e).ToString()
                }).ToList();
            viewModel.ProductOptions = Enum.GetValues(typeof(ProductType))
                    .Cast<ProductType>()
                    .Select(e => new SelectListItem
                    {
                        Value = ((int)e).ToString(),
                        Text = e.GetDisplayName()
                    }).ToList();
            viewModel.SeasonOptions = Enum.GetValues(typeof(Seasons))
                .Cast<Seasons>()
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.GetDisplayName()
                }).ToList();

            if (ModelState.IsValid)
            {
                using (ServiceBoxContract sBC = new ServiceBoxContract())
                {
                    sBC.InsertBoxContract(viewModel.BoxContract);
                    return RedirectToAction("ShowAllBoxContracts", "BoxContract");
                }
            }
            
            return View(viewModel);
        }

        public IActionResult ShowAllBoxContracts()
        {
            using (ServiceBoxContract sBC = new ServiceBoxContract())
            {
                List<BoxContract> boxContracts = sBC.GetAllBoxContracts();
                return View(boxContracts);
            }
        }
    }
}
