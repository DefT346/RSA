using System.Numerics;

namespace RSA
{
    internal class RSAKeys
    {
        public BigInteger e { get; private set; }
        public BigInteger d { get; private set; }
        public BigInteger n { get; private set; }
        public int size { get; private set; }

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
            var n = p * q;

            var fi = Euler(p, q);

            BigInteger d = new BigPrime(p.bitsCount + q.bitsCount).ToBigInteger();
            while (d >= fi)
            {
                d -= fi - 1;
            }
            while (true)
            {
                if (BigInteger.GreatestCommonDivisor(d, fi) == 1)
                    break;
                else d -= 1;
            }

            BigInteger e = AdvEuler(fi, d).y; // [mx + de = 1]; (ax + by = 1) => e = y; de = 1 (mod m)

            if (e < 0)
                e += fi;

            this.e = e;
            this.d = d;
            this.n = n;
        }

        static BigInteger Euler(BigPrime p, BigPrime q) => (p - 1) * (q - 1);
        static (BigInteger x, BigInteger y) AdvEuler(BigInteger a, BigInteger b)
        {
            Matrix E = new Matrix(new BigInteger[,]
            {
                { 1, 0 },
                { 0, 1 }
            });

            while (true)
            {
                var max = BigInteger.Max(a, b);
                var min = BigInteger.Min(a, b);
                a = max;
                b = min;

                var q = BigInteger.DivRem(a, b, out BigInteger remainder);
                if (remainder == 0)
                {
                    var col = E.Column(1);
                    return (col[0], col[1]);
                }
                else
                {
                    var Eh = new Matrix(new BigInteger[,]
                    {
                        { 0, 1 },
                        { 1, -q }
                    });

                    E = E * Eh;

                    a = b;
                    b = remainder;
                }
            }
        }
    }
}
