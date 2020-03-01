using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Translator.Server
{
    internal static class Program
    {
        private const int BufferSize = 6;
        private const ushort Port = 5002;

        private static TcpListener Listener = null;

        private static void Console_CancelKeyPress(Object sender, ConsoleCancelEventArgs e) => Program.Listener.Stop();

        private static void Handle(byte[] rawData, int remainingLength)
        {
            int offset = rawData[0];
            int dataLength = rawData[1] * 256 + rawData[2];
            byte[] byteMessage = new byte[remainingLength];
            Array.Copy(rawData, 3, byteMessage, 0, dataLength);
            String message = Encoding.ASCII.GetString(rawData, 3, dataLength);
            Console.WriteLine($"Encryption key: {offset}");
            Console.WriteLine($"Data length: {dataLength}");
            Console.WriteLine($"Received message: {message}");

            for (int i = 0; i < message.Length; i ++)
            {
                if ((byteMessage[i] = (byte) ((int) byteMessage[i] + (int) offset)) > 126)
                {
                    byteMessage[i] = (byte) ((int) byteMessage[i] - 126 + 31);
                }
            }

            String encryptedMessage = Encoding.ASCII.GetString(byteMessage, 0, dataLength);
            Console.WriteLine($"Encrypted message: {encryptedMessage}");
        }

        private async static Task<int> Main(String[] args)
        {
            Program.Listener = new TcpListener(IPAddress.Any, Program.Port);
            Program.Listener.Start();
            Console.WriteLine($"Server now listening for all connections on port {Program.Port}");
            Console.CancelKeyPress += new ConsoleCancelEventHandler(Program.Console_CancelKeyPress);

            do
            {
                byte[] rawData;
                int remainingLength;

                try
                {
                    using (TcpClient client = Program.Listener.AcceptTcpClient())
                    {
                        NetworkStream ns = client.GetStream();
                        byte[] header = new byte[Program.BufferSize];
                        await ns.ReadAsync(header, 0, Program.BufferSize);
                        int messageType = header[3];
                        remainingLength = header[4] * 256 + header[5];
                        Console.WriteLine($"Message type: {messageType}");
                        Console.WriteLine($"Length: {remainingLength}");
                        rawData = new byte[remainingLength];
                        await ns.ReadAsync(rawData, 0, remainingLength);
                        ns.Close();
                    }
                }

                catch
                {
                    break;
                }

                Thread t = new Thread(() => Program.Handle(rawData, remainingLength));
                t.Start();
            } while (true);

            Console.WriteLine("Server shutting down");
            return 0;
        }
    }
}