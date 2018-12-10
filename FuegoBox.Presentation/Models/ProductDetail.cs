using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace FuegoBox.Presentation.Models
{
    public class ProductDetail
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int OrderLimit { get; set; }
        public string ImageURL { get; set; }
        public Guid ID { get; set; }
        public IEnumerable<VariantModel> Variants { get; set; }
        public double ListingPrice { get; set; }
        public double Discount { get; set; }
        public string CatName { get; set; }
        public int QuantitySold { get; set; }
        public int Inventory { get; set; }

    }
}