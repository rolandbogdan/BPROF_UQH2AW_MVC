﻿namespace PcStoreWpfClient.UI
{
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

    public partial class EditingWindow : Window
    {
        private EditorViewModel vm;

        public EditingWindow()
        {
            this.InitializeComponent();
            this.vm = this.FindResource("VM") as EditorViewModel;
        }

        public EditingWindow(Product oldProduct)
            : this()
        {
            this.vm.Product = oldProduct;
        }

        public Product Product { get => this.vm.Product; }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
