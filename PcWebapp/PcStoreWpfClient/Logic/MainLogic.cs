namespace PcStoreWpfClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using GalaSoft.MvvmLight.Messaging;
    using Models;

    public class MainLogic : IMainLogic
    {
        private string url = "https://localhost:7766/";
        private HttpClient client = new HttpClient();
        private JsonSerializerOptions jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

        public void SendMessage(bool success)
        {
            string msg = success ? "Operation completed successfully" : "Operation failed successfully";
            Messenger.Default.Send(msg, "ProductResult");
        }

        public List<Product> ApiGetProducts()
        {
            RestService restService = new RestService(url, "/Product");
            List<Product> outp = restService.Get<Product>().Result;
            return outp;
        }

        public void ApiDelProducts(Product product)
        {
            bool success = false;
            if (product != null)
            {
                RestService restService = new RestService(url, "/Product");
                restService.Delete<string>(product.ProductID);
                success = true; // todo
            }

            this.SendMessage(success);
        }

        public void ApiEditProduct(Product product)
        {
            RestService restService = new RestService(url, "/Product");
            restService.Post<Product>(product);
        }

        public async void ApiAddProduct(Product product)
        {
            RestService restService = new RestService(url, "/Product");
            await restService.Put<Product, Product>(product);
        }

        #region Old
        /*
        public void EditProduct(Product product, Func<Product, bool> editorFunc)
        {
            Product clone = new Product() { ProductID = Guid.NewGuid().ToString() };

            if (product != null)
            {
                clone.CopyFrom(product);
            }

            bool? success = editorFunc?.Invoke(clone);

            if (success == true)
            {
                if (product != null)
                {
                    success = this.ApiEditProduct(clone, true);
                }
                else
                {
                    success = this.ApiEditProduct(clone, false);
                }
            }

            this.SendMessage(success == true);
        }

        private bool ApiEditProduct(Product p, bool isEditing)
        {
            if (p == null || p.ProductID == null)
            {
                return false;
            }

            string myUrl = this.url + (isEditing ? "modproduct" : "addproduct");

            Dictionary<string, string> postData = new Dictionary<string, string>();
            if (isEditing)
            {
                postData.Add("productID", p.ProductID);
            }

            postData.Add("productName", p.ProductName);
            postData.Add("category", p.Category.ToString());
            postData.Add("manufacturer", p.Manufacturer);
            postData.Add("price", p.Price.ToString());
            postData.Add("inStock", p.InStock.ToString());
            postData.Add("quantity", p.Quantity.ToString());
            postData.Add("description", p.Description);

            string json = this.client.PostAsync(
                myUrl,
                new FormUrlEncodedContent(postData))
                .Result.Content.ReadAsStringAsync().Result;

            JsonDocument doc = JsonDocument.Parse(json);
            return doc.RootElement.EnumerateObject().First().Value.GetRawText() == "true";
        }
        */
        #endregion
    }
}
