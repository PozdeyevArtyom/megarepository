using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Entities;
using System.Web.Security;

namespace FinalTask.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(Models.RegistredUserModel regUserModel)
        {
            if (ModelState.IsValid)
            {
                User user = new User(regUserModel.Name, regUserModel.Password, DateTime.Now, regUserModel.Email);
                try
                {
                    Logic.RegisterUser(user);
                }
                catch(ArgumentException e)
                {
                    ModelState.AddModelError(e.ParamName, e.Message);
                }
                return Redirect("Login");
            }
            else
                return View(regUserModel);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.LoggedUserModel loggedUserModel)
        {
            if(ModelState.IsValid)
            {
                if (Logic.Auth(loggedUserModel.Name, loggedUserModel.Password))
                {
                    FormsAuthentication.SetAuthCookie(loggedUserModel.Name, false);
                    return Redirect("~/");
                }
                else
                    ModelState.AddModelError("Password", "Неверная комбинация логина и пароля.");
            }
            return View(loggedUserModel);
        }        
    }
}