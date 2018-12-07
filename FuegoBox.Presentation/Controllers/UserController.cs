using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FuegoBox.Presentation.ActionFilters;
using FuegoBox.Business.BusinessObjects;

namespace FuegoBox.Presentation.Controllers
{
    public class UserController:Controller
    {
        UserBusinessContext userBusinessContext = new UserBusinessContext();
        [UserAuthenticationFilter]
        public ActionResult CheckAdmin()
        {
            try
            {
                if (userBusinessContext.CheckAdmin(new Guid(Session["UserID"].ToString())))
                {
                    return View("Admin");
                }
                else
                {
                    return View("NotAdmin");
                }
            }
            catch (Exception)
            {
                return View("InternalError");
            }
        }
    }
}