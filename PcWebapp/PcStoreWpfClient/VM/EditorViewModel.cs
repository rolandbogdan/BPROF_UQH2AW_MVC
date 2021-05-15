namespace PcStoreWpfClient.VM
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GalaSoft.MvvmLight;
    using Models;

    public class EditorViewModel : ViewModelBase
    {
        private Product product;

        public EditorViewModel()
        {
            this.product = new Product();
            if (this.IsInDesignMode)
            {
                this.Product.ProductName = "Ryzen 5600X";
                this.Product.Price = 120000;
                this.Product.Manufacturer = "AMD";
                this.Product.Category = ProductCategory.CPU;
                this.Product.InStock = true;
                this.Product.Description = "Fast new cpu";
            }
        }

        public Array Categories
        {
            get { return Enum.GetValues(typeof(ProductCategory)); }
        }

        public Product Product
        {
            get { return this.product; }
            set { this.Set(ref this.product, value); }
        }
    }
}
