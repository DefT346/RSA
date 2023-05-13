using RSA;
using System.Numerics;

namespace RSAEDS
{
    internal class EDSignature
    {
        public byte[] encHash { get; private set; }
        public BigInteger openKey { get; private set; }
        public BigInteger n { get; private set; }

        public EDSignature(byte[] document, int blocksize)
        {
            var hashedDocument = ComputeSHA512(document);

            var keys = new RSAKeys(1024);

            encHash = RSAProvider.Encrypt(keys.d, keys.n, hashedDocument, blocksize); // шифрование хеша закрытым ключом
            openKey = keys.e;
            n = keys.n;
        }

        public static byte[] ComputeSHA512(byte[] data)
        {
            using (System.Security.Cryptography.SHA512 shaM = System.Security.Cryptography.SHA512.Create())
                return shaM.ComputeHash(data);
        }
    }
}
