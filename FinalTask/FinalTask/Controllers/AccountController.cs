using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLL;
using Entities;

namespace FinalTask.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult ProfilePage(Models.UserModel user)
        {
            User u = Logic.GetUserByName(user.Name);
            return View(new Models.UserModel(u, Logic.GetFileID(u.Name)));
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("~/");
        }

        [ChildActionOnly]
        public bool ContainUser(IEnumerable<User> userlist, string username)
        {
            foreach (User u in userlist)
                if (u.Name.Equals(username))
                    return true;
            return false;
        }

        public ActionResult Storage(int id)
        {
            Models.FolderModel folder = new Models.FolderModel()
            {
                CurrentFolder = Logic.GetFileById(id),
                RootID = Logic.GetParentId(id),
            };
            List<Models.FileModel> subfolders = new List<Models.FileModel>();
            List<Models.FileModel> files = new List<Models.FileModel>();
            foreach(FileEntity f in Logic.GetSubfolders(id))
                subfolders.Add(new Models.FileModel(f));

            foreach (FileEntity f in Logic.GetFiles(id))
                files.Add(new Models.FileModel(f));

            for (int i = 0; i < files.Count; i++)
            {
                files[i].AccessedUsers = new List<User>(Logic.GetAllowedUsers(files[i].ID));
                files[i].HasAccess = files[i].Access == AccessType.Public ||
                    files[i].Access == AccessType.Private &&
                    files[i].OwnerName.Equals(User.Identity.Name) ||
                    files[i].Access == AccessType.Limited &&
                    ContainUser(files[i].AccessedUsers, User.Identity.Name);

            }

            folder.SubFolders = subfolders;
            folder.Files = files;

            folder.HasAccess = folder.CurrentFolder.Access == AccessType.Public ||
                folder.CurrentFolder.Access == AccessType.Private && 
                folder.CurrentFolder.Owner.Name.Equals(User.Identity.Name) ||
                folder.CurrentFolder.Access == AccessType.Limited &&
                ContainUser(Logic.GetAllowedUsers(id), User.Identity.Name);
            return View(folder);
        }

        public ActionResult ChangePassword(string name)
        {
            if(User.Identity.Name.Equals(name) || User.IsInRole("Admin"))
                return View(new Models.ChangePasswordModel() { UserName = name });
            return Redirect("~/");
        }

        [HttpPost]
        public ActionResult ChangePassword(Models.ChangePasswordModel ChangedPass)
        {
            if (ModelState.IsValid)
            {
                if (Logic.Auth(ChangedPass.UserName, ChangedPass.OldPass))
                {
                    Logic.ChangePasswordForUser(ChangedPass.UserName, ChangedPass.NewPass);
                    return Redirect("~/Account/ProfilePage?Name=" + ChangedPass.UserName);
                }
                ModelState.AddModelError("OldPass", "Неверный пароль.");
            }

            return View(ChangedPass);         
        }

        public ActionResult ChangeEmail(string name)
        {
            if (User.Identity.Name.Equals(name) || User.IsInRole("Admin"))
                return View(new Models.ChangeEmailModel() { UserName = name });
            return Redirect("~/");
        }

        [HttpPost]
        public ActionResult ChangeEmail(Models.ChangeEmailModel ChangedEmail)
        {
            if (ModelState.IsValid)
            {
                if (Logic.CheckEmailForUser(ChangedEmail.UserName, ChangedEmail.OldEmail))
                {
                    Logic.ChangeEmailForUser(ChangedEmail.UserName, ChangedEmail.NewEmail);
                    return Redirect("~/Account/ProfilePage?Name=" + ChangedEmail.UserName);
                }
                ModelState.AddModelError("OldEmail", "Неверный пароль.");
            }

            return View(ChangedEmail);
        }

        public ActionResult RemoveUser(string name)
        {
            if (User.IsInRole("Admin"))
            {
                Logic.RemoveUser(name);
                return Redirect("~/Home/UserList");
            }
            return Redirect("~/");

        }
    }
}