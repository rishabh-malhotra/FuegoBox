using AutoMapper;
using FuegoBox.Business.BusinessObjects;
using FuegoBox.Presentation.Models;
using FuegoBox.Shared.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FuegoBox.Presentation.Controllers
{
    public class RegisterController : Controller
    {
        UserBusinessContext userBusinessContext;
        IMapper RegistrationVMMapper;
        public RegisterController()
        {

            userBusinessContext = new UserBusinessContext();

            var config = new MapperConfiguration(cfg =>{
                cfg.CreateMap<RegisterModel, UserRegisterDTO>();
            });

            RegistrationVMMapper = new Mapper(config);


        }
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Name, Email, Password, PhoneNumber")] RegisterModel registerModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    UserRegisterDTO userRegisterDTO = RegistrationVMMapper.Map<RegisterModel, UserRegisterDTO>(registerModel);
                    UserBasicDTO userBasicDTO = userBusinessContext.RegisterUser(userRegisterDTO);    
                  return View("Success");
                }
                else
                {
                    return View(registerModel);
                }
            }
           
            catch (Exception ex)
            {

                ModelState.AddModelError("",ex+":Exception occured");
            }
            return View("Error");
        }









    }
}