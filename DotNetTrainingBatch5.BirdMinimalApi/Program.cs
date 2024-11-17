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

//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast")
//.WithOpenApi();

app.MapGet("/birds", () =>
{
    string folderPath = "Data/Birds.json";
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;

    return Results.Ok(result.Tbl_Bird);
}).WithName("GetBirds").WithOpenApi();

app.MapGet("/birds/{id}", (int id) =>
{
    string folderPath = "Data/Birds.json";
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;
    var item = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);

    if(item is null) return Results.BadRequest("No data is founed.");

    return Results.Ok(item);
}).WithName("GetBird").WithOpenApi();

app.MapPost("/birds", (BirdModel requestModel) =>
{
    string folderPath = "Data/Birds.json";
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;
    requestModel.Id = result.Tbl_Bird.Count == 0 ? 1 : result.Tbl_Bird.Max(x => x.Id) + 1;
    result.Tbl_Bird.Add(requestModel);
    
    var jsonStrToWrite = JsonConvert.SerializeObject(result);
    File.WriteAllText(folderPath, jsonStrToWrite);

    return Results.Ok(requestModel);
}).WithName("PostBird").WithOpenApi();

app.MapPut("/birds/{id}", (int id, BirdModel requestModel) =>
{
    string folderPath = "Data/Birds.json";
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;
    var item = result.Tbl_Bird.Find(x => x.Id == id);

    if (item is null) return Results.BadRequest("no data is found.");

    item.BirdMyanmarName = requestModel.BirdMyanmarName;
    item.BirdEnglishName = requestModel.BirdEnglishName;
    item.Description = requestModel.Description;
    item.ImagePath = requestModel.ImagePath;

    var jsonStrToWrite = JsonConvert.SerializeObject(result);
    File.WriteAllText(folderPath, jsonStrToWrite);

    return Results.Ok(item);
}).WithName("PutBird").WithOpenApi();

app.MapPatch("/birds/{id}", (int id, BirdModel requestModel) =>
{
    string folderPath = "Data/Birds.json";
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;
    var item = result.Tbl_Bird.Find(x => x.Id == id);

    if (item is null) return Results.BadRequest("no data is found.");

    if (!String.IsNullOrEmpty(requestModel.BirdMyanmarName)) item.BirdMyanmarName = requestModel.BirdMyanmarName;
    if (!String.IsNullOrEmpty(requestModel.BirdEnglishName)) item.BirdEnglishName = requestModel.BirdEnglishName;
    if (!String.IsNullOrEmpty(requestModel.Description)) item.Description = requestModel.Description;
    if (!String.IsNullOrEmpty(requestModel.ImagePath)) item.ImagePath = requestModel.ImagePath;

    var jsonStrToWrite = JsonConvert.SerializeObject(result);
    File.WriteAllText(folderPath, jsonStrToWrite);

    return Results.Ok(item);
}).WithName("PatchBird").WithOpenApi();

app.MapDelete("/birds/{id}", (int id) =>
{
    string folderPath = "Data/Birds.json";
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;
    var lst = result.Tbl_Bird.Where(x => x.Id != id).ToList();

    result.Tbl_Bird = lst;

    var jsonStrToWrite = JsonConvert.SerializeObject(result);
    File.WriteAllText(folderPath, jsonStrToWrite);

    return Results.Ok(result.Tbl_Bird);
}).WithName("DeleteBird").WithOpenApi();

app.Run();

//internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}

public class BirdResponseModel
{
    public List<BirdModel> Tbl_Bird { get; set; }
}

public class BirdModel
{
    public int Id { get; set; }
    public string BirdMyanmarName { get; set; }
    public string BirdEnglishName { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
}
