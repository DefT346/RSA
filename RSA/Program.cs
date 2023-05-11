using RSA;
using System.Text;

//var keys = new RSAKeys(32);


//Console.WriteLine($"\nОткрытый ключ e: {keys.e}");
//Console.WriteLine($"\nЗакрытый ключ d: {keys.d}");

while (true)
{
    Console.Write($"\nВведите сообщение: ");
    var message = Console.ReadLine();

    //var encodedData = RSAProvider.Encrypt(keys.e, keys.n, Encoding.UTF8.GetBytes(message), blocksize);
    //var source = RSAProvider.Decrypt(keys.d, keys.n, encodedData, blocksize);

    var res = RSAProvider.Test(Encoding.UTF8.GetBytes(message), 3620132861, 1111729205, 4196583073, 32);

    Console.WriteLine("\nРасшифрованное сообщение: " + Encoding.UTF8.GetString(res));
}
