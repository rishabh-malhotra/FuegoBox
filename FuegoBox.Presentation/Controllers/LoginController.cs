using AutoMapper;
using FuegoBox.Business.BusinessObjects;
using FuegoBox.Presentation.Models;
using FuegoBox.Shared.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FuegoBox.Business.Exceptions;

namespace FuegoBox.Presentation.Controllers
{
    public class LoginController : Controller
    {
        private UserBusinessContext userBusinessContext;
        IMapper LoginMapper;
        public LoginController()
        {
            userBusinessContext = new UserBusinessContext();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<LoginModel, UserLoginDTO>();
            });

            LoginMapper = new Mapper(config);

        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = " Email, Password")] LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                UserLoginDTO userLoginDTO = LoginMapper.Map<LoginModel, UserLoginDTO>(loginModel);
                try
                {
                    UserBasicDTO loggedInUserDTO = userBusinessContext.LoginUser(userLoginDTO);
                    Session["UserID"] = loggedInUserDTO.ID;
                    Session["name"] = loggedInUserDTO.Name;
                    ViewBag.MSG = loggedInUserDTO.ID;
                   // if (Request.UrlReferrer.ToString() == "a") { return null; }
                    return RedirectToAction("Index", "Home");
                }
                catch (InvalidLoginException ex)
                {

                    ModelState.AddModelError("", "Invalid Login Credentials.");
                    return View(loginModel);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Something went wrong, Please try again.");
                    return View("Error");
                }
            }
            return View(loginModel);
        
        }
        public ActionResult Logout()
        {
            Session.Clear();
           return RedirectToAction("Index", "Home");

        }
    }
}