using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using FuegoBox.Business.BusinessObjects;
using FuegoBox.Presentation.Models;
using FuegoBox.Shared.DTO.Category;


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
            cdto = cdc.GetCategoryOnHomePage();
            categorymodel = catMapper.Map<CategoryDTO, CategoryModel>(cdto);
            return View(categorymodel);
            
        }
        
    }
}