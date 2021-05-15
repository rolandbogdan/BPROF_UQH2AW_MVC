namespace PcStoreWpfClient
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using CommonServiceLocator;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using Models;

    public class MainVM : ViewModelBase
    {
        private IMainLogic logic;
        private Product selectedProduct;
        private IList<Product> allProducts;

        public MainVM(IMainLogic logic)
        {
            RestService restService = new RestService("https://localhost:7766/", "/Product");
            this.logic = logic;

            this.LoadCmd = new RelayCommand(
                () => new List<Product>(
                this.logic.ApiGetProducts()));

            this.DelCmd = new RelayCommand(
                () =>
                this.logic.ApiDelProducts(this.selectedProduct));

            this.AddCmd = new RelayCommand(
                () =>
                this.logic.ApiAddProduct(new Product())); // TODO

            this.ModCmd = new RelayCommand(
                () =>
                this.logic.ApiEditProduct(new Product())); //TODO
        }

        public MainVM()
            : this(IsInDesignModeStatic ? null : ServiceLocator.Current.GetInstance<IMainLogic>())
        {
        }

        public Product SelectedProduct
        {
            get { return this.selectedProduct; }
            set { this.Set(ref this.selectedProduct, value); }
        }

        public IList<Product> AllProducts
        {
            get { return this.allProducts; }
            set { this.Set(ref this.allProducts, value); }
        }

        public Func<Product, bool> EditorFunc { get; set; }

        public ICommand AddCmd { get; private set; }

        public ICommand DelCmd { get; private set; }

        public ICommand ModCmd { get; private set; }

        public ICommand LoadCmd { get; private set; }
    }
}
