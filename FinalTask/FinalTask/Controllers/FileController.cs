using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;

namespace FinalTask.Controllers
{
    public class FileController : Controller
    {
        // GET: File
        public ActionResult Upload(string ownerName, string savePath)
        {
            return View(new Models.UploadedFileModel(ownerName, savePath));
        }

        [HttpPost]
        public ActionResult Upload(Models.UploadedFileModel fileinfo)
        {
            if (ModelState.IsValid)
            {
            }

            return View(fileinfo);
        }

        public ActionResult NewFolder(string ownerName, string savePath)
        {
            return View(new Models.NewFolderModel(ownerName, savePath));
        }

        [HttpPost]
        public ActionResult NewFolder(Models.NewFolderModel newfolder)
        {
            if (ModelState.IsValid) 
            {
                try
                {
                    Logic.CreateSubfolder(newfolder.OwnerName, newfolder.SavePath, newfolder.Name);
                    return Redirect("~/Account/Storage?Name=" + newfolder.SavePath + "&OwnerName=" + newfolder.OwnerName);
                }
                catch (ArgumentException e)
                {
                    ModelState.AddModelError(e.ParamName, e.Message);
                }
            }

            return View(newfolder);
        }
    }
}