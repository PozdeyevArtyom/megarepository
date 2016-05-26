using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace FinalTaskDAL
{
    public interface IDAL
    {
        bool AddUser(User user);
        bool AddFile(int parid, FileEntity file);
        bool Auth(string name, string pass);
        bool ChangeAccess(FileEntity file, AccessType access);
        bool ChangeEmailForUser(string username, string newemail);
        bool ChangePasswordForUser(string username, string newpass);
        bool CheckEmailForUser(string username, string email);
        IEnumerable<User> GetAllUsers();
        IEnumerable<FileEntity> GetAllFiles();
        IEnumerable<FileEntity> GetAllFilesOwnedByUser(User user);
        IEnumerable<User> GetAllowedUsers(FileEntity file);
        IEnumerable<FileEntity> GetAccessedFilesForUser(User user);
        IEnumerable<FileEntity> GetChildren(int id, bool f);
        FileEntity GetFileById(int id);
        FileEntity GetFileByFullName(string filename);
        int GetFileId(string fullname);
        int GetParentId(int id);
        User GetUserById(int id);
        User GetUserByName(string name);
        int GetUserType(string username);
        bool GrantAccess(int userid, int fileid);
        bool CreateSubFolder(FileEntity root, string name);
        bool HasAccess(int userid, int fileid);
        bool RemoveAccess(int userid, int fileid);
        bool RemoveFile(int id);
        bool RemoveUser(string username);
        bool Download(int fileid);
        bool ChangeDirSize(int dirid, int amount);
    }
}
