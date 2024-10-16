using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgriNov.Models;

namespace AgriNov.ViewModels
{
    public class SupplierWithProof
    {
        public Supplier Supplier {get; set;}
        public IFormFile PdfFile { get; set;}
    }
}