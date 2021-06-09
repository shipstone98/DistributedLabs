using System;
using System.Security.Cryptography;
using System.Text;

namespace Cryptography.UI
{
    internal static class Program
    {
        private static int Main(String[] args)
        {
            const String MESSAGE = "Hello World!";
            byte[] asciiByteMessage = Encoding.ASCII.GetBytes(MESSAGE);
            Console.WriteLine($"Original message: {MESSAGE}");
            byte[] sha1Message = Cryptographer.SHA1Encrypt(asciiByteMessage);
            Console.WriteLine($"Encrypted message using SHA1: {Cryptographer.ByteArrayToHexString(sha1Message)}");
            byte[] encryptedByteMessage, decryptedByteMessage;

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                encryptedByteMessage = Cryptographer.RSAEncrypt(asciiByteMessage, rsa.ExportParameters(false));
                decryptedByteMessage = Cryptographer.RSADecrypt(encryptedByteMessage, rsa.ExportParameters(true));
            }
            
            Console.WriteLine($"Encrypted message using RSA: {Cryptographer.ByteArrayToHexString(encryptedByteMessage)}");
            Console.WriteLine($"Decrypted message using RSA: {Encoding.ASCII.GetString(decryptedByteMessage)}");

            if (!Console.IsInputRedirected)
            {
                Console.Write("Press any key to quit...");
                Console.ReadKey(true);
                Console.WriteLine();
            }

            return 0;
        }
    }
}