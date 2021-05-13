using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient
{
    class RestService
    {
        HttpClient client;
        string endpoint;
        public RestService(string baseurl, string endpoint, string token = "")
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseurl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue
                ("application/json"));

            if (token != "")
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            this.endpoint = endpoint;
        }

        public async Task<List<T>> Get<T>()
        {
            List<T> items = new List<T>();
            HttpResponseMessage response = await
                client.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                items = await response.Content.ReadAsAsync<List<T>>();
            }
            return items;
        }

        public async Task<T> Get<T, K>(K id)
        {
            T item = default(T);
            HttpResponseMessage response = await
                client.GetAsync(endpoint + "/" + id.ToString());
            if (response.IsSuccessStatusCode)
            {
                item = await response.Content.ReadAsAsync<T>();
            }
            return item;
        }

        public async Task<R> Post<R, T>(T item)
        {
            HttpResponseMessage response =
                await client.PostAsJsonAsync(endpoint, item);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<R>();
        }

        public async Task<HttpResponseHeaders> PostAndHeaders<T>(T item)
        {
            HttpResponseMessage response =
                await client.PostAsJsonAsync(endpoint, item);

            response.EnsureSuccessStatusCode();
            return response.Headers;
        }

        public async void Post<T>(T item)
        {
            HttpResponseMessage response =
                await client.PostAsJsonAsync(endpoint, item);

            response.EnsureSuccessStatusCode();
        }

        public async Task<R> Delete<R, K>(K id)
        {
            HttpResponseMessage response =
                await client.DeleteAsync(endpoint + "/" + id.ToString());

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<R>();
        }

        public async void Delete<K>(K id)
        {
            HttpResponseMessage response =
                await client.DeleteAsync(endpoint + "/" + id.ToString());

            response.EnsureSuccessStatusCode();
        }

        public async Task<R> Put<R, K, T>(K id, T item)
        {
            HttpResponseMessage response =
                await client.PutAsJsonAsync(endpoint + "/" + id.ToString(), item);


            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<R>();
        }

        public async void Put<K, T>(K id, T item)
        {
            HttpResponseMessage response =
                await client.PutAsJsonAsync(endpoint + "/" + id.ToString(), item);


            response.EnsureSuccessStatusCode();
        }

    }
}
