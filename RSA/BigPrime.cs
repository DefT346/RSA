using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    internal class BigPrime
    {
        private BigInteger result, d;
        private int f;

        public int bitsCount = 0;

        public BigPrime(int size = 512)
        {
            bitsCount = size;
            //byte[] randomNumber = new byte[size / 8];
            var randomNumber = GetRandom(size);
            result = new BigInteger(randomNumber);
            result = BigInteger.Abs(result);

            CorrectToNearestPrime();
        }

        //private BigInteger GetRandom(long size)
        //{
        //    BigInteger value = 0;
        //    var rnd = new Random();
        //    for (int i = 0; i < size; i++)
        //    {
        //        var bit = rnd.Next(0, 2) == 0 ? 1 : 0;
        //        value.SetBit(i, bit);
        //    }
        //    return value;
        //}

        public static BigInteger GetMinByBitCount(int bitsCount)
        {
            return BigInteger.Pow(2, bitsCount - 1);
        }

        private byte[] GetRandom(long bits)
        {
            var bytes = new List<byte>();

            int byteValue = 0;
            var rnd = new Random();
            for (int i = 0; i < bits; i++)
            {

                var bit = rnd.Next(0, 2) == 0 ? 1 : 0;

                if (i == bits - 1)
                    byteValue.SetBit(i % 8, 1);
                else
                    byteValue.SetBit(i % 8, bit);

                if (i % 8 == 7)
                {
                    bytes.Add((byte)byteValue);
                    byteValue = 0;
                }
                if (i == bits - 1)
                {
                    bytes.Add((byte)byteValue);
                }
            }

            return bytes.ToArray();
        }

        //private byte[] GetRandomMin(long bits)
        //{
        //    var bytes = new List<byte>();

        //    int byteValue = 0;
        //    var rnd = new Random();
        //    for (int i = 0; i < bits; i++)
        //    {

        //        var bit = rnd.Next(0, 2) == 0 ? 1 : 0;
        //        byteValue.SetBit(i % 8, bit);

        //        if (i % 8 == 7)
        //        {
        //            bytes.Add((byte)byteValue);
        //            byteValue = 0;
        //        }
        //        if (i == bits - 1)
        //        {
        //            bytes.Add((byte)byteValue);
        //        }
        //    }

        //    return bytes.ToArray();
        //}

        private void FillRandom(byte[] array)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = (byte)new Random().Next(0, 255);
            //RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            //rng.GetBytes(array);
        }

        public static string ToBase64(BigInteger value)
        {
            return Convert.ToBase64String(value.ToByteArray());
        }

        public BigInteger ToBigInteger()
        {
            return result;
        }
        public override string ToString()
        {
            return result.ToString();
        }

        public static BigInteger operator *(BigPrime a, BigPrime b)
        {
            return a.result * b.result;
        }

        public static BigInteger operator -(BigPrime a, BigPrime b)
        {
            return a.result - b.result;
        }

        public static BigInteger operator -(BigPrime a, int b)
        {
            return a.result - b;
        }

        private bool MillerRabinTest(int k) 
        {
            if (result == 2 || result == 3)
                return true;
            if (result < 2 || result % 2 == 0)
                return false;

            BigInteger d = result - 1;
            int s = 0;

            while (d % 2 == 0)
            {
                d /= 2;
                s += 1;

            }

            for (int i = 0; i < k; i++)
            {
                byte[] _a = new byte[result.ToByteArray().LongLength];
                BigInteger a;

                do
                {
                    //FillRandom(_a);
                    //a = new BigInteger(_a);

                    //var size = result.ToByteArray().LongLength * 8;
                    //a = GetRandom(bitsSize);

                    _a = GetRandom(bitsCount);
                    a = new BigInteger(_a);
                }
                while (a < 2 || a >= result - 2);

                BigInteger x = BigInteger.ModPow(a, d, result);

                if (x == 1 || x == result - 1)
                    continue;

                for (int r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, result);

                    if (x == 1)
                        return false;
                    if (x == result - 1)
                        break;
                }

                if (x != result - 1)
                    return false;
            }
            return true;
        }

        private BigInteger CorrectToNearestPrime() 
        {
            while (MillerRabinTest(10) == false)
            {
                result++;
            }
            return result;
        }
    }
}
