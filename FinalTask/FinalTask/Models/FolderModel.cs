using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;

namespace FinalTask.Models
{
    public class FolderModel
    {
        public string OwnerName { get; set; }
        public string Name { get; set; }
        public IEnumerable<FileEntity> SubFolders { get; set; }
        public IEnumerable<FileEntity> Files { get; set; }
    }
}