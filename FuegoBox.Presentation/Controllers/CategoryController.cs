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
        IMapper SearchResultVMMapper;

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

           var productSearchResultDTOConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductDetailDTO, ProductModel>();
                cfg.CreateMap<VariantDTO, VariantModel>();
                cfg.CreateMap<SearchResultsDTO, SearchResultsModel>();
            });

            productmapper = new Mapper(config);
            catMapper = new Mapper(conf);
            SearchResultVMMapper = new Mapper(productSearchResultDTOConfig);

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

        
        public ActionResult SearchResults(string SearchString)
        {
            if (String.IsNullOrEmpty(SearchString))
            {
                ViewBag.SearchString = "Search String Empty";
                return View();//TODO
            }
            SearchResultsDTO newProductsSearchResultDTO = new SearchResultsDTO();
            SearchResultsModel viewModel = new SearchResultsModel();
            try
            {
                newProductsSearchResultDTO = productDetailContext.GetProductsWithString(SearchString);
                viewModel = SearchResultVMMapper.Map<SearchResultsDTO, SearchResultsModel>(newProductsSearchResultDTO);
                ViewBag.SearchString = SearchString;
                return View(viewModel);
            }
            catch (Exception ex)
            {
                return View("Internal Error");
            }
        }
    }
}
