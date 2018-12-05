using FuegoBox.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FuegoBox.Presentation.Models
{
    public class CardModel
    {

        public System.Guid VariantID { get; set; }
        public double SellingPrice { get; set; }
        public int Qty { get; set; }
        public virtual Variant Variant { get; set; }
    }
}