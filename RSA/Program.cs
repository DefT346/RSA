using RSA;
using System.Numerics;


var keys = RSAKeys.Generate(1024);

Console.WriteLine($"\nОткрытый ключ e: {keys.e}");
Console.WriteLine($"\nЗакрытый ключ d: {keys.d}");

Console.WriteLine($"\nВведите сообщение a: ");
//BigInteger a = BigInteger.Parse(Console.ReadLine());
BigInteger a = BigInteger.Parse("45623462344654734325465473432344654734654734325465474654734325465473432344654");

var b = BigInteger.ModPow(a, keys.e, keys.n);

var _a = BigInteger.ModPow(b, keys.d, keys.n);

Console.WriteLine($"\nВходное сообщение: {a}");
Console.WriteLine($"Выхоное сообщение: {_a}");



// Шифруемые данные необходимо разбить на блоки
// Массив чисел от 0 до n - 1

//string message = "тест"; 
////var data = Encoding.UTF8.GetBytes(message);
//var bytes = new byte[511];

//if (a > n - 1)
//{
//    "Ошибка. Число сообщения больше открытого ключа");
//}
