using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    internal class Utils
    {

        public static (BigInteger x, BigInteger y, BigInteger gcd) EuclidAdvanced(BigInteger a, BigInteger b)
        {
            a = BigInteger.Abs(a);
            b = BigInteger.Abs(b);

            if (a < b)
            {
                var t = a;
                a = b;
                b = t;
            }

            BigInteger ttx = 1;
            BigInteger tx = 0;

            BigInteger tty = 0;
            BigInteger ty = 1;

            BigInteger ttr = a;
            BigInteger tr = b;

            BigInteger tq = 0;

            BigInteger i = 0;

            //WriteRow("i", "ri", "xi", "yi", "qi");

            while (true)
            {
                var r = ttr - tr * tq;

                if (r == 0) break;

                //var q = (int)Math.Truncate(tr / (double)r);
                var q = BigInteger.DivRem(tr, r, out BigInteger remainder);

                var x = ttx - tq * tx;
                var y = tty - tq * ty;

                //WriteRow(i, r, x, y, q);

                ttx = tx; tx = x;
                tty = ty; ty = y;

                ttr = tr; tr = r;

                tq = q;
                i++;
            }

            //Console.WriteLine($"\n{a} * {tx} + {b} * {ty} = {a * tx + b * ty} (gcd: {tr})");

            return (tx, ty, tr);
        }

        public static void WriteRow(params object[] elements)
        {
            var y = Console.GetCursorPosition().Top;
            var step = 7;

            for (int i = 0; i < elements.Length; i++)
            {
                Console.SetCursorPosition(i * step, y);
                Console.Write(elements[i]);
            }
            Console.WriteLine();
        }
    }
}
