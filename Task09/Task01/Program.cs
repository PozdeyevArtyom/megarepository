using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task01
{
    class Program
    {
        public static string Path = "C:\\VSProjects\\megafolder";
        public static string Temp;
        
        //метод ReadDate() считывает дату и время с консоли
        public static DateTime ReadDate()
        {
            Console.WriteLine("\nВведите дату отката:");
            int year = 0, month = 0, day = 0, hour = 0, minute = 0, second = 0;
            Console.Write("  Год: ");
            while (!int.TryParse(Console.ReadLine(), out year) || year > DateTime.Now.Year+1)
                Console.Write("    Неверный ввод. Введите целое число меньше {0}: ", DateTime.Now.Year+1);

            Console.Write("  Месяц: ");
            if (year == DateTime.Now.Year)
                while (!int.TryParse(Console.ReadLine(), out month) || month > DateTime.Now.Month)
                    Console.Write("    Неверный ввод. Введите целое число в диапазоне от 1 до {0}: ", DateTime.Now.Month);
            else
                while (!int.TryParse(Console.ReadLine(), out month) || month > 12)
                    Console.Write("    Неверный ввод. Введите целое число в диапазоне от 1 до 12: ");

            Console.Write("  День: ");
            if (month == 2)
            {
                if (year % 4 == 0 && year % 100 != 0 || year % 400 == 0)
                    while (!int.TryParse(Console.ReadLine(), out day) || day > 29)
                        Console.Write("    Неверный ввод. Введите целое число в диапазоне от 1 до 29: ");
                else
                    while (!int.TryParse(Console.ReadLine(), out day) || day > 28)
                        Console.Write("    Неверный ввод. Введите целое число в диапазоне от 1 до 28: ");
            }
            else
            {
                if (month == 4 || month == 6 || month == 9 || month == 11)
                    while (!int.TryParse(Console.ReadLine(), out day) || day > 30)
                        Console.Write("    Неверный ввод. Введите целое число в диапазоне от 1 до 30: ");
                else
                    while (!int.TryParse(Console.ReadLine(), out day) || day > 31)
                        Console.Write("    Неверный ввод. Введите целое число в диапазоне от 1 до 31: ");
            }

            Console.Write("  Час: ");
            while (!int.TryParse(Console.ReadLine(), out hour) || hour > 24)
                Console.Write("    Неверный ввод. Введите целое число в диапазоне от 1 до 24: ");

            Console.Write("  Минута: ");
            while (!int.TryParse(Console.ReadLine(), out minute) || minute > 60)
                Console.Write("    Неверный ввод. Введите целое число в диапазоне от 1 до 60: ");

            Console.Write("  Секунда: ");
            while (!int.TryParse(Console.ReadLine(), out second) || second > 60)
                Console.Write("    Неверный ввод. Введите целое число в диапазоне от 1 до 60: ");

            return new DateTime(year, month, day, hour, minute, second);
        }

        //метод GetDates(DirectoryInfo directory) возвращает коллекцию дат всех сохранений из директории directory
        public static ICollection<DateTime> GetDates(DirectoryInfo directory)
        {
            List<DateTime> result = new List<DateTime>();
            int year, month, day, hour, minute, second;

            //Если название директории можно пропарсить следующим образом, то это директория сохранения
            foreach (DirectoryInfo dir in directory.GetDirectories())
                if(int.TryParse(dir.Name.Substring(0, 2), out day) &&
                    int.TryParse(dir.Name.Substring(3, 2), out month) &&
                    int.TryParse(dir.Name.Substring(6, 4), out year) &&
                    int.TryParse(dir.Name.Substring(11, 2), out hour) &&
                    int.TryParse(dir.Name.Substring(14, 2), out minute) &&
                    int.TryParse(dir.Name.Substring(17, 2), out second))
                    result.Add(new DateTime(year, month, day, hour, minute, second));
            return result;
        }

        //возвращает наиболее подходящую дату для отката
        public static DateTime TheMostAppropriate(DateTime[] array, DateTime key)
        {
            Array.Sort(array);
            int i = 0;
            while (i < array.Length && array[i] < key)
                i++;
            return array[--i];
        }

        //метод Clone(DirectoryInfo savingfolder, DirectoryInfo tempfolder) клонирует savingfolder в tempfolder
        public static void Clone(DirectoryInfo savingfolder, DirectoryInfo tempfolder)
        {
            foreach(FileInfo file in savingfolder.EnumerateFiles())
            {
                FileInfo newfile = new FileInfo(tempfolder.FullName + '\\' + file.Name);
                using (newfile.Create()) { }
                file.CopyTo(newfile.FullName, true);
            }

            foreach(DirectoryInfo dir in savingfolder.EnumerateDirectories()) 
            {
                DirectoryInfo newdir = tempfolder.CreateSubdirectory(dir.Name);
                Clone(dir, newdir);
            }
        }

        //собите изменения файла
        public static void onChange(object source, FileSystemEventArgs e)
        {
            //сообщаем об изменении
            Console.WriteLine("Файл '{0}' был изменён!", e.FullPath);
            
            //сохраняем изменения в папку с именем текущей даты
            char[] c = DateTime.Now.ToString().ToCharArray();
            while (Array.IndexOf(c,':') != -1)
                c[Array.IndexOf(c, ':')] = '-';
            string date = new string(c);
            DirectoryInfo tempfolder = new DirectoryInfo(Temp + date);
            tempfolder.Create();
            Clone(new DirectoryInfo(Path), tempfolder);
        }

        static void Main(string[] args)
        {
            //удаляем лишний слеш если таковой имеется
            if (Path[Path.Length - 1] == '\\')
                Path = Path.Substring(0, Path.Length - 1);
            int i = Path.LastIndexOf('\\');
            //создаём каталог временных файлов
            Temp = Path.Substring(0, i + 1) + "temp\\";

            Console.WriteLine("0 | Режим наблюдения");
            Console.WriteLine("1 | Откат изменений");
            char c = Console.ReadKey().KeyChar;
            switch (c)
            {
                case '0':
                    Console.WriteLine("\nРежим наблюдения включён.");
                    FileSystemWatcher watcher = new FileSystemWatcher(Path, "*.txt");
                    watcher.IncludeSubdirectories = true;
                    //подписываемся на событие
                    watcher.Changed += new FileSystemEventHandler(onChange);
                    //начинам слежение
                    watcher.EnableRaisingEvents = true;
                    break;
                case '1':
                    //считываем дату
                    DateTime date = ReadDate();
                    //ищем наиболее подъодящую дате для отката
                    DateTime[] datesofsavings = GetDates(new DirectoryInfo(Temp)).ToArray();
                    date = TheMostAppropriate(datesofsavings, date);
                    char[] cdate = date.ToString().ToCharArray();
                    while (Array.IndexOf(cdate, ':') != -1)
                        cdate[Array.IndexOf(cdate, ':')] = '-';
                    //откатываем
                    Clone(new DirectoryInfo(Temp + new string(cdate)), new DirectoryInfo(Path));
                    Console.WriteLine("Произведён откат на {0}", date.ToString());
                    break;
                default:
                    Console.WriteLine("Неверный ввод.");
                    break;
            }

            Console.ReadLine();
        }
    }
}
