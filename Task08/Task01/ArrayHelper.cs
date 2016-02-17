using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01
{
    /// <summary>
    /// Класс ArrayHelper добавляет метод расширения Sum для массивов всех целочисленных типов,
    /// кроме char, и типов чисел с плавающей точкой 
    /// </summary>
    public static class ArrayHelper
    {
        /// <summary>
        /// Метод Sum возвращает сумму всех элементов массива
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int Sum(this int[] array)
        {
            int sum = default(int);
            foreach (int i in array)
                sum += i;
            return sum;
        }

        /// <summary>
        /// Метод Sum возвращает сумму всех элементов массива
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static uint Sum(this uint[] array)
        {
            uint sum = default(uint);
            foreach (uint i in array)
                sum += i;
            return sum;
        }

        /// <summary>
        /// Метод Sum возвращает сумму всех элементов массива
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static double Sum(this double[] array)
        {
            double sum = default(double);
            foreach (double i in array)
                sum += i;
            return sum;
        }

        /// <summary>
        /// Метод Sum возвращает сумму всех элементов массива
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static float Sum(this float[] array)
        {
            float sum = default(float);
            foreach (float i in array)
                sum += i;
            return sum;
        }

        /// <summary>
        /// Метод Sum возвращает сумму всех элементов массива
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static byte Sum(this byte[] array)
        {
            byte sum = default(byte);
            foreach (byte i in array)
                sum += i;
            return sum;
        }

        /// <summary>
        /// Метод Sum возвращает сумму всех элементов массива
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static sbyte Sum(this sbyte[] array)
        {
            sbyte sum = default(sbyte);
            foreach (sbyte i in array)
                sum += i;
            return sum;
        }

        /// <summary>
        /// Метод Sum возвращает сумму всех элементов массива
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static short Sum(this short[] array)
        {
            short sum = default(short);
            foreach (short i in array)
                sum += i;
            return sum;
        }

        /// <summary>
        /// Метод Sum возвращает сумму всех элементов массива
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static ushort Sum(this ushort[] array)
        {
            ushort sum = default(ushort);
            foreach (ushort i in array)
                sum += i;
            return sum;
        }

        /// <summary>
        /// Метод Sum возвращает сумму всех элементов массива
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static long Sum(this long[] array)
        {
            long sum = default(long);
            foreach (long i in array)
                sum += i;
            return sum;
        }

        /// <summary>
        /// Метод Sum возвращает сумму всех элементов массива
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static ulong Sum(this ulong[] array)
        {
            ulong sum = default(ulong);
            foreach (ulong i in array)
                sum += i;
            return sum;
        }
    }
}
