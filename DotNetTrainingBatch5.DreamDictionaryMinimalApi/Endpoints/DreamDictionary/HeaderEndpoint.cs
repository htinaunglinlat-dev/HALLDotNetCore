using Newtonsoft.Json;

namespace DotNetTrainingBatch5.DreamDictionaryMinimalApi.Endpoints.DreamDictionary
{
    public static class HeaderEndpoint
    {
        public static void UseHeaderEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/blogs/getHeaders", () =>
            {
                string folderPath = "Data/DreamDictionary.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<BlogDataModel>(jsonStr);
                if (result is null) return Results.NotFound("No data found.");

                return Results.Ok(result.BlogHeader);
            }).WithName("GetDreamDictionaryHeaders").WithOpenApi();

            app.MapGet("/blogs/getHeaders/{id}", (int id) =>
            {
                string folderPath = "Data/DreamDictionary.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<BlogDataModel>(jsonStr);
                if (result is null) return Results.NotFound("No data found.");

                var item = result.BlogHeader.FirstOrDefault(x => x.BlogId == id);
                if (item is null) return Results.BadRequest("No data is found by provided id.");

                return Results.Ok(item);
            }).WithName("GetDreamDictionaryHeader").WithOpenApi();
        }
    }
}
