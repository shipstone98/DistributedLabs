using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace Translator.Server
{
    internal static class Program
    {
        private const ushort Port = 5002;

        private static int Main(String[] args)
        {
            try
            {
                TcpChannel channel = new TcpChannel(Program.Port);
                ChannelServices.RegisterChannel(channel, false);
                RemotingConfiguration.RegisterWellKnownServiceType(typeof (Translator), "Translate", WellKnownObjectMode.SingleCall);
                Console.WriteLine("Channel service is now registered");
            }

            catch
            {
                Console.WriteLine("ERROR: couldn't bind service");
                return -1;
            }

            Console.Write("Press any key to exit...");
            Console.ReadKey(true);
            Console.WriteLine();
            return 0;
        }
    }
}
