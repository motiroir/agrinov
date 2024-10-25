﻿using AgriNov.Models;

namespace AgriNov.ViewModels
{
    public class ProductViewModel
    {
        public List<Product> AllProducts { get; set; }
        public Product Product { get; set; }
        public int QuantityByProductInCart { get; set; }
        public List<BoxContract> AllBoxContractsToSale { get; set; }
        public List<BoxContractSubscription> MyCurrentBoxContracts { get; set;}
        public string NextSeasonInfo {get; set; }
        public int QuantityByBoxContract {  get; set; }
        public string SupplierName { get; set; }
    }
}
