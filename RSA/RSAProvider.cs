using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    internal class RSAProvider
    {

        private static BigInteger[] ToNums(int blockSize, string text)
        {
            List<BigInteger> blocks = new List<BigInteger>();

            var block = "";
            for (int i = 0; i < text.Length; i++)
            {
                block += text[i];
                if (i != 0) 
                if (i % blockSize == 0 || i == text.Length - 1)
                {
                    blocks.Add(new BigInteger(Encoding.UTF8.GetBytes(block)));
                    block = "";
                }
            }

            return blocks.ToArray();
        }

        private static string ToString(params BigInteger[] blocks)
        {
            string text = "";
            for (int i = 0; i < blocks.Length; i++)
            {
                text += Encoding.UTF8.GetString(blocks[i].ToByteArray());
            }

            return text;
        }

        public static BigInteger[] Encode(BigInteger e, BigInteger n, int blockSize, string message)
        {
            var blocks = ToNums(blockSize, message);

            for(int i = 0; i< blocks.Length; i++)
                blocks[i] = BigInteger.ModPow(blocks[i], e, n);

            return blocks;
        }

        public static string Decode(BigInteger d, BigInteger n, BigInteger[] encodedData)
        {
            var message = "";
            for (int i = 0; i < encodedData.Length; i++)     
                message += ToString(BigInteger.ModPow(encodedData[i], d, n));
            
            return message;
        }
    }
}
