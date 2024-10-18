﻿using AgriNov.Models;
using AgriNov.Models.ProductionModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgriNov.ViewModels
{
    public class BoxContractViewModel
    {
        public BoxContract BoxContract { get; set; }
        public List<SelectListItem> YearOptions { get; set; }
        public List<SelectListItem> ProductOptions { get; set; }
        public List<SelectListItem> SeasonOptions { get; set; }

        public Years Years { get; set; }         
        public ProductType ProductType { get; set; } 
        public Seasons Seasons { get; set; }
    }
}
