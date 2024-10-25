﻿using AgriNov.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgriNov.ViewModels
{
    public class ProductionViewModel
    {
        public Production Production { get; set; }
        public List<SelectListItem> YearOptions { get; set; }
        public List<SelectListItem> ProductOptions { get; set; }
        public List<SelectListItem> SeasonOptions { get; set; }
        public List<SelectListItem> DeliveryFrequencyOptions { get; set; }
        public List<SelectListItem> ValidationStatusOptions { get; set; }

        public List<Production> ViewModelList { get; set; }

        public List <Production> ProductionsBySupplier { get; set; }

        public UserAccount UserAccount { get; set; }
        public string SupplierName { get; set; }

       

        public int Year { get; set; }
        public ProductType ProductType { get; set; }
        public Seasons Season { get; set; }
        public DeliveryFrequency DeliveryFrequency { get; set; }

        public ValidationStatus ValidationStatus { get; set; }

     
    }
}
