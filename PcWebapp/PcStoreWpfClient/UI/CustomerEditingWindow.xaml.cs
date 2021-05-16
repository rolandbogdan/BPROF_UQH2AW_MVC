using Models;
using PcStoreWpfClient.VM;
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
    /// Interaction logic for CustomerEditingWindow.xaml
    /// </summary>
    public partial class CustomerEditingWindow : Window
    {
        private CustomerEditorVM vm;
        public CustomerEditingWindow()
        {
            InitializeComponent();
            this.vm = this.FindResource("VM") as CustomerEditorVM;
        }

        public CustomerEditingWindow(Customer oldCustomer) : this()
        {
            this.vm.Customer = oldCustomer;
        }

        public Customer Customer { get => this.vm.Customer; }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
