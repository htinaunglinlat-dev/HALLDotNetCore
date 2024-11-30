// See https://aka.ms/new-console-template for more information
using DotNetTrainingBatch5.ConsoleApp3;

Console.WriteLine("Hello, World!");

// GET
// POST
// PUT
// PATCH
// DELETE

//string uri = "https://fakestoreapi.com/products";
//HttpClient client = new HttpClient();
//var response = await client.GetAsync(uri);
//if(response.IsSuccessStatusCode)
//{
//    string jsonStr = await response.Content.ReadAsStringAsync();
//    Console.WriteLine(jsonStr);
//}

HttpClientExample httpClientExample = new HttpClientExample();
//await httpClientExample.ReadAsync();
await httpClientExample.EditAsync(1);
//await httpClientExample.EditAsync(110);
//await httpClientExample.CreateAsync("hello world", "hello world is just testing", 3);
Console.WriteLine("processing ...");