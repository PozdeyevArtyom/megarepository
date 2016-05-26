using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.IO;

namespace FinalTaskDAL
{
    public class FDAL
    {
        public DirectoryInfo Storage;

        public FDAL(string path)
        {
            Storage = new DirectoryInfo(path);
        }

        public IEnumerable<DirectoryInfo> GetSubfolders(string name)
        {
            return new DirectoryInfo(Storage.FullName + name).EnumerateDirectories();
        }

        public IEnumerable<FileInfo> GetFiles(string name)
        {
            return new DirectoryInfo(Storage.FullName + name).EnumerateFiles();
        }

        public void CreateFolder(FileEntity root, string foldername)
        {
            Storage.CreateSubdirectory(root.FullName + '\\' + foldername);
        }

        public string GetPath()
        {
            return Storage.FullName;
        }

        public void RemoveFile(FileEntity file)
        {
            if (file.Extension == "folder")
                new DirectoryInfo(Storage.FullName + '\\' + file.FullName).Delete();
            else
                new FileInfo(Storage.FullName + '\\' + file.FullName).Delete();
            return;
        }
    }
}
