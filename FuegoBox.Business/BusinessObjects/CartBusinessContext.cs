using FuegoBox.Business.Exceptions;
using FuegoBox.DAL.DBObjects;
using FuegoBox.DAL.Exceptions;
using FuegoBox.Shared.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.Business.BusinessObjects
{
    public class CartBusinessContext
    {
        CartDatabaseContext cartDatabaseContext=new CartDatabaseContext();
        public void AddToCart(CartDTO cartDTO)
        {
            try
            {
                bool alreadyPresent = cartDatabaseContext.PresentInCart(cartDTO);
            }


            catch (AlreadyPresentException ex)
            {
                throw new ItemAlreadyInCartException();
            }

            cartDatabaseContext.AddToCart(cartDTO);
        }
        public CartsDTO GetCart(Guid UserID)
        {
            CartsDTO newCartsDTO = cartDatabaseContext.GetCart(UserID);
            double subtotal = new double();
            foreach (var cartVariant in newCartsDTO.CartItems)
            {
                int OrderQuantity = cartVariant.Qty;
                double DiscountedPrice = cartVariant.Variant.Discount;
                subtotal += DiscountedPrice * OrderQuantity;
            }
            newCartsDTO.SubTotal = subtotal;
            return newCartsDTO;
        }
        public void RemoveItem(Guid UserID, Guid VariantID)
        {
            cartDatabaseContext.RemoveItem(UserID, VariantID);
        }

    }
}
