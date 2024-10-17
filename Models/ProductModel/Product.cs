using System;
using System.ComponentModel.DataAnnotations;

namespace AgriNov.Models
{
    public class Product
    {
     

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public string Category { get; set; }
    }
}
