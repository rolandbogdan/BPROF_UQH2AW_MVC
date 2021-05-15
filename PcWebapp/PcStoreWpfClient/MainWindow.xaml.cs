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
                RestService restService = new RestService("https://localhost:7766/", "/Auth");

                TokenViewModel tvm = await restService.Put<TokenViewModel, LoginViewModel>(new LoginViewModel()
                {
                    Username = lw.UserName,
                    Password = lw.Password
                });
                token = tvm.Token;

                await this.GetCustomerNames();
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
            {
                await this.RefreshProductList();
            }
            else
            {
                this.RefreshProductList(CustomerCbox.SelectedItem as Customer);
            }
        }

        private void CustomerCbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Customer customer = CustomerCbox.SelectedItem as Customer;
            RefreshProductList(customer);
        }

        private async Task GetCustomerNames()
        {
            CustomerCbox.ItemsSource = null;
            RestService restService = new RestService("https://localhost:7766/", "/Customer", token);
            IEnumerable<Customer> customernames = await restService.Get<Customer>();

            CustomerCbox.ItemsSource = customernames;
        }

        private void ClearSelection_ButtonClick(object sender, RoutedEventArgs e)
        {
            CustomerCbox.SelectedIndex = -1;
        }

        private async void AddNewProduct_ButtonClick(object sender, RoutedEventArgs e)
        {
            EditingWindow ew = new EditingWindow();
            if (ew.ShowDialog() == true)
            {
                // adatbázisba post kérés új productról
                RestService restService = new RestService("https://localhost:7766/", "/Product", token);
                if (ew.Product.ProductID == null || ew.Product.ProductID == string.Empty)
                {
                    ew.Product.ProductID = Guid.NewGuid().ToString();
                }
                restService.Post<Product>(ew.Product);
                MessageBox.Show("Product added to database");
                // hozzáadni a customerhez
                if (CustomerCbox.SelectedItem as Customer != null)
                {
                    restService = new RestService("https://localhost:7766/", "/Customer", token);
                    Customer updatedCustomer = (CustomerCbox.SelectedItem as Customer);
                    updatedCustomer.Products.Add(ew.Product);
                    restService.Put<string, Customer>(
                        (CustomerCbox.SelectedItem as Customer).CustomerID,
                        updatedCustomer);

                    // todo maybe
                    //restService = new RestService("https://localhost:7766/", "/Product", token);
                    //ew.Product.Customer = updatedCustomer;
                    //Product updatedProduct = ew.Product;
                    //updatedProduct.Customer = updatedCustomer;
                    //updatedProduct.CustomerID = updatedCustomer.CustomerID;
                    //restService.Put<string, Product>(
                    //    updatedProduct.ProductID,
                    //    updatedProduct
                    //    );

                    await RefreshProductList(CustomerCbox.SelectedItem as Customer);
                }
            }
            else
            {
                MessageBox.Show("Adding new product was not successful");
            }
        }
    }
}
