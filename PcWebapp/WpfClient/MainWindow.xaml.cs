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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string token;
        public MainWindow()
        {
            InitializeComponent();
            Login();
        }

        public async Task Login()
        {
            PasswordWindow pw = new PasswordWindow();
            if (pw.ShowDialog() == true)
            {
                RestService restService = new RestService("https://localhost:7766/", "/Auth");

                TokenViewModel tvm = await restService.Put<TokenViewModel, LoginViewModel>(new LoginViewModel()
                {
                    Username = pw.UserName,
                    Password = pw.Password
                });
                token = tvm.Token;
                await GetCustomerNames();
            }
            else
            {
                MessageBox.Show("error");
                this.Close();
            }
        }

        public async Task GetCustomerNames()
        {
            cbox1.ItemsSource = null;
            RestService restService = new RestService("https://localhost:7766/", "/Customer", token);
            IEnumerable<Customer> customernames = await restService.Get<Customer>();

            cbox1.ItemsSource = customernames;
            cbox1.SelectedIndex = 0;
        }

        private void cbox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Customer c1 = cbox1.SelectedItem as Customer;
            lbox1.ItemsSource = c1.Products.Select(p => p.ProductName);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Product p = new Product()
            {
                ProductName = prod_name.Text,
                Category = ProductCategory.CPU,
                Manufacturer = "teszt",
                Customer = cbox1.SelectedItem as Customer,
                CustomerID = (cbox1.SelectedItem as Customer).CustomerID
            };
            (cbox1.SelectedItem as Customer).Products.Add(p);
            RestService restservice = new RestService("https://localhost:7766", "/Product", token);
            //restservice.Post<Product>(p);
            //MessageBox.Show("Product added");
        }
    }
}
