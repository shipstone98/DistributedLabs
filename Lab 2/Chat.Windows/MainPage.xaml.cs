using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Chat.Windows
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const String Hostname = "localhost";
        private const ushort Port = 44333;

        private bool Connected;
        private readonly HubConnection Connection;

        public MainPage()
        {
            this.InitializeComponent();
            this.UsernameTextBox.Focus(FocusState.Keyboard);
            this.Connected = false;
            this.Connection = new HubConnectionBuilder().WithUrl($"https://{MainPage.Hostname}:{MainPage.Port}/ChatHub").Build();

            this.Connection.On<String, String, DateTime>("GetMessage",
                new Action<String, String, DateTime>((username, message, timestamp) =>
                    this.GetMessage(username, message, timestamp)));

            this.Connection.Closed += new Func<Exception, Task>(this.Connection_ClosedAsync);
        }

        private async Task ConnectAsync()
        {
            if (this.Connected)
            {
                return;
            }

            try
            {
                await this.Connection.StartAsync();
                this.Connected = true;
            }

            catch
            {
                MessageDialog md = new MessageDialog("Could not connect to host.", "Error!");
                await md.ShowAsync();
                this.Connected = false;
            }
        }

        private async Task Connection_ClosedAsync(Exception ex) => this.Connected = false;

        private async void GetMessage(String username, String message, DateTime timestamp)
        {
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.High,
                () =>
                {
                    String chat = $"{timestamp}\t{username}: {message}";
                    this.MessageListBox.Items.Add(chat);
                });
        }

        private void MessageTextBox_KeyDown(Object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                this.SendButton_ClickAsync(this.SendButton, new RoutedEventArgs());
                e.Handled = true;
            }
        }

        private async void SendButton_ClickAsync(Object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.UsernameTextBox.Text))
            {
                MessageDialog md = new MessageDialog("You must enter your username.", "Error!");
                await md.ShowAsync();
            }

            if (String.IsNullOrWhiteSpace(this.MessageTextBox.Text))
            {
                MessageDialog md = new MessageDialog("You must enter a message.", "Error!");
                await md.ShowAsync();
            }

            await this.ConnectAsync();

            if (!this.Connected)
            {
                return;
            }

            try
            {
                await this.Connection.InvokeAsync("BroadcastMessage", this.UsernameTextBox.Text, this.MessageTextBox.Text);
                this.MessageTextBox.Text = "";
            }

            catch
            {
                MessageDialog md = new MessageDialog("An unknown error occurred.", "Error!");
                await md.ShowAsync();
            }
        }

        private void UsernameTextBox_KeyDown(Object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                this.MessageTextBox.Focus(FocusState.Keyboard);
                e.Handled = true;
            }
        }
    }
}
