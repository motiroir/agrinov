using AgriNov.Models;
using AgriNov.Models.ProductionModel;
using AgriNov.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Diagnostics;

namespace AgriNov.Controllers
{
    public class BoxContractController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

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
        private List<SelectListItem> GetEnumSelectListInt<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)(object)e).ToString(),
                    Text = ((int)(object)e).ToString()
                }).ToList();
        }

        [HttpGet]
        public IActionResult CreateBoxContract()
        {
            BoxContractViewModel viewModel = new BoxContractViewModel
            {
                YearOptions = GetEnumSelectListInt<Years>(),
                ProductOptions = GetEnumSelectListString<ProductType>(),
                SeasonOptions = GetEnumSelectListString<Seasons>(),
                BoxContract = new BoxContract()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CreateBoxContract(BoxContractViewModel viewModel, string action)
        {
            viewModel.YearOptions = GetEnumSelectListInt<Years>();
            viewModel.ProductOptions = GetEnumSelectListString<ProductType>();
            viewModel.SeasonOptions = GetEnumSelectListString<Seasons>();

            if (action == "calculate")
            {
                using (ServiceProduction sP = new ServiceProduction())
                {
                    Debug.WriteLine($"ProductType: {viewModel.ProductType}, Seasons: {viewModel.Seasons}, Years: {viewModel.BoxContract.Years}");
                    int stock = sP.CalculateStock(viewModel.BoxContract.ProductType, viewModel.BoxContract.Seasons, viewModel.BoxContract.Years);
                    

                    Debug.WriteLine($"Stock calculé: {stock}");
                    int quantityPerBox = (int)((stock / viewModel.BoxContract.MaxSubscriptions)/13);

                    viewModel.GlobalStock = stock;
                    viewModel.QuantityPerBox = quantityPerBox;

                    Debug.WriteLine($"GlobalStock: {viewModel.GlobalStock}, QuantityPerBox: {viewModel.QuantityPerBox}");

                }

                return View(viewModel);
            }

            else if (action == "validate")
            {
                if (ModelState.IsValid)
                {
                    using (ServiceBoxContract sBC = new ServiceBoxContract())
                    {
                        sBC.InsertBoxContract(viewModel.BoxContract);
                        return RedirectToAction("ShowAllBoxContracts", "BoxContract");
                    }
                }
            }

            return View(viewModel);
        }

        public IActionResult ShowAllBoxContracts()
        {
            using (ServiceBoxContract sBC = new ServiceBoxContract())
            {
                List<BoxContract> boxContracts = sBC.GetAllBoxContracts();
                var viewModel = boxContracts.Select(boxContract => new BoxContractViewModel
                {
                    BoxContract = boxContract,
                    ProductOptions = GetEnumSelectListString<ProductType>(),
                    SeasonOptions = GetEnumSelectListString<Seasons>(),
                    ProductType = boxContract.ProductType,
                    Seasons = boxContract.Seasons,
                }).ToList();

                return View(viewModel);
            }
        }
    }
}
