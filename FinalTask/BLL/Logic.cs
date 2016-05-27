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
        public static FDAL Storage = new FDAL(@"C:\MegaFileStorage");

        public static bool RegisterUser(User user)
        {
            bool b = Data.AddUser(user);
            if (b)
                Storage.CreateFolder(new FileEntity() { Name = user.Name }, "root");
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
        
        public static IEnumerable<FileEntity> GetSubfolders(int id)
        {
            return Data.GetChildren(id, true);
        }
        
        public static IEnumerable<FileEntity> GetFiles(int id)
        {
            return Data.GetChildren(id, false);
        }

        public static bool CreateSubfolder(FileEntity root, string name)
        {
            Storage.CreateFolder(root, name);
            return Data.CreateSubFolder(root, name);
        }

        public static FileEntity GetFileById(int id)
        {
            return Data.GetFileById(id);
        }

        public static IEnumerable<User> GetAllowedUsers(int id)
        {
            return Data.GetAllowedUsers(Data.GetFileById(id));
        }

        public static int GetFileID(string fullname)
        {
            return Data.GetFileId(fullname);
        }

        public static FileEntity GetFileByFullName(string name)
        {
            return Data.GetFileByFullName(name);
        }

        public static int GetParentId(int id)
        {
            return Data.GetParentId(id);
        }

        public static bool UploadFile(int rootid, FileEntity newfile)
        {
            Data.AddFile(rootid, newfile);
            Data.ChangeDirSize(rootid, (int)newfile.Size);
            return false;
        }

        public static string GetStorageLocation()
        {
            return Storage.GetPath();
        }

        public static bool ChangeAccess(FileEntity file, int access)
        {
            return Data.ChangeAccess(file, (AccessType)access);
        }

        public static bool GrantAccess(int userid, int fileid)
        {
            return Data.GrantAccess(userid, fileid);
        }

        public static bool RemoveAccess(int userid, int fileid)
        {
            return Data.RemoveAccess(userid, fileid);
        }

        public static int GetUserType(string username)
        {
            return Data.GetUserType(username);
        }

        public static IEnumerable<FileEntity> GetAllFiles()
        {
            return Data.GetAllFiles();
        }

        public static bool ChangePasswordForUser(string username, string newpass)
        {
            return Data.ChangePasswordForUser(username, newpass);
        }

        public static bool CheckEmailForUser(string username, string email)
        {
            return Data.CheckEmailForUser(username, email);
        }

        public static bool ChangeEmailForUser(string username, string newemail)
        {
            return Data.ChangeEmailForUser(username, newemail);
        }
        
        public static void RemoveFile(FileEntity file)
        {
            int rootid = GetParentId(file.Id);
            if (file.Extension == "folder")
            {
                foreach (FileEntity f in GetFiles(file.Id))
                    RemoveFile(f);
                foreach (FileEntity f in GetSubfolders(file.Id))
                    RemoveFile(f);
            }
            Data.RemoveFile(file.Id);
            if (rootid != -1)
                Data.ChangeDirSize(rootid, -(int)file.Size);
            Storage.RemoveFile(file);
            return;
        }

        public static IEnumerable<User> GetAllUsers()
        {
            return Data.GetAllUsers();
        }

        public static bool Download(int id)
        {
            return Data.Download(id);
        }

        public static bool RemoveUser(string UserName)
        {
            RemoveFile(GetFileByFullName(UserName));
            return Data.RemoveUser(UserName);
        }
    }
}
