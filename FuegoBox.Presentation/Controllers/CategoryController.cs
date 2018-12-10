using AutoMapper;
using FuegoBox.Business.BusinessObjects;
using FuegoBox.Business.Exceptions;
using FuegoBox.Presentation.ActionFilters;
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
        IMapper ProductsSearchResultVMMapper;

        public CategoryController()
        {

            productDetailContext = new ProductDetailContext();
            categoryDetailContext = new CategoryDetailContext();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductDetail, ProductDetailDTO>();
            });

            var conf = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryDTO, CategoryModel>();
            });

            var conf1 = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductSearchResultDTO, ProductsSearchModel>();
            });

            productmapper = new Mapper(config);
            catMapper = new Mapper(conf);
            ProductsSearchResultVMMapper = new Mapper(conf1);

        }
        

        [UserAuthenticationFilter]
        public ActionResult PDetail([Bind(Include = "Name")] ProductDetail productDetail)
        {
            ProductDetailDTO prodDetailDTO = new ProductDetailDTO();
            try
            {
                ProductDetailDTO productDetailDTO = productmapper.Map<ProductDetail, ProductDetailDTO>(productDetail);
                prodDetailDTO = productDetailContext.GetProductDetail(productDetailDTO);
            }
            catch (CategoryDoesNotExistsException ex)
            {
                return View("Error" + ex);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex + ":Exception occured");
            }
            ProductDetail p = productmapper.Map<ProductDetailDTO, ProductDetail>(prodDetailDTO);
            return View(p);
            
        }


        public ActionResult SearchResult(string searchString)
        {
            if (Session["UserID"] != null)
            {
                ViewBag.IsLoggedIn = "True";
            }
            if (String.IsNullOrEmpty(searchString))
            {

                return View();//TODO
            }
            try
            {
                ProductSearchResultDTO newProductsSearchResultDTO = new ProductSearchResultDTO();
                ProductsSearchModel viewModel = new ProductsSearchModel();
                
                newProductsSearchResultDTO = productDetailContext.GetProductwithString(searchString);
                if (newProductsSearchResultDTO.Products.Count() == 0)
                {
                    return View("NoSearch");
                }
                viewModel = ProductsSearchResultVMMapper.Map<ProductSearchResultDTO, ProductsSearchModel>(newProductsSearchResultDTO);
                ViewBag.searchString = searchString;
                return View(viewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex + ":Exception occured");
            }
            return View();
        }

        public ActionResult ViewProductCategory(string CategoryName)
        {

            CategoryModel categorymodel = new CategoryModel();
            CategoryDTO cdto = new CategoryDTO();
            cdto = categoryDetailContext.GetCategoryProduct(CategoryName);
            categorymodel = catMapper.Map<CategoryDTO, CategoryModel>(cdto);
            ViewBag.CategoryName = CategoryName;
            return View(categorymodel);
        }

    }
}

