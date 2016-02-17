using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task02
{
    /// <summary>
    /// Класс StringHelper добавляет метод расширения для типа string
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// Метод IsPositiveInteger() проверяет является ли число положительным целым числом.
        /// Возвращает true, если строка является положительным целым числом, в потивном случае - false
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsPositiveInteger(this string str)
        {
            char[] digits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            bool b = true;
            int i = 0;
            //если в массиве будет какой-либо элемент, кроме цифр - возвращаем false
            while (i < str.Length && b == true)
                b = digits.Contains(str[i++]);
            return b;
        } 
    }
}
