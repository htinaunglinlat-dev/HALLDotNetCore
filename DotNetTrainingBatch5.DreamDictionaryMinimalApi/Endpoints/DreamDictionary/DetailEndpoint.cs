using Newtonsoft.Json;

namespace DotNetTrainingBatch5.DreamDictionaryMinimalApi.Endpoints.DreamDictionary
{
    public static class DetailEndpoint
    {
        public static void UseDetailEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/blogs/getDetails", () =>
            {
                string folderPath = "Data/DreamDictionary.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<BlogDataModel>(jsonStr);
                if (result is null) return Results.NotFound("No data found.");

                return Results.Ok(result.BlogDetail);
            }).WithName("GetDreamDictionaryDetails").WithOpenApi();

            app.MapGet("/blogs/getDetails/{id}", (int id) =>
            {
                string folderPath = "Data/DreamDictionary.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<BlogDataModel>(jsonStr);
                if (result is null) return Results.NotFound("No data found.");

                var item = result.BlogDetail.FirstOrDefault(x => x.BlogDetailId == id);
                if (item is null) return Results.BadRequest("No data is found by provided id.");

                return Results.Ok(item);
            }).WithName("GetDreamDictionaryDetail").WithOpenApi();
        }
    }
}
