using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FuegoBox.Presentation.Models
{
    public class ViewCartModel
    {
        public double Total { get; set; }
        public IEnumerable<CartProductModel> CartProduct;
    }
}