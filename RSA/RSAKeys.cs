﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    internal class RSAKeys
    {

        public static (BigInteger e, BigInteger d, BigInteger n) Generate(int size)
        {
            Console.WriteLine("Поиск простых чисел..");
            var p = new BigPrime(size / 2);
            var q = new BigPrime(size / 2);
            Console.WriteLine("Генерация ключей");
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
            {

                e += fi;
            }

            return (e, d, n);
        }

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

        static BigInteger Euler(BigPrime p, BigPrime q) => (p - 1) * (q - 1);

        //public BigInteger GeneratePrivateKey(BigInteger e, BigInteger m)
        //{
        //    var d = (1 / e) % m;
        //    return d;
        //}

        //public BigInteger GeneratePublicKey(BigInteger e, BigInteger n, BigInteger m) 
        //{
        //    while (e > 1)
        //    {
        //        BigInteger ef = BigInteger.Pow(e, m);
        //        BigInteger.ModPow();
        //        ef = 1 % n;
        //    }
        //    return e;
        //}
    }
}