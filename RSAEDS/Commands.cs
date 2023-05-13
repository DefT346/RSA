using RSA;
using System.Numerics;
using System.Reflection;

namespace RSAEDS
{

    public static class Extensions
    {
        public static T[] SubArray<T>(this T[] array, int offset, int length)
        {
            return new List<T>(array)
                        .GetRange(offset, length)
                        .ToArray();
        }

        public static T[] SubArray<T>(this T[] array, int offset)
        {
            return new List<T>(array)
                        .GetRange(offset, array.Length - offset)
                        .ToArray();
        }
    }

    internal class Commands
    {
        internal static void Print(string mess, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(mess);
            Console.ResetColor();
        }

        internal static void Parse(string input)
        {
            try
            {
                var data = input.Split(' ');
                var key = data[0].ToLower();
                var method = typeof(Commands).GetMethod(key, BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.Public);
                var body = data.SubArray(1);
                if (method == null) throw new Exception("Команда не найдена");
                method.Invoke(null, new object[] { body });
            }
            catch (Exception ex)
            {
                Print("\n" + ex.Message, ConsoleColor.Red);
            }
        }

        public static void Sign(params string[] args)
        {
            // Создание подписи
            Print("\nСоздание подписи", ConsoleColor.Yellow);

            var document = FileDialog.ReadFile(FileDialog.ShowDialog()).Result;

            Console.WriteLine();
            var signature = new EDSignature(document, 100);

            Console.WriteLine($"\nОткрытый ключ: {signature.openKey}"); 
            Console.WriteLine($"\nЗашифрованный хеш: {Utils.Byte16(signature.encHash)}");
            Console.WriteLine($"\nn: {signature.n}");

            Print($"\nПодпись создана", ConsoleColor.Green);
        }

        public static void Verify(params string[] args)
        {
            Console.Write("\nВведите открытый ключ: "); var ok = BigInteger.Parse(Console.ReadLine());
            Console.Write("\nВведите зашифрованный хеш: "); var eh = Utils.Parse16(Console.ReadLine());
            Console.Write("\nВведите n: "); var n = BigInteger.Parse(Console.ReadLine());

            var doc = FileDialog.ReadFile(FileDialog.ShowDialog()).Result;

            var decHash = RSAProvider.Decrypt(ok, n, eh, 100);
            Utils.PrintByteArray("\nРасшифрованный хеш: \n", decHash);

            var hash = EDSignature.ComputeSHA512(doc);
            Utils.PrintByteArray("\nХеш открытого документа: \n", hash);

            if (decHash.SequenceEqual(hash)) Print("\nПодпись подтверждена", ConsoleColor.Green);
            else Print("\nПодпись не подтверждена", ConsoleColor.Red);
        }
    }
}
