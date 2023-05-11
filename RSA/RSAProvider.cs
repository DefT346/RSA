using System.Numerics;
using System.Text;

namespace RSA
{
    public class RSAProvider
    {

        public static byte[] Encrypt(BigInteger e, BigInteger n, byte[] bytes, int blockSize)
        {
            var step = blockSize / 8; //1024 / 8 = 128

            var inblocks = ToIntegersArray(bytes, step);

            for (int i = 0; i < inblocks.Length; i++)
                inblocks[i] = BigInteger.ModPow(inblocks[i], e, n);

            var ret = toBytes(inblocks, step + 1);

            return ret;
        }

        public static byte[] Decrypt(BigInteger d, BigInteger n, byte[] encodedData, int blockSize)
        {
            var bytes = new List<byte>();
            var step = blockSize / 8; //1024 / 8 = 128

            var inblocks = toBigIntegers(encodedData, step + 1);
            for (int i = 0; i < inblocks.Length; i++)
                bytes.AddRange(BigInteger.ModPow(inblocks[i], d, n).ToByteArray());

            return bytes.ToArray();
        }


        public static byte[] Test(byte[] bytes, BigInteger e, BigInteger d, BigInteger n, int blockSize)
        {
            var step = blockSize / 8; //1024 / 8 = 128

            var inblocks = ToIntegersArray(bytes, step);

            for (int i = 0; i < inblocks.Length; i++)
                inblocks[i] = BigInteger.ModPow(inblocks[i], e, n);

            //var ret0 = inblocks[0].ToByteArray();
            //var ret1 = inblocks[1].ToByteArray();

            //inblocks[0] = new BigInteger(ret0);
            //inblocks[1] = new BigInteger(ret1);

            //var ret = toBytes(inblocks, step + 1);
            //inblocks = toBigIntegers(ret, step + 1);

            for (int i = 0; i < inblocks.Length; i++)
                inblocks[i] = BigInteger.ModPow(inblocks[i], d, n);

            ////var ret = toBytes(inblocks);
            ////var encblocks = toBigIntegers(ret, blockSize);
            //var encblocks = inblocks;

            //var decbytes = new List<byte>();

            //for (int i = 0; i < encblocks.Length; i++)
            //    decbytes.AddRange(BigInteger.ModPow(encblocks[i], d, n).ToByteArray());

            return new byte[1];
        }

        //public static byte[] Encrypt(BigInteger e, BigInteger n, byte[] bytes, int blockSize)
        //{
        //    return ModifyBytes(bytes, blockSize, e, n);
        //}

        //public static byte[] Decrypt(BigInteger d, BigInteger n, byte[] bytes, int blockSize)
        //{
        //    return ModifyBytes(bytes, blockSize, d, n);
        //}
        //BigInteger[] -> byte[]
        //byte[] -> BigInteger[]


        public static byte[] toBytes(BigInteger[] bigIntegers, int step)
        {
            var bytes = new List<byte>();

            for (int i = 0; i < bigIntegers.Length; i++)
            {
                var buffer = new byte[step];
                var ba = bigIntegers[i].ToByteArray();

                Array.Copy(ba, buffer, ba.Length > step ? step : ba.Length);

                bytes.AddRange(buffer);
            }

            return bytes.ToArray();
        }

        public static BigInteger[] toBigIntegers(byte[] bytes, int blockSize)
        {
            return ToIntegersArray(bytes, blockSize);
        }

        //private static byte[] ModifyBytes(byte[] data, int blockSize, BigInteger key, BigInteger modulus)
        //{
        //    var blocks = ToIntegersArray(data, blockSize);
        //    var bytes = new List<byte>();

        //    for (int i = 0; i < blocks.Length; i++) {
        //        var encVal = BigInteger.ModPow(blocks[i], key, modulus);
        //        //var encVal = blocks[i];
        //        var b = encVal.ToByteArray();
        //        bytes.AddRange(b);
        //    }

        //    return bytes.ToArray();
        //}

        private static BigInteger[] ToIntegersArray(byte[] bytes, int block/*int bitblockSize*/)
        {
            //var blockSize = bitblockSize; //bits count

            List<BigInteger> blocks = new List<BigInteger>();



            //var step = blockSize / 8; //1024 / 8 = 128
            var step = block;
            var cursor = 0;
            while (true)
            {
                
                var blockBuffer = new byte[step];
                for (int i = 0; i < step /*128*/; i++) //цикл записывающий 1024 битное число
                {
                    if (cursor + i >= bytes.Length) break;

                    blockBuffer[i] = bytes[cursor + i];
                }
                blocks.Add(new BigInteger(blockBuffer));
                cursor += step;
                if (cursor >= bytes.Length) break;
            }

            //for (int i = 0; i < bytes.Length; i++)
            //{
            //    blockBuffer.Add(bytes[i]);
            //    if (i != 0) 
            //    if (i % (blockSize / 8) == 0 || i == bytes.Length - 1)
            //    {
            //        blocks.Add(new BigInteger(blockBuffer.ToArray()));
            //        blockBuffer.Clear();
            //    }
            //}

            return blocks.ToArray();
        }
    }
}
