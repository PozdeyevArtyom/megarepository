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
        
        public ActionResult Search()
        {
            if (!User.IsInRole("Admin"))
                return Redirect("~/");
            List<Models.FileModel> files = new List<Models.FileModel>();
            foreach (FileEntity f in Logic.GetAllFiles())
                if (f.Extension != "folder")
                    files.Add(new Models.FileModel(f));
            return View(files);
        }

        [HttpPost]
        public ActionResult Search(Models.SearchParams Params)
        {
            if (Params.NamePattern == null)
                Params.NamePattern = "";
            if (Params.OwnerNamePattern == null)
                Params.OwnerNamePattern = "";
            if (!User.IsInRole("Admin"))
                return Redirect("~/");
            List<Models.FileModel> files = new List<Models.FileModel>();
            foreach (FileEntity f in Logic.GetAllFiles())
                if (f.Extension != "folder" && f.Name.Contains(Params.NamePattern) 
                    && f.Owner.Name.Contains(Params.OwnerNamePattern))
                    files.Add(new Models.FileModel(f));
            ViewBag.NamePattern = Params.NamePattern;
            ViewBag.OwnerNamePattern = Params.OwnerNamePattern;
            ViewBag.Searching = 1;
            return View(files);
        }

        public ActionResult UserList()
        {
            if (!User.IsInRole("Admin"))
                return Redirect("~/");
            List<Models.UserModel> users = new List<Models.UserModel>();
            foreach (User u in Logic.GetAllUsers())
                users.Add(new Models.UserModel(u, Logic.GetFileID(u.Name)));
            return View(users);
        }

        [HttpPost]
        public ActionResult UserList(string pattern)
        {
            if (!User.IsInRole("Admin"))
                return Redirect("~/");
            List<Models.UserModel> users = new List<Models.UserModel>();
            foreach (User u in Logic.GetAllUsers())
                if (u.Name.Contains(pattern))
                    users.Add(new Models.UserModel(u, Logic.GetFileID(u.Name)));
            ViewBag.NamePattern = pattern;
            ViewBag.Searching = 1;
            return View(users);
        }
    }
}