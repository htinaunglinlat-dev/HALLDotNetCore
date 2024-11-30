// See https://aka.ms/new-console-template for more information
using ConsoleAppHW;

Console.WriteLine("Hello, World!");

HttpClientExample httpClientExample = new HttpClientExample();
//await httpClientExample.ReadAsync();
//await httpClientExample.EditAsync(1);
//await httpClientExample.EditAsync(101);

// string title, float price, string desc, string category, string image, float rate, int count
//await httpClientExample.CreateAsync("hello world", 100, "we are description", "cloth", "https://fakestoreapi.com/img/81fPKd-2AYL._AC_SL1500_.jpg", 4, 10);

//await httpClientExample.UpdateAsync(1, "hello world", 100, "we are description", "cloth", "https://fakestoreapi.com/img/81fPKd-2AYL._AC_SL1500_.jpg", 4, 10);
await httpClientExample.DeleteAsync(1);



//{ "id":1,"title":"Fjallraven - Foldsack No. 1 Backpack, Fits 15 Laptops","price":109.95,"description":"Your perfect pack for everyday use and walks in the forest. Stash your laptop (up to 15 inches) in the padded sleeve, your everyday","category":"men's clothing","image":"https://fakestoreapi.com/img/81fPKd-2AYL._AC_SL1500_.jpg","rating":{ "rate":3.9,"count":120} }