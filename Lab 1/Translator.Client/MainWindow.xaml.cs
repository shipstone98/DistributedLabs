using System;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Windows;
using System.Windows.Input;

namespace Translator.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const String URI = "tcp://localhost:5002/Translate";

        private readonly ITranslator Translator;

        public MainWindow()
        {
            this.InitializeComponent();
            this.InputTextBox.Focus();

            try
            {
                TcpChannel channel = new TcpChannel();
                ChannelServices.RegisterChannel(channel, false);
                this.Translator = (ITranslator) Activator.GetObject(typeof (ITranslator), MainWindow.URI);
            }

            catch
            {
                MessageBox.Show("The remote host could not be reached.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Translator = null;
            }
        }

        private void InputTextBox_KeyDown(Object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.TranslateButton_Click(this.TranslateButton, new RoutedEventArgs());
                e.Handled = true;
            }
        }

        private void TranslateButton_Click(Object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.InputTextBox.Text))
            {
                MessageBox.Show("You must enter a string to be translated.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.OutputTextBlock.Text = this.Translator.Translate(this.InputTextBox.Text);
        }
    }
}
