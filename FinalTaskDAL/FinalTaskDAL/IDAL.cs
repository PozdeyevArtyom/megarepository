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
        bool AddFile(FileEntity file);
        bool Auth(string name, string pass);
        bool CreateSubFolder(string name, User user);
        IEnumerable<User> GetAllUsers();
        IEnumerable<FileEntity> GetAllFiles();
        IEnumerable<FileEntity> GetAllFilesOwnedByUser(User user);
        IEnumerable<User> GetAllowedUsers(FileEntity file);
        IEnumerable<FileEntity> GetAccessedFilesForUser(User user);
        FileEntity GetFileById(int id);
        FileEntity GetFileByNameOwnedByUser(string filename, string username);
        User GetUserById(int id);
        User GetUserByName(string name);
        bool RemoveUser(string username);
        bool RemoveFile(string filename);
    }
}
