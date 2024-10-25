using AgriNov.Models;
using AgriNov.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Diagnostics;
using static System.Collections.Specialized.BitVector32;

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
        

        [HttpGet]
        public IActionResult CreateBoxContract()
        {
            ViewBag.ContractExists = false;
            BoxContractViewModel viewModel = new BoxContractViewModel
            {
                YearOptions = GetEnumSelectListString<Years>(),
                ProductOptions = GetEnumSelectListString<ProductType>(),
                SeasonOptions = GetEnumSelectListString<Seasons>(),
                BoxContract = new BoxContract()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CreateBoxContract(BoxContractViewModel viewModel, string action)
        {
            ViewBag.ContractExists = false;
            viewModel.YearOptions = GetEnumSelectListString<Years>();
            viewModel.ProductOptions = GetEnumSelectListString<ProductType>();
            viewModel.SeasonOptions = GetEnumSelectListString<Seasons>();

            if (action == "calculate")
            {
                if (ModelState.IsValid)
                {
                    using (ServiceProduction sP = new ServiceProduction())
                    {
                       
                        int stock = sP.CalculateStock(viewModel.ProductType, viewModel.Seasons, viewModel.Years);
                        viewModel.GlobalStock = stock;
                        viewModel.BoxContract.MaxSubscriptions = (int)(stock / (viewModel.QuantityPerBox * 13));

                        ViewBag.ContractExists = false;
                        return View(viewModel);
                    }
                }

            }

            else if (action == "validate")
            {
                if (ModelState.IsValid)
                {
                    using (ServiceBoxContract sBC = new ServiceBoxContract())
                    {
                        bool contractExists = sBC.ContractExist(viewModel.ProductType, viewModel.Seasons, viewModel.Years);


                        if (!contractExists)
                        {
                            using (ServiceProduction sP = new ServiceProduction())
                            {
                                int stock = sP.CalculateStock(viewModel.ProductType, viewModel.Seasons, viewModel.Years);
                                viewModel.GlobalStock = stock;
                                viewModel.BoxContract.MaxSubscriptions = (int)(stock / (viewModel.QuantityPerBox * 13));
                            }
                            viewModel.BoxContract.ProductType = viewModel.ProductType;
                            viewModel.BoxContract.Seasons = viewModel.Seasons;
                            viewModel.BoxContract.Years = viewModel.Years;

                            sBC.InsertBoxContract(viewModel.BoxContract);
                            return RedirectToAction("ShowAllBoxContracts", "BoxContract");
                        }
                        else
                        {
                            ViewBag.ContractExists = true;
                            return View(viewModel);
                        }
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
                List<BoxContractViewModel> viewModel = boxContracts.Select(boxContract => new BoxContractViewModel
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


        [HttpPost]
        public IActionResult ShowAllBoxContracts(BoxContractViewModel viewModel, string action, int selectedBoxContractId)
        {

            switch (action)
            {
                case "add":
                    return RedirectToAction("CreateBoxContract", "BoxContract");

                case "delete":
                    // popup : are you sure ?
                    // delete message
                    return RedirectToAction("ShowAllBoxContracts", "BoxContract");

                case "update":
                    return UpdateBoxContract(selectedBoxContractId);

                default:
                    return View(viewModel);
            }
        }

        [HttpGet]
        public IActionResult UpdateBoxContract(int id)
        {
            ViewBag.ContractExists = false;

            using (ServiceBoxContract sBC = new ServiceBoxContract())
            {
                if (id > 0)
                {
                    BoxContract oldBoxContract = sBC.GetBoxContractById(id);

                    if (oldBoxContract != null)
                    {
                        BoxContractViewModel viewModel = new BoxContractViewModel
                        {
                            BoxContract = oldBoxContract,
                            ProductType = oldBoxContract.ProductType,
                            Years = oldBoxContract.Years,
                            Seasons = oldBoxContract.Seasons,
                            YearOptions = GetEnumSelectListString<Years>(),
                            ProductOptions = GetEnumSelectListString<ProductType>(),
                            SeasonOptions = GetEnumSelectListString<Seasons>(),
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
        }


        [HttpPost]
        public IActionResult UpdateBoxContract(string action, int id, BoxContractViewModel viewModel)
        {
            ViewBag.ContractExists = false;
            viewModel.BoxContract.Id = id;

            viewModel.YearOptions = GetEnumSelectListString<Years>();
            viewModel.ProductOptions = GetEnumSelectListString<ProductType>();
            viewModel.SeasonOptions = GetEnumSelectListString<Seasons>();

            if (action == "calculate")
            {
                if (ModelState.IsValid)
                {
                    
                        
                    using (ServiceProduction sP = new ServiceProduction())
                    {
                        int stock = sP.CalculateStock(viewModel.ProductType, viewModel.Seasons, viewModel.Years);

                        int quantityPerBox = (int)((stock / viewModel.BoxContract.MaxSubscriptions) / 13);

                        viewModel.GlobalStock = stock;
                        viewModel.QuantityPerBox = quantityPerBox;

                        ViewBag.ContractExists = false;
                        return View(viewModel);
                    }   
                        
                    
                }
            }

            else if (action == "validate")
            {
                if (ModelState.IsValid)
                {
                    if (id > 0)
                    {
                        using (ServiceBoxContract sBC = new ServiceBoxContract())
                        {
                            bool contractExists = sBC.ContractExist(id, viewModel.ProductType, viewModel.Seasons, viewModel.Years);

                            if (!contractExists)
                            {
                                viewModel.BoxContract.DateLastModified = DateTime.Now;
                                viewModel.BoxContract.ProductType = viewModel.ProductType;
                                viewModel.BoxContract.Seasons = viewModel.Seasons;
                                viewModel.BoxContract.Years = viewModel.Years;

                                sBC.UpdateBoxContract(viewModel.BoxContract);

                                return RedirectToAction("ShowAllBoxContracts", "BoxContract");
                            }
                            else
                            {
                                ViewBag.ContractExists = true;
                                return View(viewModel);
                            }
                        }
                    }
                    return View(viewModel);
                }
            }
            return View(viewModel);
        }



        [HttpPost]
        public IActionResult UpdateForSaleStatus(int id, bool forSale)
        {
            Debug.WriteLine(id);

            using (ServiceBoxContract sBC = new ServiceBoxContract())
            {
                var boxContract = sBC.GetBoxContractById(id);
                if (boxContract != null)
                {
                    
                    if (boxContract.ForSale)
                    {
                        boxContract.ForSale = false;
                    }
                    else
                    {
                        boxContract.ForSale = true;
                    }
                    Debug.WriteLine(boxContract.ForSale);
                    sBC.UpdateBoxContract(boxContract); 
                }
            }

            return RedirectToAction("ShowAllBoxContracts", "BoxContract");
        }


        [HttpDelete]
        public IActionResult DeleteBoxContract(int id)
        {
            using (ServiceBoxContract sBC = new ServiceBoxContract())
            {
                sBC.DeleteBoxContract(id);
                return Ok();
            }
        }


    }
}
