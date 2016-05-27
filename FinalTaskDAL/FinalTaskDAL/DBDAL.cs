using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace FinalTaskDAL
{
    public class DBDAL : IDAL
    {
        public string ConnectionString;

        public DBDAL(string connectionstring)
        {
            ConnectionString = connectionstring;
        }

        /// <summary>
        /// Метод AddFile добавляет информацию о файле в базу данных
        /// </summary>
        /// <param name="parid"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool AddFile(int parid, FileEntity file)
        {
            bool r;
            if (GetFileByFullName(file.FullName) != null)
                throw new ArgumentException("Файл с таким именем уже существует.", "UploadedFile");
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO MegaFileStorage.Files (OwnerID, FileName, Extension, "
                    + "Size, UploadDate, Downloads, FullName, AccessType, ContentType) VALUES(@oid, @n, @e, @s, @ud, "
                    + "@d, @fn, @at, @ct)");
                command.Connection = connection;
                command.Parameters.AddWithValue("@oid", file.Owner.ID);
                command.Parameters.AddWithValue("@n", file.Name);
                command.Parameters.AddWithValue("@e", file.Extension);
                command.Parameters.AddWithValue("@s", file.Size);
                command.Parameters.AddWithValue("@ud", file.UploadDate);
                command.Parameters.AddWithValue("@d", file.Downloads);
                command.Parameters.AddWithValue("@fn", file.FullName);
                command.Parameters.AddWithValue("@at", file.Access);
                command.Parameters.AddWithValue("@ct", file.ContentType);

                connection.Open();

                r = command.ExecuteNonQuery() == 1;
                if (!r) return r;
            }

            int chid = GetFileByFullName(file.FullName).Id;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO MegaFileStorage.Folders (ChID, ParID) VALUES(@c, @p)");
                command.Connection = connection;
                command.Parameters.AddWithValue("@c", chid);
                command.Parameters.AddWithValue("@p", parid);

                connection.Open();

                return command.ExecuteNonQuery() == 1;
            }
        }

        /// <summary>
        /// Метод AddUser создаёт нового пользователя в базе данных
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool AddUser(User user)
        {
            if (CheckName(user.Name))
                throw new ArgumentException("Такой логин уже занят.", "Name");
            if (CheckEmail(user.Email))
                throw new ArgumentException("Пользователь с таким e-mail уже зарегистрирован.", "Email");

            bool result = true;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO MegaFileStorage.Users (UserName, Pass, RegistrationDate, "
                    + "UserType, Email) VALUES(@un, @p, @rd, @ut, @e)");
                command.Connection = connection;
                command.Parameters.AddWithValue("@un", user.Name);
                command.Parameters.AddWithValue("@p", user.Password);
                command.Parameters.AddWithValue("@rd", user.RegDate);
                command.Parameters.AddWithValue("@ut", user.Type);
                command.Parameters.AddWithValue("@e", user.Email);

                connection.Open();

                result &= command.ExecuteNonQuery() == 1;
            }

            user = GetUserByName(user.Name);

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO MegaFileStorage.Files (OwnerID, FileName, Extension,"
                    + " UploadDate, FullName, AccessType, Size) VALUES(@oid, @n, @e, @ud, @fn, @at, @s)");
                command.Connection = connection;
                command.Parameters.AddWithValue("@oid", user.ID);
                command.Parameters.AddWithValue("@n", user.Name);
                command.Parameters.AddWithValue("@e", "folder");
                command.Parameters.AddWithValue("@ud", DateTime.Now);
                command.Parameters.AddWithValue("@fn", user.Name);
                command.Parameters.AddWithValue("@at", 0);
                command.Parameters.AddWithValue("@s", 0);

                connection.Open();

                result &= command.ExecuteNonQuery() == 1;
            }

            result &= CreateSubFolder(GetFileByFullName(user.Name), "root");
            return result;
        }

        /// <summary>
        /// Метод GetAccessedFilesForUser возвращает все файлы, доступ к которым имеется у пользователя user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<FileEntity> GetAccessedFilesForUser(User user)
        {
            List<FileEntity> files = new List<FileEntity>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT FileID FROM MegaFileStorage.Access WHERE UserID = @uid");
                command.Connection = connection;
                command.Parameters.AddWithValue("@uid", user.ID);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                        while (reader.Read())
                            files.Add(GetFileById(reader.GetInt32(0)));
                }
            }
            return files;
        }

        /// <summary>
        /// Метод GetAllFiles возвращает все файлы, имеющиеся в базе данных
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FileEntity> GetAllFiles()
        {
            List<FileEntity> files = new List<FileEntity>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT FileID, OwnerID, [FileName], Extension, Size, UploadDate, "
                    + "Downloads, FullName, AccessType, ContentType FROM MegaFileStorage.Files");
                command.Connection = connection;

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        FileEntity file = new FileEntity();
                        file.Id = reader.GetInt32(0);
                        file.Owner = new User();
                        file.Owner.ID = reader.GetInt32(1);
                        file.Name = reader.GetString(2).Trim();
                        file.Extension = reader.GetString(3).Trim();
                        if (!reader.IsDBNull(4))
                            file.Size = reader.GetInt32(4);
                        file.UploadDate = reader.GetDateTime(5);
                        if (!reader.IsDBNull(6))
                            file.Downloads = reader.GetInt32(6);
                        file.FullName = reader.GetString(7).Trim();
                        switch (reader.GetInt32(8))
                        {
                            case 0:
                                file.Access = AccessType.Private;
                                break;
                            case 1:
                                file.Access = AccessType.Public;
                                break;
                            default:
                                file.Access = AccessType.Limited;
                                break;
                        }
                        if (!reader.IsDBNull(9))
                            file.ContentType = reader.GetString(9);

                        files.Add(file);
                    }
                }

                for(int i = 0; i < files.Count; i++)
                    files[i].Owner = GetUserById(files[i].Owner.ID);
            }
            return files;
        }

        /// <summary>
        /// Метод GetAllFilesOwnedByUser возвращает все файлы, которые загрузил пользователь user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<FileEntity> GetAllFilesOwnedByUser(User user)
        {
            List<FileEntity> files = new List<FileEntity>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT FileID, [FileName], Extension, Size, UploadDate, "
                    + "Downloads, FullName, AccessType, ContentType WHERE OwnerID = @oid");
                command.Connection = connection;
                command.Parameters.AddWithValue("@oid", user.ID);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FileEntity file = new FileEntity();
                        file.Id = reader.GetInt32(0);
                        file.Owner = user;
                        file.Name = reader.GetString(1).Trim();
                        file.Extension = reader.GetString(2).Trim();
                        if(!reader.IsDBNull(3))
                            file.Size = reader.GetInt32(3);
                        file.UploadDate = reader.GetDateTime(4);
                        if (!reader.IsDBNull(5))
                            file.Downloads = reader.GetInt32(5);
                        file.FullName = reader.GetString(6).Trim();
                        switch (reader.GetInt32(7))
                        {
                            case 0:
                                file.Access = AccessType.Private;
                                break;
                            case 1:
                                file.Access = AccessType.Public;
                                break;
                            default:
                                file.Access = AccessType.Limited;
                                break;
                        }
                        if (!reader.IsDBNull(8))
                            file.ContentType = reader.GetString(8);
                    }
                }

                for (int i = 0; i < files.Count; i++)
                    files[i].Owner = GetUserById(files[i].Owner.ID);
            }
            return files;
        }

        /// <summary>
        /// Метод GetAllowedUsers возвращает всех пользователей, которые имеют доступ к файлу file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public IEnumerable<User> GetAllowedUsers(FileEntity file)
        {
            List<User> users = new List<User>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT UserID FROM MegaFileStorage.Access WHERE FileID = @fid");
                command.Connection = connection;
                command.Parameters.AddWithValue("@fid", file.Id);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                        while (reader.Read())
                            users.Add(GetUserById(reader.GetInt32(0)));
                }
            }
            return users;
        }

        /// <summary>
        /// Метод GetAllUsers возвращает всех зарегистрированных пользователей
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT UserID, UserName, Pass, RegistrationDate, UserType, Email FROM "
                    + "MegaFileStorage.Users");
                command.Connection = connection;
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User user = new User();
                        user.ID = reader.GetInt32(0);
                        user.Name = reader.GetString(1).Trim();
                        user.Password = reader.GetString(2).Trim();
                        user.RegDate = reader.GetDateTime(3);
                        switch(reader.GetInt32(4))
                        {
                            case 0:
                                user.Type = UserType.Administrator;
                                break;
                            default:
                                user.Type = UserType.User;
                                break;
                        }
                        user.Email = reader.GetString(5).Trim();
                        users.Add(user);
                    }
                }
            }
            return users;
        }

        /// <summary>
        /// Метод GetFileById возвращает файл, соответствующий идентификатору id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FileEntity GetFileById(int id)
        {
            FileEntity file = null;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT FileID, OwnerID, [FileName], Extension, Size, UploadDate, "
                    + "Downloads, FullName, AccessType, ContentType FROM MegaFileStorage.Files WHERE FileID = @fid");
                command.Connection = connection;
                command.Parameters.AddWithValue("@fid", id);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        file = new FileEntity();
                        file.Id = reader.GetInt32(0);
                        file.Owner = new User();
                        file.Owner.ID = reader.GetInt32(1);
                        file.Name = reader.GetString(2).Trim();
                        file.Extension = reader.GetString(3).Trim();
                        if (!reader.IsDBNull(4))
                            file.Size = reader.GetInt32(4);
                        file.UploadDate = reader.GetDateTime(5);
                        if (!reader.IsDBNull(6))
                            file.Downloads = reader.GetInt32(6);
                        file.FullName = reader.GetString(7).Trim();
                        switch (reader.GetInt32(8))
                        {
                            case 0:
                                file.Access = AccessType.Private;
                                break;
                            case 1:
                                file.Access = AccessType.Public;
                                break;
                            default:
                                file.Access = AccessType.Limited;
                                break;
                        }
                        if (!reader.IsDBNull(9))
                            file.ContentType = reader.GetString(9);
                    }
                }

                if (file != null)
                    file.Owner = GetUserById(file.Owner.ID);
            }

            return file;
        }

        /// <summary>
        /// Метод GetUserById возвращает пользователя, соответствующего идентификатору id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUserById(int id)
        {
            User user = null;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT UserID, UserName, Pass, RegistrationDate, UserType, Email FROM "
                    + "MegaFileStorage.Users WHERE UserID = @uid");
                command.Connection = connection;
                command.Parameters.AddWithValue("@uid", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user = new User();
                        user.ID = reader.GetInt32(0);
                        user.Name = reader.GetString(1).Trim();
                        user.Password = reader.GetString(2);
                        user.RegDate = reader.GetDateTime(3);
                        switch (reader.GetInt32(4))
                        {
                            case 0:
                                user.Type = UserType.Administrator;
                                break;
                            default:
                                user.Type = UserType.User;
                                break;
                        }
                        user.Email = reader.GetString(5);
                    }
                }
            }
            return user;
        }

        /// <summary>
        /// Метод GetUserByName возвращает пользователя с именем name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public User GetUserByName(string name)
        {
            User user = null;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT UserID, UserName, Pass, RegistrationDate, UserType, Email FROM "
                    + "MegaFileStorage.Users WHERE UserName = @un");
                command.Connection = connection;
                command.Parameters.AddWithValue("@un", name);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user = new User();
                        user.ID = reader.GetInt32(0);
                        user.Name = reader.GetString(1).Trim();
                        user.Password = reader.GetString(2);
                        user.RegDate = reader.GetDateTime(3);
                        switch (reader.GetInt32(4))
                        {
                            case 0:
                                user.Type = UserType.Administrator;
                                break;
                            default:
                                user.Type = UserType.User;
                                break;
                        }
                        user.Email = reader.GetString(5);
                    }
                }
            }
            return user;
        }

        /// <summary>
        /// Метод RemoveUser удаляет пользователя с именем name
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool RemoveUser(string username)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("DELETE FROM MegaFileStorage.Users WHERE UserName = @un");
                command.Connection = connection;
                command.Parameters.AddWithValue("@un", username);

                connection.Open();

                int id = GetUserByName(username).ID;

                if (command.ExecuteNonQuery() != 1) return false;

                command.CommandText = "DELETE FROM MegaFileStorage.Access WHERE UserID = @uid";
                command.Parameters.AddWithValue("@uid", id);

                return command.ExecuteNonQuery() == 1;
            }
        }

        /// <summary>
        /// Метод CheckName проверяет существует ли пользователя с именем name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool CheckName(string name)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT UserID, UserName, Pass, RegistrationDate, UserType, Email FROM "
                    + "MegaFileStorage.Users WHERE UserName = @un");
                command.Connection = connection;
                command.Parameters.AddWithValue("@un", name);
                connection.Open();

                return command.ExecuteNonQuery() == 1;
            }
        }

        /// <summary>
        /// Метод CheckEmail проверяет существует ли пользователь с электронной почтой email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool CheckEmail(string email)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT UserID, UserName, Pass, RegistrationDate, UserType, Email FROM "
                    + "MegaFileStorage.Users WHERE Email = @e");
                command.Connection = connection;
                command.Parameters.AddWithValue("@e", email);
                connection.Open();

                return command.ExecuteNonQuery() == 1;
            }
        }

        /// <summary>
        /// Метод Auth проверяет комбинацию логина и пароля
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public bool Auth(string name, string pass)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT UserName FROM MegaFileStorage.Users WHERE UserName = @un "
                    +"AND Pass = @pw");
                command.Connection = connection;
                command.Parameters.AddWithValue("@un", name);
                command.Parameters.AddWithValue("@pw", pass);

                connection.Open();

                using (SqlDataReader r = command.ExecuteReader())
                    return r.HasRows;
            }
        }

        /// <summary>
        /// Метод GetFileByFullName возвращает файл, полное имя которого соотвествует filename
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public FileEntity GetFileByFullName(string filename)
        {
            FileEntity file = null;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT FileID, OwnerID, [FileName], Extension, Size, UploadDate, "
                    + "Downloads, FullName, AccessType FROM MegaFileStorage.Files WHERE FullName = @fn");
                command.Connection = connection;
                command.Parameters.AddWithValue("@fn", filename);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        file = new FileEntity();
                        file.Id = reader.GetInt32(0);
                        file.Owner = new User { ID = reader.GetInt32(1) };
                        file.Name = reader.GetString(2).Trim();
                        file.Extension = reader.GetString(3).Trim();
                        if (!reader.IsDBNull(4))
                            file.Size = reader.GetInt32(4);
                        file.UploadDate = reader.GetDateTime(5);
                        if (!reader.IsDBNull(6))
                            file.Downloads = reader.GetInt32(6);
                        file.FullName = reader.GetString(7).Trim();
                        switch (reader.GetInt32(8))
                        {
                            case 0:
                                file.Access = AccessType.Private;
                                break;
                            case 1:
                                file.Access = AccessType.Public;
                                break;
                            default:
                                file.Access = AccessType.Limited;
                                break;
                        }
                    }
                }
            }
            return file;
        }

        /// <summary>
        /// Метод CreateSubFolder добавляет в базу данных информацию о подкаталоге каталога root с именем name
        /// </summary>
        /// <param name="root"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool CreateSubFolder(FileEntity root, string name)
        {
            if (GetFileId(root.FullName + '\\' + name) != -1)
                throw new ArgumentException("Существующее имя каталога.", "Name");
            bool r = true;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO MegaFileStorage.Files (OwnerID, FileName, Extension,"
                    + " UploadDate, FullName, AccessType, Size) VALUES(@oid, @n, @e, @ud, @fn, @at, @s)");
                command.Connection = connection;
                command.Parameters.AddWithValue("@oid", root.Owner.ID);
                command.Parameters.AddWithValue("@n", name);
                command.Parameters.AddWithValue("@e", "folder");
                command.Parameters.AddWithValue("@ud", DateTime.Now);
                command.Parameters.AddWithValue("@fn", root.FullName + '\\' + name);
                command.Parameters.AddWithValue("@at", 0);
                command.Parameters.AddWithValue("@s", 0);

                connection.Open();

                r &= command.ExecuteNonQuery() == 1;
            }

            if (!r) return r;

            int chid = GetFileId(root.FullName + '\\' + name);

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO MegaFileStorage.Folders (ChID, ParID) VALUES(@cd, @pd)");
                command.Connection = connection;
                command.Parameters.AddWithValue("@cd", chid);
                command.Parameters.AddWithValue("@pd", root.Id);

                connection.Open();

                r &= command.ExecuteNonQuery() == 1;
            }

            return r;
        }

        /// <summary>
        /// Метод GetFileId возвращает идентификатор файла, полное имя которого соответствует fullname
        /// </summary>
        /// <param name="fullname"></param>
        /// <returns></returns>
        public int GetFileId(string fullname)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand resultcommand = new SqlCommand("SELECT FileID FROM MegaFileStorage.Files WHERE FullName = @fn");
                resultcommand.Connection = connection;
                resultcommand.Parameters.AddWithValue("@fn", fullname);

                connection.Open();

                using (SqlDataReader reader = resultcommand.ExecuteReader())
                    if (reader.Read())
                        return reader.GetInt32(0);
                    else
                        return -1;
            }
        }

        /// <summary>
        /// Если параметр f - истина, то метод GetChildren возвращает все подкаталоги каталога с идентификатором id
        /// В противном случае вернём все содержащиеся в нём файлы
        /// </summary>
        /// <param name="id"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public IEnumerable<FileEntity> GetChildren(int id, bool f)
        {
            List<int> ids = new List<int>();
            List<FileEntity> files = new List<FileEntity>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT ChID FROM MegaFileStorage.Folders WHERE ParID=@id");
                command.Connection = connection;
                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        ids.Add(reader.GetInt32(0));
                }

                foreach(int i in ids)
                {
                    if(f)
                    {
                        command.CommandText = "SELECT OwnerID, FileName, UploadDate, FullName, AccessType, Size FROM "
                            + "MegaFileStorage.Files WHERE FileID = @fid AND Extension = 'folder'";

                        command.Parameters.AddWithValue("@fid", i);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                FileEntity folder = new FileEntity();
                                folder.Id = i;
                                folder.Owner = new User() { ID = reader.GetInt32(0) };
                                folder.Name = reader.GetString(1).Trim();
                                folder.UploadDate = reader.GetDateTime(2);
                                folder.FullName = reader.GetString(3).Trim();
                                switch (reader.GetInt32(4))
                                {
                                    case 0:
                                        folder.Access = AccessType.Private;
                                        break;
                                    case 1:
                                        folder.Access = AccessType.Public;
                                        break;
                                    default:
                                        folder.Access = AccessType.Limited;
                                        break;
                                }
                                folder.Size = reader.GetInt32(5);
                                files.Add(folder);
                            }
                        }
                    }
                    else
                    {
                        command.CommandText = "SELECT OwnerID, FileName, Extension, Size, UploadDate, Downloads, "
                            + "FullName, AccessType FROM MegaFileStorage.Files WHERE FileID = @fid AND NOT Extension "
                            + "= 'folder'";

                        command.Parameters.AddWithValue("@fid", i);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                FileEntity file = new FileEntity();
                                file.Id = i;
                                file.Owner = new User() { ID = reader.GetInt32(0) };
                                file.Name = reader.GetString(1).Trim();
                                file.Extension = reader.GetString(2).Trim();
                                file.Size = reader.GetInt32(3);
                                file.UploadDate = reader.GetDateTime(4);
                                file.Downloads = reader.GetInt32(5);
                                file.FullName = reader.GetString(6).Trim();
                                switch (reader.GetInt32(7))
                                {
                                    case 0:
                                        file.Access = AccessType.Private;
                                        break;
                                    case 1:
                                        file.Access = AccessType.Public;
                                        break;
                                    default:
                                        file.Access = AccessType.Limited;
                                        break;
                                }
                                files.Add(file);
                            }
                        }
                    }

                    command.Parameters.Clear();
                }

                for (int i = 0; i < files.Count; i++)
                    files[i].Owner = GetUserById(files[i].Owner.ID);
            }
            return files;
        }

        /// <summary>
        /// Метод GetParentId возвращает индентификатор родительского каталога для файла с идентиыикатором id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetParentId(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT ParID FROM MegaFileStorage.Folders WHERE ChID = @id");
                command.Connection = connection;
                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                    if (reader.Read())
                        return reader.GetInt32(0);
                    else
                        return -1;
            }
        }

        /// <summary>
        /// Метод HasAccess проверяет имеет ли пользователь с идентификатором userid доступ к файлу
        /// с идентификатором fileid
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="fileid"></param>
        /// <returns></returns>
        public bool HasAccess(int userid, int fileid)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM MegaFileStorage.Access WHERE UserID = @uid AND "
                    + "FileID = @fid");
                command.Connection = connection;
                command.Parameters.AddWithValue("@uid", userid);
                command.Parameters.AddWithValue("@fid", fileid);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                    return reader.HasRows;
            }
        }

        /// <summary>
        /// Метод ChangeAccess изменяет тип доступа файла file на accesstype
        /// </summary>
        /// <param name="file"></param>
        /// <param name="accesstype"></param>
        /// <returns></returns>
        public bool ChangeAccess(FileEntity file, AccessType accesstype)
        {
            bool r;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("UPDATE MegaFileStorage.Files SET AccessType = @at WHERE "
                    + "FileID = @fid");
                command.Connection = connection;
                command.Parameters.AddWithValue("@at", (int)accesstype);
                command.Parameters.AddWithValue("@fid", file.Id);

                connection.Open();

                r = command.ExecuteNonQuery() == 1;
                if (!r) return r;
                
                switch (accesstype)
                {
                    case AccessType.Private:
                    case AccessType.Public:
                        command.CommandText = "DELETE FROM MegaFileStorage.Access WHERE FileID = @fid";

                        r = command.ExecuteNonQuery() == 1;
                        break;
                    case AccessType.Limited:
                        command.CommandText = "INSERT INTO MegaFileStorage.Access (UserID, FileID) VALUES(@uid, @fid)";
                        command.Parameters.AddWithValue("@uid", file.Owner.ID);

                        r = command.ExecuteNonQuery() == 1;
                        break;
                }
            }
            return r;
        }

        /// <summary>
        /// Метод RemoveAccess отменяет доступ к файлу с идентификатором fileid для пользователя
        /// с идентификатором userid
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="fileid"></param>
        /// <returns></returns>
        public bool RemoveAccess(int userid, int fileid)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("DELETE FROM MegaFileStorage.Access WHERE FileID = @fid AND "
                    + "UserID = @uid");
                command.Connection = connection;
                command.Parameters.AddWithValue("@fid", fileid);
                command.Parameters.AddWithValue("@uid", userid);

                connection.Open();

                return command.ExecuteNonQuery() == 1;
            }
        }

        /// <summary>
        ///  Метод GrantAccess даёт доступ к файлу с идентификатором fileid для пользователя
        /// с идентификатором userid
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="fileid"></param>
        /// <returns></returns>
        public bool GrantAccess(int userid, int fileid)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT AccessID FROM MegaFileStorage.Access WHERE FileID = @fid AND "
                    + "UserID = @uid");
                command.Connection = connection;
                command.Parameters.AddWithValue("@fid", fileid);
                command.Parameters.AddWithValue("@uid", userid);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                    if (reader.Read())
                        return false;

                command.CommandText = "INSERT INTO MegaFileStorage.Access (FileID, UserID) VALUES(@fid, @uid)";
                return command.ExecuteNonQuery() == 1;
            }
        }

        /// <summary>
        /// Метод GetUserType возвращает тип пользователя
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int GetUserType(string username)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT UserType FROM MegaFileStorage.Users WHERE UserName = @un");
                command.Connection = connection;
                command.Parameters.AddWithValue("@un", username);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                    if (reader.Read())
                        return reader.GetInt32(0);
                return -1;
            }
        }


        /// <summary>
        /// Метод ChangePasswordForUser изменяет пароль пользователя с именем username на newpass
        /// </summary>
        /// <param name="username"></param>
        /// <param name="newpass"></param>
        /// <returns></returns>
        public bool ChangePasswordForUser(string username, string newpass)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("UPDATE MegaFileStorage.Users SET Pass = @np WHERE UserName = @un");
                command.Connection = connection;
                command.Parameters.AddWithValue("@np", newpass);
                command.Parameters.AddWithValue("@un", username);

                connection.Open();

                return command.ExecuteNonQuery() == 1;
            }
        }

        /// <summary>
        /// Метод ChangeEmailForUser изменяет электронный адрес пользователя с именем username на newemail
        /// </summary>
        /// <param name="username"></param>
        /// <param name="newemail"></param>
        /// <returns></returns>
        public bool ChangeEmailForUser(string username, string newemail)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("UPDATE MegaFileStorage.Users SET Email = @e WHERE UserName = @un");
                command.Connection = connection;
                command.Parameters.AddWithValue("@e", newemail);
                command.Parameters.AddWithValue("@un", username);

                connection.Open();

                return command.ExecuteNonQuery() == 1;
            }
        }

        /// <summary>
        /// Метод CheckEmailForUser проверяет соответсвует ли электронный адрес email 
        /// электронному аресу пользователя с именем username
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool CheckEmailForUser(string username, string email)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT UserName FROM MegaFileStorage.Users WHERE UserName = @un "
                    + "AND Email = @e");
                command.Connection = connection;
                command.Parameters.AddWithValue("@un", username);
                command.Parameters.AddWithValue("@e", email);

                connection.Open();

                using (SqlDataReader r = command.ExecuteReader())
                    return r.HasRows;
            }
        }


        /// <summary>
        /// Метод RemoveFile удаляет файл с идентификатором id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RemoveFile(int id)
        {
            bool r;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("DELETE FROM MegaFileStorage.Files WHERE FileID = @fid");
                command.Connection = connection;
                command.Parameters.AddWithValue("@fid", id);

                connection.Open();

                r = command.ExecuteNonQuery() == 1;
                if (!r) return r;

                command.CommandText = "DELETE FROM MegaFileStorage.Access WHERE FileID = @fid";

                r = command.ExecuteNonQuery() == 1;
                if (!r) return r;

                command.CommandText = "DELETE FROM MegaFileStorage.Folders WHERE ChID = @fid OR ParID = @fid";

                r = command.ExecuteNonQuery() == 1;
            }
            return r;
        }

        /// <summary>
        /// Метод Download увеличивает количество скачиваний файла с идентификатором fileid на 1
        /// </summary>
        /// <param name="fileid"></param>
        /// <returns></returns>
        public bool Download(int fileid)
        {
            if (GetFileById(fileid).Extension == "folder")
                throw new ArgumentException("Неверный параметр.", "fileid");
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("UPDATE MegaFileStorage.Files SET Downloads = Downloads + 1 WHERE "
                    + "FileID = @fid");
                command.Connection = connection;
                command.Parameters.AddWithValue("@fid", fileid);

                connection.Open();

                return command.ExecuteNonQuery() == 1;
            }
        }

        /// <summary>
        /// Метод ChangeDirSize изменяет размер каталога с идентификатором dirid, а также всех
        /// родительских на amount
        /// </summary>
        /// <param name="dirid"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool ChangeDirSize(int dirid, int amount)
        {
            if (GetFileById(dirid).Extension != "folder")
                throw new ArgumentException("Неверный параметр.", "fileid");
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("UPDATE MegaFileStorage.Files SET Size = Size + @a WHERE "
                    + "FileID = @fid");
                command.Connection = connection;
                command.Parameters.AddWithValue("@fid", dirid);
                command.Parameters.AddWithValue("@a", amount);

                connection.Open();

                if (command.ExecuteNonQuery() != 1) return false;

                int rootid = GetParentId(dirid);
                while(rootid != -1)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@fid", rootid);
                    command.Parameters.AddWithValue("@a", amount);

                    if (command.ExecuteNonQuery() != 1) return false;
                    rootid = GetParentId(rootid);
                }
            }
            return true;
        }
    }
}