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

        private async Task Login()
        {
            LoginWindow lw = new LoginWindow();
            if (lw.ShowDialog() == true)
            {
                token = lw.Token;
                await this.GetCustomerNames();
                await this.RefreshProductList();
            }
        }

        private async Task GetCustomerNames()
        {
            CustomerCbox.ItemsSource = null;
            RestService restService = new RestService("https://localhost:7766/", "/Customer", token);
            IEnumerable<Customer> customernames = await restService.Get<Customer>();

            CustomerCbox.ItemsSource = customernames;
        }

        private async void CustomerCbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Customer customer = CustomerCbox.SelectedItem as Customer;
            await RefreshProductList(customer);
        }

        private void ClearSelection_ButtonClick(object sender, RoutedEventArgs e)
        {
            CustomerCbox.SelectedIndex = -1;
        }

        private async Task RefreshProductList()
        {
            DGrid1.ItemsSource = null;
            RestService restService = new RestService("https://localhost:7766/", "/Product", token);
            IEnumerable<Product> productList = await restService.Get<Product>();
            DGrid1.ItemsSource = productList;
        }

        private async Task RefreshProductList(Customer c)
        {
            if (c != null)
            {
                DGrid1.ItemsSource = null;
                IEnumerable<Product> productList = c.Products;
                DGrid1.ItemsSource = productList;
            }
            else
            {
                await RefreshProductList();
            }
        }

        private async void Refresh_Button_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerCbox.SelectedItem as Customer == null)
                await this.RefreshProductList();
            else
                await this.RefreshProductList(CustomerCbox.SelectedItem as Customer);
        }

        private async void AddNewProduct_ButtonClick(object sender, RoutedEventArgs e)
        {
            EditingWindow ew = new EditingWindow();
            if (ew.ShowDialog() == true)
            {
                RestService restService = new RestService("https://localhost:7766/", "/Product", token);

                if (ew.Product.ProductID == null || ew.Product.ProductID == string.Empty)
                    ew.Product.ProductID = Guid.NewGuid().ToString();
                if (CustomerCbox.SelectedItem as Customer != null)
                    ew.Product.CustomerID = (CustomerCbox.SelectedItem as Customer).CustomerID;

                restService.Post<Product>(ew.Product);
                MessageBox.Show("Product added to database");

                await this.GetCustomerNames();
                await this.RefreshProductList();
            }
            else
            {
                MessageBox.Show("Adding new product was not successful");
            }
        }

        private async void DeleteProduct_ButtonClick(object sender, RoutedEventArgs e)
        {
            if (DGrid1.SelectedItem as Product != null)
            {
                RestService restService = new RestService("https://localhost:7766/", "/Product", token);
                restService.Delete<string>((DGrid1.SelectedItem as Product).ProductID);
                MessageBox.Show("Product successfully deleted");
                await this.GetCustomerNames();
                await this.RefreshProductList();
            }
            else
            {
                MessageBox.Show("Could not delete product");
            }
        }

        private void EditProduct_ButtonClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
