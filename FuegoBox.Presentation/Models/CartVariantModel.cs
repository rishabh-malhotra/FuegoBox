using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FuegoBox.Presentation.Models
{
    public class CartVariantModel
    {
        public string Name;
        public VariantModel Variant;
        public Guid ID;
        public int Qty;
    }
}