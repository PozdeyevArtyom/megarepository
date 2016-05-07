using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLL;

namespace FinalTask.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult ProfilePage(Models.UserModel user)
        {
            user = new Models.UserModel(Logic.GetUserByName(user.Name));
            return View(user);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("~/");
        }

        public ActionResult Storage(Models.FolderModel folder)
        {
            folder.SubFolders = Logic.GetSubfolders(folder.Name, User.Identity.Name);
            folder.Files = Logic.GetFiles(folder.Name, User.Identity.Name);
            return View(folder);
        }
    }
}