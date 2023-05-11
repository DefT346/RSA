using System.Numerics;
using System.Text;

namespace RSA
{
    public class RSAProvider
    {

        public static byte[] Encrypt(BigInteger e, BigInteger n, byte[] bytes, int blockSize)
        {
            var step = blockSize / 8; //1024 / 8 = 128

            var inblocks = ToIntegersArray(bytes, step, true);

            var resultBytes = new List<byte>();

            step++; // расширяем буфер на один байт больше 

            for (int i = 0; i < inblocks.Length; i++)
            {
                var buffer = new byte[step];
                var byteArray = BigInteger.ModPow(inblocks[i], e, n).ToByteArray();

                Array.Copy(byteArray, buffer, byteArray.Length > step ? step : byteArray.Length);

                resultBytes.AddRange(buffer);
            }

            return resultBytes.ToArray();
        }

        public static byte[] Decrypt(BigInteger d, BigInteger n, byte[] encodedData, int blockSize)
        {
            var bytes = new List<byte>();
            var step = blockSize / 8; //1024 / 8 = 128

            var inblocks = ToIntegersArray(encodedData, step + 1, false);
            for (int i = 0; i < inblocks.Length; i++)
                bytes.AddRange(BigInteger.ModPow(inblocks[i], d, n).ToByteArray());

            return bytes.ToArray();
        }

        public static byte[] Test(byte[] inbytes, BigInteger e, BigInteger d, BigInteger n, int blockSize)
        {
            var step = blockSize / 8; //1024 / 8 = 128

            var inblocks = ToIntegersArray(inbytes, step, true);

            var resultBytes = new List<byte>();


            for (int i = 0; i < inblocks.Length; i++)
            {
                var buffer = new byte[step + 1];
                var newval = BigInteger.ModPow(inblocks[i], e, n);
                //var newval = inblocks[i];
                var byteArray = newval.ToByteArray();

                Array.Copy(byteArray, buffer, byteArray.Length > step + 1 ? step + 1 : byteArray.Length);

                resultBytes.AddRange(buffer);
            }



            var resbytes = new List<byte>();
            var newinblocks = ToIntegersArray(resultBytes.ToArray(), step + 1, false);
            for (int i = 0; i < newinblocks.Length; i++)
            {
                var buffer = new byte[step];
                newinblocks[i] = BigInteger.ModPow(newinblocks[i], d, n);
                var byteArray = newinblocks[i].ToByteArray();

                Array.Copy(byteArray, buffer, byteArray.Length < step ? byteArray.Length : step);

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
