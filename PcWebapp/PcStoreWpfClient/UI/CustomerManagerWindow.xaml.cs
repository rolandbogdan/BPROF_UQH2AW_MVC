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
    /// Interaction logic for CustomerManagerWindow.xaml
    /// </summary>
    public partial class CustomerManagerWindow : Window
    {
        public string Token { private get; set; }
        public CustomerManagerWindow()
        {
            InitializeComponent();
            GetCustomerNames();
        }

        private async Task GetCustomerNames()
        {
            DGrid2.ItemsSource = null;
            RestService restService = new RestService("https://pcwebshop.azurewebsites.net/", "/Customer", Token);
            IEnumerable<Customer> customernames = await restService.Get<Customer>();

            DGrid2.ItemsSource = customernames;
        }

        private void AddNewCustomer_ButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void EditCustomer_ButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteCustomer_ButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void RefreshCustomers(object sender, RoutedEventArgs e)
        {

        }

        private void SwitchView_ButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow(this.Token);
            mw.Show();
            this.Close();
        }
    }
}
