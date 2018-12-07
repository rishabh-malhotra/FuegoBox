using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FuegoBox.Shared.DTO.Product
{
    public class ProductDetailDTO
    {
        public string CatName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int OrderLimit { get; set; }
       
        public IEnumerable<VariantDTO> VariantDetail { get; set; }
        public double ListingPrice { get; set; }
        public double Discount { get; set; }
        public string ImageURL { get; set; }
        public string img { get; set; }
    }
}