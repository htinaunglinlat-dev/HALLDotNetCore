using Newtonsoft.Json;

namespace DotNetTrainingBatch5.DreamDictionaryMinimalApi.Endpoints.DreamDictionary
{
    public static class HeaderEndpoint
    {
        public static void UseHeaderEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/headers", () =>
            {
                string folderPath = "Data/DreamDictionary.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<BlogDataModel>(jsonStr);

                if (result is null) return Results.NotFound("No data found.");

                return Results.Ok(result.BlogHeader);
            }).WithName("GetDreamDictionaryHeaders").WithOpenApi();

            app.MapGet("/headers/{id}", (int id) =>
            {
                string folderPath = "Data/DreamDictionary.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<BlogDataModel>(jsonStr);

                if (result is null) return Results.NotFound("No data found.");

                var item = result.BlogHeader.FirstOrDefault(x => x.BlogId == id);

                if (item is null) return Results.BadRequest("No data is found by provided id.");

                return Results.Ok(item);
            }).WithName("GetDreamDictionaryHeader").WithOpenApi();

            app.MapPost("/headers", (BlogHeader requestModel) =>
            {
                if (String.IsNullOrEmpty(requestModel.BlogTitle)) return Results.BadRequest("All Fields are required.");

                string folderPath = "Data/DreamDictionary.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<BlogDataModel>(jsonStr)!;

                requestModel.BlogId = result.BlogHeader.Count == 0 ? 1 : result.BlogHeader.Max(x => x.BlogId) + 1;

                result.BlogHeader.Add(requestModel);

                var jsonStrToWrite = JsonConvert.SerializeObject(result);
                File.WriteAllText(folderPath, jsonStrToWrite);

                return Results.Ok(requestModel);
            }).WithName("PostDreamDictionaryHeader").WithOpenApi();

            app.MapPut("/headers/{id}", (int id, BlogHeader requestModel) =>
            {
                if (String.IsNullOrEmpty(requestModel.BlogTitle)) return Results.BadRequest("All fields are required.");

                string folderPath = "Data/DreamDictionary.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<BlogDataModel>(jsonStr)!;

                var item = result.BlogHeader.FirstOrDefault(x => x.BlogId == id);
                if (item is null) return Results.NotFound("No data is found.");

                item.BlogTitle = requestModel.BlogTitle;

                var jsonStrToWrite = JsonConvert.SerializeObject(result);
                File.WriteAllText(folderPath, jsonStrToWrite);

                return Results.Ok(requestModel);
            }).WithName("PutDreamDictionaryHeader").WithOpenApi();

            app.MapPatch("/headers/{id}", (int id, BlogHeader requestModel) =>
            {
                string folderPath = "Data/DreamDictionary.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<BlogDataModel>(jsonStr)!;

                var item = result.BlogHeader.FirstOrDefault(x => x.BlogId == id);
                if (item is null) return Results.NotFound("No data is found.");

                if(!String.IsNullOrEmpty(requestModel.BlogTitle)) item.BlogTitle = requestModel.BlogTitle;

                var jsonStrToWrite = JsonConvert.SerializeObject(result);
                File.WriteAllText(folderPath, jsonStrToWrite);

                return Results.Ok(requestModel);
            }).WithName("PatchDreamDictionaryHeader").WithOpenApi();

            app.MapDelete("/headers/{id}", (int id) =>
            {
                string folderPath = "Data/DreamDictionary.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<BlogDataModel>(jsonStr)!;

                var lst = result.BlogHeader.Where(x => x.BlogId != id);

                result.BlogHeader = lst.ToList();

                var jsonStrToWrite = JsonConvert.SerializeObject(result);
                File.WriteAllText(folderPath, jsonStrToWrite);

                return Results.Ok("Successfully Deleted.");
            }).WithName("DeleteDreamDictionaryHeader").WithOpenApi();
        }
    }
}
