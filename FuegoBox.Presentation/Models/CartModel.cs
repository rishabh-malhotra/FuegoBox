using FuegoBox.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FuegoBox.Presentation.Models
{
    public class CartModel
    {

        public System.Guid VariantID { get; set; }
        public double SellingPrice { get; set; }
        public int Qty { get; set; }
        public Guid ProductID { get; set; }
        public virtual Variant Variant { get; set; }
        public string ImageURL { get; set; }
        public string Variant_Property { get; set; }
        public string Variant_Value1 { get; set; }
        public string Variant_Value2 { get; set; }
        
        public int OrderLimit { get; set; }

    }

}