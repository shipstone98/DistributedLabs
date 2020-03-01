using System;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Windows;

namespace Translator.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const String URI = "tcp://localhost:5002/Translate";

        private readonly Translator.Server.Translator Translator;

        public MainWindow()
        {
            this.InitializeComponent();
            TcpChannel channel = new TcpChannel();
            ChannelServices.RegisterChannel(channel, false);
            this.Translator = (Translator.Server.Translator) Activator.GetObject(typeof (Translator.Server.Translator), MainWindow.URI);
        }
    }
}
