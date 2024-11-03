using AgriNov.Models;

namespace AgriNov.ViewModels
{
    public class SupplierWithProof
    {
        public Supplier Supplier {get; set;}
        public IFormFile PdfFile { get; set;}
    }
}