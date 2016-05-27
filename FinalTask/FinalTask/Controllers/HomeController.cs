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
                //проверяем валидность комбинации логина и пароля
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
        
        /// <summary>
        /// Действие Search возвращает страницу с поиском файлов
        /// </summary>
        /// <returns></returns>
        [Authorize]
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
        [Authorize]
        public ActionResult Search(Models.SearchParams Params)
        {
            //инициализируем строки если они пустые
            if (Params.NamePattern == null)
                Params.NamePattern = "";
            if (Params.OwnerNamePattern == null)
                Params.OwnerNamePattern = "";

            //проверяем роль
            if (!User.IsInRole("Admin"))
                return Redirect("~/");
            
            //осуществляем поиск
            List<Models.FileModel> files = new List<Models.FileModel>();
            foreach (FileEntity f in Logic.GetAllFiles())
                if (f.Extension != "folder" && f.Name.Contains(Params.NamePattern) 
                    && f.Owner.Name.Contains(Params.OwnerNamePattern))
                    files.Add(new Models.FileModel(f));

            //сохраняем параметры поиска
            ViewBag.NamePattern = Params.NamePattern;
            ViewBag.OwnerNamePattern = Params.OwnerNamePattern;
            ViewBag.Searching = 1;
            return View(files);
        }

        /// <summary>
        /// Действие UserList возвращает стрницу списка пользователей
        /// </summary>
        /// <returns></returns>
        [Authorize]
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
        [Authorize]
        public ActionResult UserList(string pattern)
        {
            //инициализируем строку если она пуста
            if (pattern == null) 
                pattern = "";

            //проверяем роль
            if (!User.IsInRole("Admin"))
                return Redirect("~/");

            //осуществляем поиск
            List<Models.UserModel> users = new List<Models.UserModel>();
            foreach (User u in Logic.GetAllUsers())
                if (u.Name.Contains(pattern))
                    users.Add(new Models.UserModel(u, Logic.GetFileID(u.Name)));

            //сохраняем параметры поиска
            ViewBag.NamePattern = pattern;
            ViewBag.Searching = 1;
            return View(users);
        }
    }
}