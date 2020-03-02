using System;
using System.Security.Cryptography;
using System.Text;

namespace Cryptography.UI
{
    internal static class Program
    {
        private static String ByteArrayToHexString(byte[] byteArray)
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

        private static int Main(String[] args)
        {
            const String MESSAGE = "Hello World!";
            byte[] asciiByteMessage = Encoding.ASCII.GetBytes(MESSAGE);
            SHA1 sha1Provider = new SHA1CryptoServiceProvider();
            byte[] sha1Message = sha1Provider.ComputeHash(asciiByteMessage);
            Console.WriteLine($"Original message: {MESSAGE}");
            Console.WriteLine($"Encrypted message using SHA1: {Program.ByteArrayToHexString(sha1Message)}");
            byte[] encryptedByteMessage, decryptedByteMessage;

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                encryptedByteMessage = Program.RSAEncrypt(asciiByteMessage, rsa.ExportParameters(false));
                decryptedByteMessage = Program.RSADecrypt(encryptedByteMessage, rsa.ExportParameters(true));
            }
            
            Console.WriteLine($"Encrypted message using RSA: {Program.ByteArrayToHexString(encryptedByteMessage)}");
            Console.WriteLine($"Decrypted message using RSA: {Encoding.ASCII.GetString(decryptedByteMessage)}");

            if (!Console.IsInputRedirected)
            {
                Console.Write("Press any key to quit...");
                Console.ReadKey(true);
                Console.WriteLine();
            }

            return 0;
        }

        private static byte[] RSADecrypt(byte[] dataToDecrypt, RSAParameters rsaKeyInfo)
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

        private static byte[] RSAEncrypt(byte[] dataToDecrypt, RSAParameters rsaKeyInfo)
        {
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.ImportParameters(rsaKeyInfo);
                    return rsa.Encrypt(dataToDecrypt, false);
                }
            }

            catch (CryptographicException)
            {
                Console.WriteLine("ERROR: couldn't encrypt data");
                return null;
            }
        }
    }
}