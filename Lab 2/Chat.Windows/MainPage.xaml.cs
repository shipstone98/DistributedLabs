using Microsoft.AspNetCore.SignalR;
using System;
using Windows.UI.Popups;
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
        public MainPage()
        {
            this.InitializeComponent();
            this.UsernameTextBox.Focus(FocusState.Keyboard);
        }

        private void MessageTextBox_KeyDown(Object sender, KeyRoutedEventArgs e)
        {
            this.SendButton_ClickAsync(this.SendButton, new RoutedEventArgs());
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
        }

        private void UsernameTextBox_KeyDown(Object sender, KeyRoutedEventArgs e)
        {
            this.MessageTextBox.Focus(FocusState.Keyboard);
        }
    }
}
