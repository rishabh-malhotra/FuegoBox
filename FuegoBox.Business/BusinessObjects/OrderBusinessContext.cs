using FuegoBox.DAL.DBObjects;
using FuegoBox.Shared.DTO.Order;
using FuegoBox.Shared.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.Business.BusinessObjects
{
    public class OrderBusinessContext
    {
        OrderDBObject orderDBObject;
        CartDatabaseContext cdo;
        CartBusinessContext cartBusinessContext;
        public OrderBusinessContext()
        {
            orderDBObject = new OrderDBObject();
            cdo = new CartDatabaseContext();
            cartBusinessContext = new CartBusinessContext();
        }

        public bool PlaceOrder(AddressDTO od, Guid userid)
        {
            Guid addressid = orderDBObject.AddAddress(od, userid);
            CartsDTO cartsDTO = cdo.GetCart(userid);
            double subtotal = new double();
            foreach (var cartVariant in cartsDTO.CartItems)
            {
                subtotal = subtotal + (cartVariant.Variant.Discount* cartVariant.Qty);
            }
            cartsDTO.SubTotal = subtotal;
            orderDBObject.PlaceOrder(userid, cartsDTO, addressid);
            cartBusinessContext.EmptyCart(userid);
            return true;
        }

        public ViewOrderDTO viewOrder(Guid userid)
        {
            ViewOrderDTO vdto = orderDBObject.ViewOrder(userid);
            return vdto;
        }


    }
}
