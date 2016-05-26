using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;

namespace FinalTask.Models
{
    public class FolderModel
    {
        public int RootID { get; set; }
        public bool HasAccess { get; set; }
        public FileEntity CurrentFolder { get; set; }
        public IEnumerable<FileModel> SubFolders { get; set; }
        public IEnumerable<FileModel> Files { get; set; }
    }
}