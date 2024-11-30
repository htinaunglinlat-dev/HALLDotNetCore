using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using static System.Net.Mime.MediaTypeNames;

namespace DotNetTrainingBatch5.ConsoleApp3
{
    public class RestClientExample
    {
        private readonly RestClient _client;
        private readonly string _postEndpoint = "https://jsonplacholder.typicode.com";
        public RestClientExample()
        {
            _client = new RestClient();
        }
        public async Task ReadAsync()
        {
            RestRequest request = new RestRequest($"{_postEndpoint}/posts", Method.Get);
            //var response = await _client.GetAsync(request);
            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                Console.WriteLine(jsonStr);
            }
        }
        public async Task EditAsync(int id)
        {
            RestRequest request = new RestRequest($"{_postEndpoint}/posts/{id}", Method.Get);
            //var response = await _client.GetAsync(request);
            var response = await _client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No data found.");
                return;
            }
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                Console.WriteLine(jsonStr);
            }
        }
        public async Task CreateAsync(string title, string body, int userId)
        {
            PostModel requestModel = new PostModel()
            {
                body = body,
                title = title,
                userId = userId
            }; // C# object | .Net object

            RestRequest request = new RestRequest($"{_postEndpoint}/posts", Method.Post);
            request.AddJsonBody(requestModel);

            //var response = await _client.PostAsync( request);
            var response = await _client.ExecuteAsync( request);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content!);
            }
        }
        public async Task UpdateAsync(int id, string title, string body, int userId)
        {
            PostModel requestModel = new PostModel()
            {
                body = body,
                title = title,
                userId = userId
            }; // C# object | .Net object
            RestRequest request = new RestRequest($"{_postEndpoint}/posts/{id}", Method.Patch);
            request.AddJsonBody(requestModel);

            //var response = await _client.PatchAsync(request);
            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content!);
            }
        }
        public async Task DeleteAsync(int id)
        {
            RestRequest request = new RestRequest($"{_postEndpoint}/posts/{id}", Method.Delete);

            //var response = await _client.DeleteAsync(request);
            var response = await _client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No data found.");
                return;
            }
            if (response.IsSuccessStatusCode)
            
                Console.WriteLine(response.Content);
            }
        }
    }
}
