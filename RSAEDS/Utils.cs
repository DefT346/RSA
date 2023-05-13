using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSAEDS
{
    internal class Utils
    {
        public static string Byte16(byte[] bytes)
        {
            return string.Join(" ", bytes.Select(x => x.ToString("X2")));
        }

        public static void PrintByteArray(string message, byte[] bytes)
        {
            string hex = message + Byte16(bytes);
            Console.WriteLine(hex);
        }

        public static byte[] Parse16(string row)
        {
            List<byte> buffer = new List<byte>();
            var arr = row.Split(' ');
            for (int i = 0; i < arr.Length; i++)
            {
                buffer.Add((byte)Convert.ToInt16(arr[i], 16));
            }
            return buffer.ToArray();
        }
    }
}
