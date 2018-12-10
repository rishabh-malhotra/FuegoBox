using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FuegoBox.Presentation.Models
{
    public class ProductsSearchModel
    {
        public string Name { get; set; }
        public IEnumerable<ProductDetail> Products;
    }
}