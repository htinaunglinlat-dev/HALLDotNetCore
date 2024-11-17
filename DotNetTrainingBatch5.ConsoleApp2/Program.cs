// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using DotNetTrainingBatch5.Database.Models;
using Newtonsoft.Json;

//AppDbContext db = new AppDbContext();   
//var lst = db.TblBlogs.ToList();

//foreach(var item in lst)
//{
//    Console.WriteLine($"{item.BlogTitle} {item.BlogContent}");
//}
var blog = new BlogModel
{
    Id = 1,
    Title = "Test title",
    Author = "Test Author",
    Content = "Test Content"
};

// C# object to JSON object
//string jsonStr = JsonConvert.SerializeObject(blog); // unformatted
//string jsonStr = JsonConvert.SerializeObject(blog, Formatting.Indented); // formatted
string jsonStr = blog.ToJson();

Console.WriteLine(jsonStr);

string jsonStr2 = """{"Id":1,"Title":"Test title","Author":"Test Author","Content":"Test Content"}""";

var blog2 = JsonConvert.DeserializeObject<BlogModel>(jsonStr2); // non-case sensetive
Console.WriteLine(blog2.Title);

//System.Text.Json.JsonSerializer.Serialize(blog); // case sensetive 
//System.Text.Json.JsonSerializer.Deserialize<BlogModel>(jsonStr2);

public class BlogModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Content { get; set; }
}

public static class Extensions // a.k.a Dev Code
{
    public static string ToJsonWithFormat(this object obj)
    {
        string jsonStr = JsonConvert.SerializeObject(obj, Formatting.Indented);
        return jsonStr;
    }
    public static string ToJson(this object obj)
    {
        string jsonStr = JsonConvert.SerializeObject(obj);
        return jsonStr;
    }
}