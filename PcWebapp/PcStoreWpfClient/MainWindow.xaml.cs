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
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<string>(this, "ProductResult", msg =>
            {
                (this.DataContext as MainVM).LoadCmd.Execute(null);
                MessageBox.Show(msg);
            });

            (this.DataContext as MainVM).EditorFunc = (productVm) =>
            {
                EditingWindow win = new EditingWindow(productVm);
                return win.ShowDialog() == true;
            };
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Messenger.Default.Unregister(this);
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
            }
            else
            {
                MessageBox.Show("error");
                this.Close();
            }
        }
    }
}
