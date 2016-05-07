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
        public string Storage = "Storage";

        public DBDAL(string connectionstring)
        {
            ConnectionString = connectionstring;
        }

        public DBDAL(string connectionstring, string storage)
        {
            ConnectionString = connectionstring;
            Storage = storage;
        }

        public bool AddFile(FileEntity file)
        {
            if (GetUserByName(file.Owner.Name) == null)
                throw new ArgumentException("Недопустимое значение параметра.", "UserName");
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO MegaFileStorage.Files (OwnerID, FileName, Extension, Size,"
                    + " UploadDate, Downloads, FullName, AccessType) VALUES(@oid, @n, @e, @s, @ud, @d, @fn, @at)");
                command.Connection = connection;
                command.Parameters.AddWithValue("@oid", file.Owner.ID);
                command.Parameters.AddWithValue("@n", file.Name);
                command.Parameters.AddWithValue("@e", file.Extension);
                command.Parameters.AddWithValue("@s", file.Size);
                command.Parameters.AddWithValue("@ud", file.UploadDate);
                command.Parameters.AddWithValue("@d", file.Downloads);
                command.Parameters.AddWithValue("@fn", file.Access);

                connection.Open();

                return command.ExecuteNonQuery() == 1;

            }
        }

        public bool AddUser(User user)
        {
            if (CheckName(user.Name))
                throw new ArgumentException("Такой логин уже занят.", "Name");
            if (CheckEmail(user.Email))
                throw new ArgumentException("Пользователь с таким e-mail уже зарегистрирован.", "Email");
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

                return command.ExecuteNonQuery() == 1;
            }
        }

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

        public IEnumerable<FileEntity> GetAllFiles()
        {
            List<FileEntity> files = new List<FileEntity>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT FileID, OwnerID, [FileName], Extension, Size, UploadDate, "
                    + "Downloads, FullName, AccessType");
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
                        file.Name = reader.GetString(2);
                        file.Extension = reader.GetString(3);
                        file.Size = reader.GetInt32(4);
                        file.UploadDate = reader.GetDateTime(5);
                        file.Downloads = reader.GetInt32(6);
                        file.FullName = reader.GetString(7);
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

                for(int i = 0; i < files.Count; i++)
                    files[i].Owner = GetUserById(files[i].Owner.ID);
            }
            return files;
        }

        public IEnumerable<FileEntity> GetAllFilesOwnedByUser(User user)
        {
            List<FileEntity> files = new List<FileEntity>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT FileID, [FileName], Extension, Size, UploadDate, "
                    + "Downloads, FullName, AccessType WHERE OwnerID = @oid");
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
                        file.Name = reader.GetString(1);
                        file.Extension = reader.GetString(2);
                        file.Size = reader.GetInt32(3);
                        file.UploadDate = reader.GetDateTime(4);
                        file.Downloads = reader.GetInt32(5);
                        file.FullName = reader.GetString(6);
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
                    }
                }

                for (int i = 0; i < files.Count; i++)
                    files[i].Owner = GetUserById(files[i].Owner.ID);
            }
            return files;
        }

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
                        user.Password = reader.GetString(2);
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
                        users.Add(user);
                    }
                }
            }
            return users;
        }

        public FileEntity GetFileById(int id)
        {
            FileEntity file = null;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT FileID, OwnerID, [FileName], Extension, Size, UploadDate, "
                    + "Downloads, FullName, AccessType FROM MegaFileStorage.Files WHERE FileID = @fid");
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
                        file.Name = reader.GetString(2);
                        file.Extension = reader.GetString(3);
                        file.Size = reader.GetInt32(4);
                        file.UploadDate = reader.GetDateTime(5);
                        file.Downloads = reader.GetInt32(6);
                        file.FullName = reader.GetString(7);
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

                if (file != null)
                    file.Owner = GetUserById(file.Owner.ID);
            }

            return file;
        }
        
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

        public bool RemoveFile(string filename)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("DELETE FROM MegaFileStorage.Files WHERE FileName = @fn");
                command.Connection = connection;
                command.Parameters.AddWithValue("@fn", filename);

                connection.Open();

                return command.ExecuteNonQuery() == 1;
            }
        }

        public bool RemoveUser(string username)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("DELETE FROM MegaFileStorage.Users WHERE UserName = @un");
                command.Connection = connection;
                command.Parameters.AddWithValue("@un", username);

                connection.Open();

                return command.ExecuteNonQuery() == 1;
            }
        }

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

        public FileEntity GetFileByNameOwnedByUser(string filename, string username)
        {
            FileEntity file = null;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT f.FileID, f.OwnerID, f.[FileName], f.Extension, f.Size, "
                    + "f.UploadDate, f.Downloads, f.FullName, f.AccessType FROM MegaFileStorage.Files as f, "
                    + "MegaFileStorage.Users as u WHERE f.OwnerID = u.UserID AND u.UserName = @un AND f.FileName = "
                    + "@fn");
                command.Connection = connection;
                command.Parameters.AddWithValue("@un", username);
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
                        file.Size = reader.GetInt32(4);
                        file.UploadDate = reader.GetDateTime(5);
                        file.Downloads = reader.GetInt32(6);
                        file.FullName = reader.GetString(7);
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

        public bool CheckFolderName(string name, int userid)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT FileName FROM MegaFileStorage.Files WHERE FullName = @fn AND "
                    + "OwnerID = @oid");
                command.Connection = connection;
                command.Parameters.AddWithValue("@fn", name);
                command.Parameters.AddWithValue("@oid", userid);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                    return reader.HasRows;
            }
        }

        public bool CreateSubFolder(string name, User user)
        {
            user = GetUserByName(user.Name);
            if (CheckFolderName(name, user.ID))
                throw new ArgumentException("Существующее имя каталога.", "Name");
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO MegaFileStorage.Files (OwnerID, FileName, Extension,"
                    + " UploadDate, FullName, AccessType) VALUES(@oid, @n, @e, @ud, @fn, @at)");
                command.Connection = connection;
                command.Parameters.AddWithValue("@oid", user.ID);
                command.Parameters.AddWithValue("@n", name.Substring(name.LastIndexOf('\\') + 1));
                command.Parameters.AddWithValue("@e", "folder");
                command.Parameters.AddWithValue("@ud", DateTime.Now);
                command.Parameters.AddWithValue("@fn", name);

                connection.Open();

                return command.ExecuteNonQuery() == 1;

            }
        }
    }
}
