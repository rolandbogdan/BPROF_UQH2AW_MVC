using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PcStoreWpfClient.UI
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            UserName = tb_username.Text;
            Password = tb_pass.Password;
            this.DialogResult = true;
        }

        private void Register_Button_click(object sender, RoutedEventArgs e)
        {
            RestService restService = new RestService("https://localhost:7766/", "/Auth");
            restService.Post<RegisterViewModel>(new RegisterViewModel()
            {
                Email = tb_username.Text,
                Password = tb_pass.Password
            });

            MessageBox.Show("You can log in now.");
        }
    }
}
