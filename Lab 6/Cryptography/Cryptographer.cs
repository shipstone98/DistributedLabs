using System;
using System.Security.Cryptography;
using System.Text;

namespace Cryptography
{
    public static class Cryptographer
    {
        public static String ByteArrayToHexString(byte[] byteArray)
        {
            StringBuilder sb = new StringBuilder();

            if (!(byteArray is null && byteArray.Length == 0))
            {
                foreach (byte b in byteArray)
                {
                    sb.Append(b.ToString("x2"));
                }
            }

            return sb.ToString();
        }

        public static byte[] RSADecrypt(byte[] dataToDecrypt, RSAParameters rsaKeyInfo)
        {
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.ImportParameters(rsaKeyInfo);
                    return rsa.Decrypt(dataToDecrypt, false);
                }
            }

            catch (CryptographicException)
            {
                Console.WriteLine("ERROR: couldn't decrypt data");
                return null;
            }
        }

        public static byte[] RSAEncrypt(byte[] dataToEncrypt, RSAParameters rsaKeyInfo)
        {
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.ImportParameters(rsaKeyInfo);
                    return rsa.Encrypt(dataToEncrypt, false);
                }
            }

            catch (CryptographicException)
            {
                Console.WriteLine("ERROR: couldn't encrypt data");
                return null;
            }
        }

        public static byte[] SHA1Encrypt(byte[] dataToEncrypt)
        {
            using (SHA1 sha1Provider = new SHA1CryptoServiceProvider())
            {
                return sha1Provider.ComputeHash(dataToEncrypt);
            }
        }
    }
}
