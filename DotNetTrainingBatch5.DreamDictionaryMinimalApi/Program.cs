using DotNetTrainingBatch5.DreamDictionaryMinimalApi.Endpoints.DreamDictionary;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.MapGet("/blogs/getHeaders", () =>
//{
//    string folderPath = "Data/DreamDictionary.json";
//    var jsonStr = File.ReadAllText(folderPath);
//    var result = JsonConvert.DeserializeObject<BlogDataModel>(jsonStr);
//    if (result is null) return Results.NotFound("No data found.");

//    return Results.Ok(result.BlogHeader);
//}).WithName("GetDreamDictionaryHeaders").WithOpenApi();

//app.MapGet("/blogs/getDetails", () =>
//{
//    string folderPath = "Data/DreamDictionary.json";
//    var jsonStr = File.ReadAllText(folderPath);
//    var result = JsonConvert.DeserializeObject<BlogDataModel>(jsonStr);
//    if (result is null) return Results.NotFound("No data found.");

//    return Results.Ok(result.BlogDetail);
//}).WithName("GetDreamDictionaryDetails").WithOpenApi();

//app.MapGet("/blogs/getHeaders/{id}", (int id) =>
//{
//    string folderPath = "Data/DreamDictionary.json";
//    var jsonStr = File.ReadAllText(folderPath);
//    var result = JsonConvert.DeserializeObject<BlogDataModel>(jsonStr);
//    if (result is null) return Results.NotFound("No data found.");

//    var item = result.BlogHeader.FirstOrDefault(x => x.BlogId == id);
//    if (item is null) return Results.BadRequest("No data is found by provided id.");

//    return Results.Ok(item);
//}).WithName("GetDreamDictionaryHeader").WithOpenApi();

//app.MapGet("/blogs/getDetails/{id}", (int id) =>
//{
//    string folderPath = "Data/DreamDictionary.json";
//    var jsonStr = File.ReadAllText(folderPath);
//    var result = JsonConvert.DeserializeObject<BlogDataModel>(jsonStr);
//    if (result is null) return Results.NotFound("No data found.");

//    var item = result.BlogDetail.FirstOrDefault(x => x.BlogDetailId == id);
//    if (item is null) return Results.BadRequest("No data is found by provided id.");

//    return Results.Ok(item);
//}).WithName("GetDreamDictionaryDetail").WithOpenApi();

app.UseHeaderEndpoint();
app.UseDetailEndpoint();

app.Run();


public class BlogDataModel
{
    public List<BlogHeader> BlogHeader { get; set; }
    public List<BlogDetail> BlogDetail { get; set; }
}

public class BlogHeader
{
    public int BlogId { get; set; }
    public string BlogTitle { get; set; }
}

public class BlogDetail
{
    public int BlogDetailId { get; set; }
    public int BlogId { get; set; }
    public string BlogContent { get; set; }
}
