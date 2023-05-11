using RSA;
using System.Text;

var keys = new RSAKeys(1024);

Console.WriteLine($"\nОткрытый ключ e: {keys.e}");
Console.WriteLine($"\nЗакрытый ключ d: {keys.d}");

while (true)
{
    Console.Write($"\nВведите сообщение: ");
    var message = Console.ReadLine();

    var encodedData = RSAProvider.Encrypt(keys.e, keys.n, Encoding.UTF8.GetBytes(message), keys.size);
    var source = RSAProvider.Decrypt(keys.d, keys.n, encodedData, keys.size);

    ///////var m = Encoding.UTF8.GetBytes("fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff");

    //var data = RSAProvider.toBigIntegers(m, 1024);
    //var b = RSAProvider.toBytes(data);

    //var dd = RSAProvider.toBigIntegers(b, 1024);
    //var res = RSAProvider.toBytes(dd);

    //Console.WriteLine(Encoding.UTF8.GetString(res));

    /////var source = RSAProvider.Test(m, keys.e, keys.d, keys.n, keys.size);

    Console.WriteLine("\nРасшифрованное сообщение: " + Encoding.UTF8.GetString(source));
}
