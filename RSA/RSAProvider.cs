using System.Numerics;
using System.Text;

namespace RSA
{
    public class RSAProvider
    {

        public static byte[] Encrypt(BigInteger e, BigInteger n, byte[] bytes, int blockSize)
        {
            var inblocks = ToIntegersArray(bytes, blockSize, true);

            var resultBytes = new List<byte>();

            var maxblock = 130; // максимальный размер блока который может понадобиться для числа полсе мода (зависит от размера ключа)

            for (int i = 0; i < inblocks.Length; i++)
            {
                var buffer = new byte[maxblock];
                var newval = BigInteger.ModPow(inblocks[i], e, n);

                var byteArray = newval.ToByteArray();

                Array.Copy(byteArray, buffer, byteArray.Length > maxblock ? maxblock : byteArray.Length);

                resultBytes.AddRange(buffer);
            }

            return resultBytes.ToArray();
        }

        public static byte[] Decrypt(BigInteger d, BigInteger n, byte[] encodedData, int blockSize)
        {

        }

        public static byte[] Test(byte[] inbytes, BigInteger e, BigInteger d, BigInteger n, int blockSize)
        {
            var inblocks = ToIntegersArray(inbytes, blockSize, true);

            var resultBytes = new List<byte>();


            var maxblock = 130; // тут максимальный размер блока который может понадобиться для числа полсе мода (зависит от размера ключа)

            for (int i = 0; i < inblocks.Length; i++)
            {
                var buffer = new byte[maxblock];
                var newval = BigInteger.ModPow(inblocks[i], e, n);

                ///проверка
                var check = BigInteger.ModPow(newval, d, n);

                //var newval = inblocks[i];
                var byteArray = newval.ToByteArray();

                Array.Copy(byteArray, buffer, byteArray.Length > maxblock ? maxblock : byteArray.Length);

                resultBytes.AddRange(buffer);
            }



            var resbytes = new List<byte>();
            var newinblocks = ToIntegersArray(resultBytes.ToArray(), maxblock, false);

            for (int i = 0; i < newinblocks.Length; i++)
            {
                var buffer = new byte[blockSize];
                newinblocks[i] = BigInteger.ModPow(newinblocks[i], d, n);
                var byteArray = newinblocks[i].ToByteArray();

                Array.Copy(byteArray, buffer, byteArray.Length < blockSize ? byteArray.Length : blockSize);

                resbytes.AddRange(buffer);
            }

            return resbytes.ToArray();

        }

        private static BigInteger[] ToIntegersArray(byte[] bytes, int block, bool addZero)
        {
            List<BigInteger> blocks = new List<BigInteger>();

            var step = block;
            var cursor = 0;
            while (true)
            {            
                var blockBuffer = new List<byte>();
                for (int i = 0; i < step; i++)
                {
                    if (cursor + i >= bytes.Length) break;

                    blockBuffer.Add(bytes[cursor + i]);
                }
                if (addZero) blockBuffer.Add(0);
                blocks.Add(new BigInteger(blockBuffer.ToArray()));
                cursor += step;
                if (cursor >= bytes.Length) break;
            }

            return blocks.ToArray();
        }
    }
}
