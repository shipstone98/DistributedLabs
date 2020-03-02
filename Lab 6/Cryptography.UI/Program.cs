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
            Console.WriteLine($"Decrypted message: {MESSAGE}");
            Console.WriteLine($"Encrypted message: {Program.ByteArrayToHexString(sha1Message)}");
            
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