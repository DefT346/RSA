using RSA;
using System.Numerics;


var keys = new RSAKeys(1024);

Console.WriteLine($"\nОткрытый ключ e: {keys.e}");
Console.WriteLine($"\nЗакрытый ключ d: {keys.d}");


while (true)
{
    Console.Write($"\nВведите сообщение: ");
    var message = Console.ReadLine();

    var encodedData = RSAProvider.Encode(keys.e, keys.n, 300, message);

    Console.WriteLine("\nРасшифрованное сообщение: " + RSAProvider.Decode(keys.d, keys.n, encodedData));
}
