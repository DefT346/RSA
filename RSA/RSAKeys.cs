using System.Numerics;

namespace RSA
{
    public class RSAKeys
    {
        public BigInteger e { get; private set; }
        public BigInteger d { get; private set; }
        public BigInteger n { get; private set; }
        public int size { get; private set; }

        public int blockSize { get => size - 1; }

        public RSAKeys(int size)
        {
            this.size = size;
            Generate(this.size);
        }

        public void Generate(int size)
        {
            Console.WriteLine("Поиск простых чисел..");
            var p = new BigPrime(size / 2);
            var q = new BigPrime(size / 2);
            Console.WriteLine($"\np = {p}");
            Console.WriteLine($"\nq = {q}");

            Console.WriteLine("\nГенерация ключей");
            n = p * q;

            Console.WriteLine($"\nn = {n}");

            var fi = Euler(p, q);

            e = new BigPrime(p.bitsCount + q.bitsCount).ToBigInteger();
            while (e >= fi)
            {
                e -= fi - 1;
            }
            while (true)
            {
                if (BigInteger.GreatestCommonDivisor(e, fi) == 1)
                    break;
                else e -= 1;
            }


            ExtGCD(fi, e, out _, out BigInteger y); // [mx + de = 1]; (ax + by = 1) => e = y; de = 1 (mod m)
            d = y;

            if (d < 0)
                d += fi;
        }

        static BigInteger Euler(BigPrime p, BigPrime q) => (p - 1) * (q - 1); // Функция Эйлера

        public static BigInteger ExtGCD(BigInteger a, BigInteger b, out BigInteger x, out BigInteger y)
        {
            if (b == 0)
            {
                x = 1;
                y = 0;
                return a;
            }
            BigInteger g = ExtGCD(b, a % b, out y, out x); // x и y - переставляются
            y = y - (a / b) * x;
            return g;
        }
    }
}
