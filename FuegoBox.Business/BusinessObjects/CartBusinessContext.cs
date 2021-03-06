﻿using FuegoBox.Business.Exceptions;
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

        //function to add items to the cart 
        public bool AddToCart(Guid id,Guid Userid)
        {

            //   bool alreadyPresent = cartDatabaseContext.PresentInCart(cartDTO);
            bool cdto = cartDatabaseContext.AddToCart(id, Userid);
            return cdto;


        }

        //function to view the cart according to the user...
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

        //function to remove the item from the cart....
        public void RemoveItem(Guid UserID, Guid VariantID)
        {
            cartDatabaseContext.RemoveItem(UserID, VariantID);
        }
        public bool EmptyCart(Guid UserID)
        {
            cartDatabaseContext.EmptyCart(UserID);
            return true;
        }

    }
}
