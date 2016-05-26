using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Entities
{
    public class FileEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User Owner { get; set; }
        public string Extension { get; set; }
        public long Size { get; set; } = 0;
        public DateTime UploadDate { get; set; }
        public int Downloads { get; set; } = 0;
        public string FullName { get; set; }
        public AccessType Access { get; set; } = AccessType.Private;
        public string ContentType { get; set; } = "";

        public FileEntity() { }

        public FileEntity(string name, User owner, string ext, int size, DateTime upldate, int downloads, string fullname, 
            AccessType access, string contenttype)
        {
            Name = name;
            Owner = owner;
            Extension = ext;
            Size = size;
            UploadDate = upldate;
            Downloads = downloads;
            FullName = fullname;
            Access = access;
            ContentType = contenttype;
        }

        public FileEntity(FileInfo file)
        {
            Name = file.Name;
            Extension = file.Extension;
            Size = file.Length;
            FullName = file.FullName;
            UploadDate = file.CreationTime;
        }
    }
}
