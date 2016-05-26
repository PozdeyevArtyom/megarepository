using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;

namespace FinalTask.Models
{
    public class FileModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public string Extension { get; set; }
        public string FullName { get; set; }
        public DateTime UploadDate { get; set; }
        public AccessType Access { get; set; }
        public List<User> AccessedUsers { get; set; }
        public bool HasAccess { get; set; }
        public long Size { get; set; }
        public int Downloads { get; set; }

        public FileModel() { }

        public FileModel(FileEntity file)
        {
            ID = file.Id;
            Name = file.Name;
            OwnerName = file.Owner.Name;
            Extension = file.Extension;
            FullName = file.FullName;
            UploadDate = file.UploadDate;
            Access = file.Access;
            Size = file.Size;
            Downloads = file.Downloads;
        }
    }
}