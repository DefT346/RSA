using RSA;

var keys = new RSAKeys(2048);

Console.WriteLine($"\nОткрытый ключ e: {keys.e}");
Console.WriteLine($"\nЗакрытый ключ d: {keys.d}");

while (true)
{
    Console.Write($"\nВведите сообщение: ");
    var message = Console.ReadLine();

    var encodedData = RSAProvider.Encrypt(keys.e, keys.n, message!);

    Console.WriteLine("\nРасшифрованное сообщение: " + RSAProvider.Decrypt(keys.d, keys.n, encodedData));
}
