using RSAEDS;
using RSA;

var document = await FileDialog.ReadFile(FileDialog.ShowDialog());

var hashedDocument = ComputeSHA512(document);

var keys = new RSAKeys(2048);

RSAProvider.Encrypt(keys.e, keys.n, hashedDocument);

byte[] ComputeSHA512(byte[] data)
{
    using (System.Security.Cryptography.SHA512 shaM = System.Security.Cryptography.SHA512.Create())
        return shaM.ComputeHash(data);
}