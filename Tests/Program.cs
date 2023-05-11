

using System.Numerics;
using System.Text;

void PrintByteArray(byte[] bytes)
{
    var sb = new StringBuilder("{ ");
    foreach (var b in bytes)
    {
        sb.Append(b + ", ");
    }
    sb.Append("}");
    Console.WriteLine(sb.ToString());
}

for(BigInteger num = 30000; num < BigInteger.Pow(2,16); num++)
{
    Console.Write($"{num} = "); PrintByteArray(num.ToByteArray());
}