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
    public class HomeController : Controller
    {
        IMapper catMapper;
        CategoryDetailContext cdc;
        public HomeController()
        {
            cdc = new CategoryDetailContext();
            var conf = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryDTO, CategoryModel>();
            });
            catMapper = new Mapper(conf);
        }
        public ActionResult Index()
        {
            CategoryModel categorymodel = new CategoryModel();
            CategoryDTO cdto = new CategoryDTO();


            // cdto = productlist;
            cdto = catMapper.Map<CategoryModel, CategoryDTO>(categorymodel);
            try
            {
                cdto = cdc.GetCategoryOnHomePage();

                categorymodel = catMapper.Map<CategoryDTO, CategoryModel>(cdto);
                return View(cdto);
            }
            catch (Exception)
            {
                return View("Internal Error");
            }
        }
    }
}