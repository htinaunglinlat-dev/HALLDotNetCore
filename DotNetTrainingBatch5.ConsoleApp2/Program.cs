// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using DotNetTrainingBatch5.Database.Models;

AppDbContext db = new AppDbContext();   
var lst = db.TblBlogs.ToList();

foreach(var item in lst)
{
    Console.WriteLine($"{item.BlogTitle} {item.BlogContent}");
}
