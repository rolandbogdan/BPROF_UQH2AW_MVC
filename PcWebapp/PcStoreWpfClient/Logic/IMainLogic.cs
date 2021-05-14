namespace PcStoreWpfClient
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IMainLogic
    {
        public List<Product> ApiGetProducts();

        public void ApiDelProducts(Product product);

        public void ApiEditProduct(Product product);

        public void ApiAddProduct(Product product);
    }
}
