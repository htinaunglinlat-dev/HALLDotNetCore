using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleAppHW
{
    public class HttpClientExample
    {
        private readonly HttpClient _client;
        private readonly string _postEndPoint = "https://fakestoreapi.com/products";
        public HttpClientExample()
        {
            _client = new HttpClient();
        }
        public async Task ReadAsync()
        {
            var response = await _client.GetAsync(_postEndPoint);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonStr);
            }
        }
        public async Task EditAsync(int id)
        {
            var response = await _client.GetAsync($"{_postEndPoint}/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No data found.");
                return;
            }
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonStr);
            }
        }
        public async Task CreateAsync(string title, float price, string desc, string category, string image, float rate, int count) 
        {
            ProductModel requestModel = new ProductModel()
            {
                title = title,
                price = price,
                description = desc,
                category = category,
                image = image,
                rating = new RatingModel ()
                {
                    rate = rate,
                    count = count
                }
            };
            
            var jsonRequest = JsonConvert.SerializeObject(requestModel);
            var content = new StringContent(jsonRequest, Encoding.UTF8, Application.Json);
            var response = await _client.PostAsync(_postEndPoint, content);
            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
        }
        public async Task UpdateAsync(int id, string title, float price, string desc, string category, string image, float rate, int count)
        {
            ProductModel requestModel = new ProductModel()
            {
                title = title,
                price = price,
                description = desc,
                category = category,
                image = image,
                rating = new RatingModel()
                {
                    rate = rate,
                    count = count
                }
            };

            var jsonRequest = JsonConvert.SerializeObject(requestModel);
            var content = new StringContent(jsonRequest, Encoding.UTF8, Application.Json);
            var response = await _client.PutAsync(_postEndPoint, content);
            if (response.IsSuccessStatusCode) {
                Console.WriteLine("Updated Successfully.");
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            } else
            {
                Console.WriteLine("Updated Failed.");
            }
        }
        public async Task DeleteAsync(int id)
        {
            var response = await _client.DeleteAsync($"{_postEndPoint}/{id}");
            if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No data found.");
                return;
            }
            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("Successfully Deleted");
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
        }
    }
}

public class ProductModel
{
    public int id { get; set; }
    public string title { get; set; }
    public float price { get; set; }
    public string description { get; set; }
    public string category { get; set; }
    public string image { get; set; }
    public RatingModel rating { get; set; }
}

public class RatingModel
{
    public float rate { get; set; }
    public int count { get; set; }
}

