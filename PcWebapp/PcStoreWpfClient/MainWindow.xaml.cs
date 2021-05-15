namespace PcStoreWpfClient
{
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
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using GalaSoft.MvvmLight.Messaging;
    using Models;
    using PcStoreWpfClient.UI;

    public partial class MainWindow : Window
    {
        private string token;
        public MainWindow()
        {
            this.InitializeComponent();
            this.Login();
        }

        public async Task Login()
        {
            LoginWindow lw = new LoginWindow();
            if (lw.ShowDialog() == true)
            {
                RestService restService = new RestService("https://localhost:7766/", "/Auth");

                TokenViewModel tvm = await restService.Put<TokenViewModel, LoginViewModel>(new LoginViewModel()
                {
                    Username = lw.UserName,
                    Password = lw.Password
                });
                token = tvm.Token;

                await this.RefreshProductList();
            }
            else
            {
                MessageBox.Show("error");
                this.Close();
            }
        }

        private async Task RefreshProductList()
        {
            DGrid1.ItemsSource = null;
            RestService restService = new RestService("https://localhost:7766/", "/Product", token);
            IEnumerable<Product> productList = await restService.Get<Product>();
            DGrid1.ItemsSource = productList;
        }

        private async void Refresh_Button_Click(object sender, RoutedEventArgs e)
        {
            await this.RefreshProductList();
        }
    }
}
