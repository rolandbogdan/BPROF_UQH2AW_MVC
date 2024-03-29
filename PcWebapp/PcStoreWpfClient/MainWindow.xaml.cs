﻿namespace PcStoreWpfClient
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
            token = string.Empty;
            this.InitializeComponent();
            this.Login();
            if (token == string.Empty)
            {
                this.Close();
            }
        }

        public MainWindow(string recievedToken)
        {
            this.InitializeComponent();
            this.token = recievedToken;
            this.GetCustomerNames();
            this.RefreshProductList();
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
            RestService restService = new RestService("https://localhost:5001/", "/Customer", token);
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
            RestService restService = new RestService("https://localhost:5001/", "/Product", token);
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
                RestService restService = new RestService("https://localhost:5001/", "/Product", token);

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
                RestService restService = new RestService("https://localhost:5001/", "/Product", token);
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

        private async void EditProduct_ButtonClick(object sender, RoutedEventArgs e)
        {
            if (DGrid1.SelectedItem as Product != null)
            {
                EditingWindow ew = new EditingWindow(DGrid1.SelectedItem as Product);
                if (ew.ShowDialog() == true)
                {
                    RestService restService = new RestService("https://localhost:5001/", "/Product", token);

                    if (ew.Product.ProductID == null || ew.Product.ProductID == string.Empty)
                        ew.Product.ProductID = Guid.NewGuid().ToString();

                    restService.Put<string, Product>((DGrid1.SelectedItem as Product).ProductID, ew.Product);
                    MessageBox.Show("Product updated in the database");

                    await this.GetCustomerNames();
                    await this.RefreshProductList();
                }
                else
                {
                    MessageBox.Show("Modifying selected product was not successful");
                }
            }
            else
            {
                MessageBox.Show("Could not modify selected product.");
            }
        }

        private async void AddNewCustomer_ButtonClick(object sender, RoutedEventArgs e)
        {
            CustomerEditingWindow ew = new CustomerEditingWindow();
            if (ew.ShowDialog() == true)
            {
                RestService restService = new RestService("https://localhost:5001/", "/Customer", token);

                if (ew.Customer.CustomerID == null || ew.Customer.CustomerID == string.Empty)
                    ew.Customer.CustomerID = Guid.NewGuid().ToString();

                restService.Post<Customer>(ew.Customer);
                MessageBox.Show("Customer added to database");

                await this.GetCustomerNames();
                await this.RefreshProductList();
            }
            else
            {
                MessageBox.Show("Adding new customer was not successful");
            }
        }

        private async void EditCustomer_ButtonClick(object sender, RoutedEventArgs e)
        {
            if (CustomerCbox.SelectedItem as Customer != null)
            {
                CustomerEditingWindow ew = new CustomerEditingWindow(CustomerCbox.SelectedItem as Customer);
                if (ew.ShowDialog() == true)
                {
                    RestService restService = new RestService("https://localhost:5001/", "/Customer", token);

                    if (ew.Customer.CustomerID == null || ew.Customer.CustomerID == string.Empty)
                        ew.Customer.CustomerID = Guid.NewGuid().ToString();

                    restService.Put<string, Customer>((CustomerCbox.SelectedItem as Customer).CustomerID, ew.Customer);
                    MessageBox.Show("Customer updated in the database");

                    await this.GetCustomerNames();
                    await this.RefreshProductList();
                }
                else
                {
                    MessageBox.Show("Modifying selected customer was not successful");
                }
            }
            else
            {
                MessageBox.Show("Could not modify selected customer.");
            }
        }

        private async void DeleteCustomer_ButtonClick(object sender, RoutedEventArgs e)
        {
            if (CustomerCbox.SelectedItem as Customer != null)
            {
                RestService restService = new RestService("https://localhost:5001/", "/Customer", token);
                restService.Delete<string>((CustomerCbox.SelectedItem as Customer).CustomerID);
                MessageBox.Show("Customer successfully deleted");
                await this.GetCustomerNames();
                await this.RefreshProductList();
            }
            else
            {
                MessageBox.Show("Could not delete customer");
            }
        }

        private void SignOut_ButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow newMW = new MainWindow();
            this.Close();
            newMW.Show();
        }
    }
}
