using GalaSoft.MvvmLight;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient
{
    public class EditorViewModel : ViewModelBase
    {
        private Product product;

        public EditorViewModel()
        {
            if (this.IsInDesignMode)
            {
                this.product = new Product()
                {
                    ProductName = "Ryzen 5600X",
                    Price = 120000,
                    Manufacturer = "AMD",
                    Category = ProductCategory.CPU,
                    InStock = true,
                    Quantity = 10,
                    Description = "Fast new CPU"
                };
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
