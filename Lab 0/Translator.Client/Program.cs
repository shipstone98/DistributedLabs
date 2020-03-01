using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Translator.Client
{
    internal static class Program
    {
        private const int EncryptionKey = 1;
        private const String Hostname = "localhost";
        private const ushort Port = 5002;

        private async static Task<int> Main(String[] args)
        {
            Console.Write("Please enter the message: ");
            String message = Console.ReadLine();
            byte[] rawData = Program.Serialize(message);
            
            using (TcpClient client = new TcpClient())
            {
                client.Connect(Program.Hostname, Program.Port);
                NetworkStream ns = client.GetStream();
                await ns.WriteAsync(rawData, 0, rawData.Length);
                await ns.FlushAsync();
                ns.Close();
            }

            Console.Write("Press any key to quit...");
            Console.ReadKey(true);
            Console.WriteLine();
            return 0;
        }

        private static byte[] Serialize(String message)
        {
            byte[] ascii = Encoding.ASCII.GetBytes(message), rawData = new byte[ascii.Length + 9];
            int index = 0;
            rawData[index ++] = 0;
            rawData[index ++] = 0;
            rawData[index ++] = 0;
            rawData[index ++] = 1;
            int remainingLength = ascii.Length + 3;
            rawData[index ++] = (byte) ((remainingLength & 0xFF00) >> 8);
            rawData[index ++] = (byte) (remainingLength & 0xFF);
            rawData[index ++] = (byte) Program.EncryptionKey;
            rawData[index ++] = (byte) ((ascii.Length & 0xFF00) >> 8);
            rawData[index ++] = (byte) (ascii.Length & 0xFF);
            Array.Copy(ascii, 0, rawData, index, ascii.Length);
            return rawData;
        }
    }
}
