using AutoMapper;
using FuegoBox.Business.BusinessObjects;
using FuegoBox.Presentation.Models;
using FuegoBox.Shared.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FuegoBox.Presentation.Controllers
{
    public class CardController : Controller
    {
        // GET: Card

        IMapper productmapper;

        ProductDetailContext productDetailContext;

        public CardController()
        {

            productDetailContext = new ProductDetailContext();


            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ProductDetail, ProductDetailDTO>();
            });

            

            productmapper = new Mapper(config);
            


        }
        public ActionResult CardDetail([Bind(Include = "Name")]ProductDetail productDetail)

        {
            ProductDetailDTO productDetailDTO = productmapper.Map<ProductDetail, ProductDetailDTO>(productDetail);
            ProductDetailDTO prodDetailDTO = productDetailContext.productAddToCart(productDetailDTO);
            ProductDetail p = productmapper.Map<ProductDetailDTO, ProductDetail>(prodDetailDTO);
            return View(p);


        }

    }
}