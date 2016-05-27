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
        public DirectoryInfo Storage { get; set; }

        public FDAL(string path)
        {
            Storage = new DirectoryInfo(path);
        }

        /// <summary>
        /// Метод CreateFolder создаёт подкаталог в каталоге root с именем foldername
        /// </summary>
        /// <param name="root"></param>
        /// <param name="foldername"></param>
        public void CreateFolder(FileEntity root, string foldername)
        {
            Storage.CreateSubdirectory(root.FullName + '\\' + foldername);
        }

        /// <summary>
        /// Метод GetPath возвращает путь к хранищилищу файлов
        /// </summary>
        /// <returns></returns>
        public string GetPath()
        {
            return Storage.FullName;
        }

        /// <summary>
        /// Метода RemoveFile удаляет file из файловой системы
        /// </summary>
        /// <param name="file"></param>
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