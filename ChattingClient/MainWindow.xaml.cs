using ChattingInterfaces;
using ChattingServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChattingClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static IChattingService server;
        private static DuplexChannelFactory<IChattingService> _channelFactory;
        public MainWindow()
        {
            InitializeComponent();
            _channelFactory = new DuplexChannelFactory<IChattingService>(new ClientCallback(), "ChattingServiceEndpoint");
            server = _channelFactory.CreateChannel();
        }


      public  void takeMessage(string message,string userName)
        {
            MessageDisplayTxtBox.Text += userName + " :" + message + "\n";
        }

        private void MessageDisplayTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SendBTN_Click(object sender, RoutedEventArgs e)
        {
            server.messageToAll(MessageTxtBox.Text, LoginTxtBx.Text);
            takeMessage(MessageTxtBox.Text, LoginTxtBx.Text);
            MessageTxtBox.Text = "";
        }

        private void LoginBTN_Click(object sender, RoutedEventArgs e)
        {
            int returnValue = server.Login(LoginTxtBx.Text);
            if (returnValue == 1)
                MessageBox.Show("you already Logged in");

            else if (returnValue == 0)
                MessageBox.Show("you logged in ");
            LoginTxtBx.IsEnabled = false;
            LoginBTN.IsEnabled = false;


            LoadUserList(server.getCurrentUsers());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            server.logout();
        }

        public void addUser(string userName)
        {
            if (UserListBox.Items.Contains(userName))
            {
                return;
            }

            UserListBox.Items.Add(userName);
        }

        public void removeUser(string userName)
        {
            if (UserListBox.Items.Contains(userName))
            {
                UserListBox.Items.Remove(userName);
            }
        }

        private void LoadUserList(List<string> users)
        {
            foreach(var user in users)
            {
                addUser(user);
            }
        }
    }
}
