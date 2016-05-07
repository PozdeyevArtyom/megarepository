using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FinalTaskDAL;
using Entities;

namespace BLL
{
    public class Logic
    {
        public static IDAL Data = new DBDAL(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=FinalTaskDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public static FDAL Storage = new FDAL(@"C:\MegaFileStorage", Data.GetAllUsers());

        public static bool RegisterUser(User user)
        {
            bool b = Data.AddUser(user);
            if (b)
                Data.CreateSubFolder(Storage.CreateFolder(user.Name, "root"), user);
            return b;
        }

        public static bool Auth(string name, string pass)
        {
            StringBuilder safename = new StringBuilder();
            StringBuilder safepass = new StringBuilder();
            int i;
            for(i = 0; i < name.Length; i++)
            {
                safename.Append(name[i]);
                if (name[i] == '\'')
                    safename.Append('\'');
            }
            for (i = 0; i < pass.Length; i++) 
            {
                safepass.Append(pass[i]);
                if (pass[i] == '\'')
                    safepass.Append('\'');
            }

            return Data.Auth(safename.ToString(), safepass.ToString());
        }

        public static User GetUserByName(string name)
        {
            return Data.GetUserByName(name);
        }

        public static IEnumerable<FileEntity> GetSubfolders(string foldername, string ownerName)
        {
            IEnumerable<string> foldernames = Storage.GetSubfolders(foldername);
            List<FileEntity> folders = new List<FileEntity>();
            foreach (string s in foldernames)
                folders.Add(new FileEntity {
                    Name = s.Substring(s.LastIndexOf('\\') + 1),
                    FullName = s,
                    Extension = "folder",
                    Owner = new User { Name = ownerName }
                });
            return folders;
        }

        public static IEnumerable<FileEntity> GetFiles(string foldername, string ownerName)
        {
            IEnumerable<FileInfo> filenames = Storage.GetFiles(foldername);
            List<FileEntity> files = new List<FileEntity>();
            foreach (FileInfo f in filenames)
            {
                FileEntity fe = new FileEntity(f);
                fe.Owner = new User { Name = ownerName };
                files.Add(fe);

            }
            return files;
        }

        public static bool CreateSubfolder(string ownerName, string root, string name)
        {
            User user = GetUserByName(ownerName);
            return Data.CreateSubFolder(Storage.CreateFolder(root, name), user);
        }
    }
}
