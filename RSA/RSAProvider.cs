using System.Numerics;

namespace RSA
{
    public class RSAProvider
    {
        /// <summary>
        /// Шифрование байтовых данных
        /// </summary>
        /// <param name="e">Открытый ключ</param>
        /// <param name="n"></param>
        /// <param name="bytes"></param>
        /// <param name="blockSize">Размер шифруемого блока в байтах</param>
        /// <returns></returns>
        public static byte[] Encrypt(BigInteger e, BigInteger n, byte[] bytes, int blockSize)
        {
            var inblocks = ToIntegersArray(bytes, blockSize, true);

            var resultBytes = new List<byte>();

            for (int i = 0; i < inblocks.Length; i++)
            {
                var newval = BigInteger.ModPow(inblocks[i], e, n);
                var byteArray = newval.ToByteArray();
                WriteInt(byteArray.Length, ref resultBytes);
                resultBytes.AddRange(byteArray);
            }

            return resultBytes.ToArray();
        }

        public static byte[] Decrypt(BigInteger d, BigInteger n, byte[] encBytes, int blockSize)
        {
            var outbytes = new List<byte>();
            for (int i = 0; i < encBytes.Length; i++)
            {
                var length = ReadInt(ref i, ref encBytes); // чтение размера массива байтов перед его чтением

                var buffer = new byte[length]; // чтение числа
                for (int j = 0; j < length; j++)
                    buffer[j] = encBytes[i + j];

                i += length - 1; // смещение

                // Расшифровка
                var num = new BigInteger(buffer);
                var dec = BigInteger.ModPow(num, d, n);
                outbytes.AddRange(Cut(dec, blockSize));
            }

            return outbytes.ToArray();
        }

        private static void WriteInt(int _value, ref List<byte> buffer)
        {
            buffer.AddRange(BitConverter.GetBytes(_value));
        }

        private static int ReadInt(ref int readPos, ref byte[] buffer)
        {
            int _value = BitConverter.ToInt32(buffer, readPos); // Преобразование байтов в int
            readPos += 4; // Прибавим 4 к позиции чтения буфера
            return _value;
        }

        private static byte[] Cut(BigInteger num, int blockSize)
        {
            
            var byteArray = num.ToByteArray();

            var size = byteArray.Length < blockSize ? byteArray.Length : blockSize;

            if (byteArray[byteArray.Length - 1] == 0)
                size -= 1;

            var buffer = new byte[size];



            Array.Copy(byteArray, buffer, size);
            return buffer;
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
