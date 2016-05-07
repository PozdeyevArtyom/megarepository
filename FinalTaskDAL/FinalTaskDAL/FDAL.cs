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

        public FDAL(string path, IEnumerable<User> users)
        {
            Storage = new DirectoryInfo(path);
            if (!Storage.Exists)
                Storage.Create();
            foreach(User u in users)
                Storage.CreateSubdirectory(u.Name + "\\root");
        }

        public IEnumerable<string> GetSubfolders(string name)
        {
            List<string> result = new List<string>();
            DirectoryInfo dir = new DirectoryInfo(Storage.FullName + name);
            foreach (DirectoryInfo d in dir.EnumerateDirectories())
                result.Add(name + "\\" + d.Name);
            return result;
        }

        public IEnumerable<FileInfo> GetFiles(string name)
        {
            return new DirectoryInfo(Storage.FullName + name).EnumerateFiles();
        }

        public string CreateFolder(string rootname, string foldername)
        {
            rootname = rootname.Trim('\\');
            foldername = foldername.Trim('\\');
            string s = Storage.CreateSubdirectory(rootname + '\\' + foldername).FullName;
            return s.Substring(s.IndexOf(Storage.FullName) + Storage.FullName.Length);
        }
    }
}
