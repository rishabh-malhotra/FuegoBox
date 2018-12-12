using AutoMapper;
using FuegoBox.DAL.Exceptions;
using FuegoBox.Shared.DTO.Product;
using FuegoBox.Shared.DTO.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.DAL.DBObjects
{
    public class CartDatabaseContext
    {
        FuegoEntities dbContext;
        IMapper CartItemsMapper;
        public CartDatabaseContext()
        {
            var cartItemsConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Variant, VariantDTO>();
                cfg.CreateMap<Cart, CartVariantDTO>();
                cfg.CreateMap<Product, ProductDetailDTO>();
            });
            CartItemsMapper = new Mapper(cartItemsConfig);
            dbContext = new FuegoEntities();
        }


        public bool PresentInCart(CartDTO cartDTO)
        {

            IEnumerable<Cart> carts = dbContext.Cart.Where(p => p.UserID == cartDTO.UserID);

            Variant variant = dbContext.Variant.Where(pro => pro.ProductID == cartDTO.ProductID).FirstOrDefault();
                //dbContext.Variant.Where(cdd=>cdd.ID==cartDTO.VariantID).ToList();

            foreach (var cart in carts)
            {
      
                    if (cart.VariantID == variant.ID)
                    {
                        return false;
                    }
                
            }
            return true;
        }

    
        public bool AddToCart(Guid id,Guid user_id)
        {
            Product product = dbContext.Product.Where(a => a.ID == id).FirstOrDefault();
            if (product == null)
            {
                dbContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                Variant variant = dbContext.Variant.Where(s => s.ID == id).FirstOrDefault();

                Guid p_id = variant.ProductID;
                Product pr = dbContext.Product.Where(cdd => cdd.ID == p_id).FirstOrDefault();
                VariantImage vimage = dbContext.VariantImage.Where(cdd => cdd.VariantID == variant.ID).FirstOrDefault();
                ProductDetailDTO cartdto = new ProductDetailDTO();
                IEnumerable<Cart> ca = dbContext.Cart.Where(pd => pd.UserID == user_id);
                foreach (var cd in ca)
                {
                    if (cd.VariantID == variant.ID)
                    {
                        return false;
                    }
                }

                Cart cart = new Cart();
                cart.ID = Guid.NewGuid();
                cart.VariantID = variant.ID;
                cart.SellingPrice = variant.Discount;
                cart.Qty = 2;
                cart.UserID = user_id;
                cartdto.Name = pr.Name;
                cartdto.ImageURL = vimage.ImageURL;
                dbContext.Cart.Add(cart);
                dbContext.SaveChanges();
                return true;


            }
            else
            {
                dbContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                Variant variant = dbContext.Variant.Where(s => s.ProductID == id).FirstOrDefault();
                //Guid p_id = variant.ProductID;               
                VariantImage vimage = dbContext.VariantImage.Where(cdd => cdd.VariantID == variant.ID).FirstOrDefault();
                ProductDetailDTO cartdto = new ProductDetailDTO();
                IEnumerable<Cart> ca = dbContext.Cart.Where(pd => pd.UserID == user_id);
                foreach (var cd in ca)
                {
                    if (cd.VariantID == variant.ID)
                    {
                        return false;
                    }
                }
                Cart cart = new Cart();
                cart.ID = Guid.NewGuid();
                cart.VariantID = variant.ID;
                cart.SellingPrice = variant.Discount;
                cart.Qty = 1;
                cart.UserID = user_id;
                cartdto.Name = product.Name;
                cartdto.ImageURL = vimage.ImageURL;
                dbContext.Cart.Add(cart);
                dbContext.SaveChanges();
                return true;
            }
        }



        //function to display all the cart items according to the user...currently logged in ID of the User is userID
        public CartsDTO GetCart(Guid UserID)
        {
            var cart = dbContext.Cart.ToList();
            CartsDTO cdto = new CartsDTO();
            cdto.CartItems = CartItemsMapper.Map<IEnumerable<Cart>, IEnumerable<CartVariantDTO>>(cart);
            //cdto.Image = imag.ImageURL;
            VariantImage imag = new VariantImage();
            dbContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            foreach (var cd in cdto.CartItems)
            {
                imag = dbContext.VariantImage.Where(vi => vi.VariantID == cd.Variant.ID).First();
                cd.Variant.image = imag.ImageURL;
            }
            return cdto;

        }

        //function to remove the item from the cart according to the logged in user and variant id...
        public void RemoveItem(Guid UserID, Guid VariantID)
        {
            dbContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            dbContext.Cart.RemoveRange(dbContext.Cart.Where(c => c.UserID == UserID && c.VariantID == VariantID));
            dbContext.SaveChanges();
            return;
        }

        public void EmptyCart(Guid UserID)
        {
            dbContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            dbContext.Cart.RemoveRange(dbContext.Cart.Where(cart => cart.UserID == UserID));
            dbContext.SaveChanges();
        }
    }
}
