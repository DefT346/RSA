using RSA;
using System.Text;

var keys = new RSAKeys(1024);


Console.WriteLine($"\nОткрытый ключ e: {keys.e}");
Console.WriteLine($"\nЗакрытый ключ d: {keys.d}");

while (true)
{
    var blocksize = 100; // размер блока шифруемых данных в байтах (входное сообщение делится на блоки по 100 байт)

    Console.Write($"\nВведите сообщение: "); var message = Console.ReadLine();

    var encodedData = RSAProvider.Encrypt(keys.e, keys.n, Encoding.UTF8.GetBytes(message), blocksize);
    var source = RSAProvider.Decrypt(keys.d, keys.n, encodedData, blocksize);

    Console.WriteLine("\nРасшифрованное сообщение: " + Encoding.UTF8.GetString(source));
}
