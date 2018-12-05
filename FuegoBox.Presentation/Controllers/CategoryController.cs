
using AutoMapper;
using FuegoBox.Business.BusinessObjects;
using FuegoBox.Presentation.Models;
using FuegoBox.Shared.DTO.Category;
using FuegoBox.Shared.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FuegoBox.Presentation.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        IMapper productmapper;
        IMapper catMapper;
        ProductDetailContext productDetailContext;
        CategoryDetailContext categoryDetailContext;

        public CategoryController()
        {

            productDetailContext = new ProductDetailContext();
            categoryDetailContext = new CategoryDetailContext();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ProductDetail, ProductDetailDTO>();
            });

            var conf = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryDTO, CategoryModel>();
            });

            productmapper = new Mapper(config);
            catMapper = new Mapper(conf);


        }

        public ActionResult Book()
        {
            String catName = "Books";

            CategoryModel categorymodel = new CategoryModel();
            CategoryDTO cdto = new CategoryDTO();
            cdto = categoryDetailContext.GetCategoryProduct(catName);
            categorymodel = catMapper.Map<CategoryDTO, CategoryModel>(cdto);
            return View(categorymodel);

        }


        public ActionResult PDetail([Bind(Include = "Name")] ProductDetail productDetail)
        {
            try
            {
                ProductDetailDTO productDetailDTO = productmapper.Map<ProductDetail, ProductDetailDTO>(productDetail);
                ProductDetailDTO prodDetailDTO = productDetailContext.GetProductDetail(productDetailDTO);
                ProductDetail p = productmapper.Map<ProductDetailDTO, ProductDetail>(prodDetailDTO);
                return View(p);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex + ":Exception occured");
            }
            return View();
        }
        public ActionResult Watches()
        {
            return View();

        }
        public ActionResult Clothing()
        {
            return View();
        }
        public ActionResult Television()
        {
            return View();
        }
        public ActionResult Mobile()
        {
            return View();
        }
        public ActionResult Footwear()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SearchResult()
        {
            return View();
        }
    }
}