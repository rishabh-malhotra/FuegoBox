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

        IMapper productmapper, cardMapper;

        ProductDetailContext productDetailContext;

        public CardController()
        {

            productDetailContext = new ProductDetailContext();


            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ProductDetail, ProductDetailDTO>();
            });

            var confi = new MapperConfiguration(cfg => {
                cfg.CreateMap<CardDTO, CardModel>();
            });

            productmapper = new Mapper(config);
            cardMapper = new Mapper(confi);


        }
        public ActionResult CardDetail([Bind(Include = "Name")]ProductDetail productDetail)

        {
            ProductDetailDTO productDetailDTO = productmapper.Map<ProductDetail, ProductDetailDTO>(productDetail);
            CardDTO prodDTO = productDetailContext.productAddToCart(productDetailDTO);
            CardModel p = cardMapper.Map<CardDTO, CardModel>(prodDTO);
            return View(p);


        }

    }
}