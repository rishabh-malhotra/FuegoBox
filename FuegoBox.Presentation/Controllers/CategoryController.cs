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
      
        public ActionResult Books()
        {
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
    }
}