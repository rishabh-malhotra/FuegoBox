using AutoMapper;
using FuegoBox.Business.BusinessObjects;
using FuegoBox.Business.Exceptions;
using FuegoBox.Presentation.ActionFilters;
using FuegoBox.Presentation.Models;
using FuegoBox.Shared.DTO.Product;
using FuegoBox.Shared.DTO.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FuegoBox.Presentation.Controllers
{
    public class CartController : Controller
    {
        // GET: Card
        CartBusinessContext cartBusinessContext;
        IMapper productmapper;
        IMapper CartMapper;
        IMapper CartsMapper;

        ProductDetailContext productDetailContext;

        public CartController()
        {

            productDetailContext = new ProductDetailContext();
            cartBusinessContext = new CartBusinessContext();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ProductDetail, ProductDetailDTO>();
            });

            var config2 = new MapperConfiguration(cfg =>
              {
                  cfg.CreateMap<ProductDetailDTO, ProductModel>();
                  cfg.CreateMap<VariantDTO, VariantModel>();
                  cfg.CreateMap<CartVariantDTO, CartVariantModel>();
                  cfg.CreateMap<CartDTO, CartModel>();
                  cfg.CreateMap<CartModel,CartDTO>();
              });

            var config3 = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductDetailDTO, ProductModel>();
                cfg.CreateMap<VariantDTO, VariantModel>();
                cfg.CreateMap<CartVariantDTO, CartVariantModel>();
            });


            productmapper = new Mapper(config);
            CartMapper = new Mapper(config2);
            CartsMapper = new Mapper(config3);

        }
        //[UserAuthenticationFilter]
        //public ActionResult CartDetail([Bind(Include = "Name")]ProductDetail productDetail)

        //{
        //    ProductDetailDTO productDetailDTO = productmapper.Map<ProductDetail, ProductDetailDTO>(productDetail);
        //    ProductDetailDTO prodDetailDTO = productDetailContext.productAddToCart(productDetailDTO);
        //    ProductDetail p = productmapper.Map<ProductDetailDTO, ProductDetail>(prodDetailDTO);
        //    return View(p);


        //}
        [UserAuthenticationFilter]
        public ActionResult CartDetail()
        {
            CartsDTO newCartsDTO = cartBusinessContext.GetCart(new Guid(Session["UserID"].ToString()));
            CartsModel cartsModel = new CartsModel();
            cartsModel.CartItems = CartsMapper.Map<IEnumerable<CartVariantDTO>, IEnumerable<CartVariantModel>>(newCartsDTO.CartItems);
            cartsModel.CartItems = cartsModel.CartItems.ToList();
            cartsModel.SubTotal = newCartsDTO.SubTotal;
            cartsModel.IsLoggedIn = true;
            return View(cartsModel);
        }

        [HttpPost]
        public ActionResult AddToCart([Bind (Include="ProductID")] CartModel cartmodel)
        {
            CartMessageModel cartMessageModel = new CartMessageModel();

          
                CartDTO cartDTO = CartMapper.Map<CartDTO>(cartmodel);
                cartDTO.UserID = new Guid(Session["UserID"].ToString());
                cartDTO.ProductID = cartmodel.ProductID;
                bool result=  cartBusinessContext.AddToCart(cartDTO);
                if (result == true)
                {
                
                    cartMessageModel.SuccessMessage = "Item Successfuly Added to Cart";
                    cartMessageModel.IsLoggedIn = true;
                    return View(cartMessageModel);
                }
                else
                {
                    return View("NotAdded");
                }
               
          
           
        }
        public ActionResult RemoveItem(Guid VariantID)
        {
            cartBusinessContext.RemoveItem(new Guid(Session["UserID"].ToString()), VariantID);
            return RedirectToAction("CartDetail");
        }

    }
}