using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Entities;

namespace FinalTask.Controllers
{
    public class FileController : Controller
    {
        // GET: File
        public ActionResult Upload(int parid)
        {
            return View(new Models.UploadedFileModel() { ParentId = parid });
        }

        [HttpPost]
        public ActionResult Upload(Models.UploadedFileModel fileinfo)
        {
            if (ModelState.IsValid)
            {
     
                FileEntity ParentFolder = Logic.GetFileById(fileinfo.ParentId);
                string Extension = "";
                if (fileinfo.UploadedFile.FileName.Contains('.'))
                    Extension = fileinfo.UploadedFile.FileName.Substring(fileinfo.UploadedFile.FileName.LastIndexOf('.'));
                FileEntity NewFile = new FileEntity(fileinfo.UploadedFile.FileName,
                    ParentFolder.Owner, Extension,
                    fileinfo.UploadedFile.ContentLength, DateTime.Now, 0,
                    ParentFolder.FullName + '\\' + fileinfo.UploadedFile.FileName, AccessType.Private, 
                    fileinfo.UploadedFile.ContentType);
                try
                {
                    Logic.UploadFile(ParentFolder.Id, NewFile);
                    fileinfo.UploadedFile.SaveAs(Logic.GetStorageLocation() + '\\' + ParentFolder.FullName + '\\' 
                        + fileinfo.UploadedFile.FileName);
                    return Redirect("~/Account/Storage?id=" + ParentFolder.Id);
                }
                catch (ArgumentException e)
                {
                    ModelState.AddModelError(e.ParamName, e.Message);
                }
            }

            return View(fileinfo);
        }

        public ActionResult NewFolder(int parid)
        {
            return View(new Models.NewFolderModel() { ParentId = parid });
        }

        [HttpPost]
        public ActionResult NewFolder(Models.NewFolderModel newfolder)
        {
            if (ModelState.IsValid) 
            {
                FileEntity ParentFolder = Logic.GetFileById(newfolder.ParentId);
                try
                {
                    Logic.CreateSubfolder(ParentFolder, newfolder.Name);
                    return Redirect("~/Account/Storage?id=" + ParentFolder.Id);
                }
                catch (ArgumentException e)
                {
                    ModelState.AddModelError(e.ParamName, e.Message);
                }
            }

            return View(newfolder);
        }

        public ActionResult FolderSettings(int id)
        {
            Models.FileModel file = new Models.FileModel(Logic.GetFileById(id));
            file.AccessedUsers = new List<User>(Logic.GetAllowedUsers(file.ID));
            if (User.Identity.Name.Equals(file.OwnerName) || User.IsInRole("Admin"))
                return View(file);
            else
                return Redirect("~/");
        }

        public ActionResult Settings(int id)
        {
            Models.FileModel file = new Models.FileModel(Logic.GetFileById(id));
            file.AccessedUsers = new List<User>(Logic.GetAllowedUsers(file.ID));
            if (User.Identity.Name.Equals(file.OwnerName) || User.IsInRole("Admin"))
                return View(file);
            else
                return Redirect("~/");
        }

        public ActionResult GrantFolderAccess(int id, string users)
        {
            string[] usernames = users.Split(',');
            List<User> Users = new List<User>();
            foreach(string s in usernames)
            {
                User user = Logic.GetUserByName(s.Trim());
                if (user != null)
                    Users.Add(user);
            }
            foreach (User u in Users)
                Logic.GrantAccess(u.ID, id);
            return Redirect("~/File/FolderSettings?id=" + id);
        }

        public ActionResult GrantFileAccess(int id, string users)
        {
            string[] usernames = users.Split(',');
            List<User> Users = new List<User>();
            foreach (string s in usernames)
            {
                User user = Logic.GetUserByName(s.Trim());
                if (user != null)
                    Users.Add(user);
            }
            foreach (User u in Users)
                Logic.GrantAccess(u.ID, id);
            return Redirect("~/File/Settings?id=" + id);
        }

        public ActionResult ChangeAccess(int id, string acc)
        {
            int access = 0;
            switch(acc)
            {
                case "Личный":
                    access = 0;
                    break;
                case "Публичный":
                    access = 1;
                    break;
                case "Ограниченный":
                    access = 2;
                    break;
            }
            Logic.ChangeAccess(Logic.GetFileById(id), access);
            return Redirect("~/File/FolderSettings?id=" + id);
        }

        public ActionResult ChangeFileAccess(int id, string acc)
        {
            int access = 0;
            switch (acc)
            {
                case "Личный":
                    access = 0;
                    break;
                case "Публичный":
                    access = 1;
                    break;
                case "Ограниченный":
                    access = 2;
                    break;
            }
            Logic.ChangeAccess(Logic.GetFileById(id), access);
            return Redirect("~/File/Settings?id=" + id);
        }

        public ActionResult RemoveAccess(int FileID, int UserID)
        {
            Logic.RemoveAccess(UserID, FileID);
            return Redirect("~/File/FolderSettings?id=" + FileID);
        }

        public ActionResult RemoveFileAccess(int FileID, int UserID)
        {
            Logic.RemoveAccess(UserID, FileID);
            return Redirect("~/File/Settings?id=" + FileID);
        }

        [ChildActionOnly]
        public bool ContainUser(IEnumerable<User> userlist, string username)
        {
            foreach (User u in userlist)
                if (u.Name.Equals(username))
                    return true;
            return false;
        }

        public ActionResult Download(int id)
        {
            FileEntity file = Logic.GetFileById(id);
            if (file == null || file.Extension == "folder")
                return View(-1);
            if (User.Identity.Name.Equals(file.Owner.Name) || file.Access == AccessType.Public || User.IsInRole("Admin"))
            {
                Logic.Download(id);
                return File(Logic.GetStorageLocation() + '\\' + file.FullName, file.ContentType, file.Name);
            }
            if (file.Access == AccessType.Private)
                return View(Logic.GetParentId(id));
            if (ContainUser(Logic.GetAllowedUsers(id), User.Identity.Name))
            {
                Logic.Download(id);
                return File(Logic.GetStorageLocation() + '\\' + file.FullName, file.ContentType, file.Name);
            }
            return View(Logic.GetParentId(id));
        }

        public ActionResult Delete(int id)
        {
            FileEntity file = Logic.GetFileById(id);
            int rootid = Logic.GetParentId(id);
            if(file.Owner.Name.Equals(User.Identity.Name) || User.IsInRole("Admin") || rootid != -1)
            {
                Logic.RemoveFile(file);
                return Redirect("~/Account/Storage/?id=" + rootid);
            }

            return Redirect("~/");
        }
    }
}