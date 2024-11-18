using Newtonsoft.Json;

namespace DotNetTrainingBatch5.DreamDictionaryMinimalApi.Endpoints.DreamDictionary
{
    public static class DetailEndpoint
    {
        public static void UseDetailEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/details", () =>
            {
                string folderPath = "Data/DreamDictionary.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<BlogDataModel>(jsonStr);
                if (result is null) return Results.NotFound("No data found.");

                return Results.Ok(result.BlogDetail);
            }).WithName("GetDreamDictionaryDetails").WithOpenApi();

            app.MapGet("/details/{id}", (int id) =>
            {
                string folderPath = "Data/DreamDictionary.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<BlogDataModel>(jsonStr);
                if (result is null) return Results.NotFound("No data found.");

                var item = result.BlogDetail.FirstOrDefault(x => x.BlogDetailId == id);
                if (item is null) return Results.BadRequest("No data is found by provided id.");

                return Results.Ok(item);
            }).WithName("GetDreamDictionaryDetail").WithOpenApi();

            app.MapPost("/details", (BlogDetail requestDetail) =>
            {
                if (requestDetail.BlogId == 0 || String.IsNullOrEmpty(requestDetail.BlogContent)) return Results.BadRequest("All fields are required.");

                string folderPath = "Data/DreamDictionary.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<BlogDataModel>(jsonStr)!;

                requestDetail.BlogDetailId = result.BlogDetail.Count == 0 ? 1 : result.BlogDetail.Max(x => x.BlogDetailId) + 1;

                result.BlogDetail.Add(requestDetail);

                var jsonStrToWrite = JsonConvert.SerializeObject(result);
                File.WriteAllText(folderPath, jsonStrToWrite);

                return Results.Ok(requestDetail);
            }).WithName("PostDreamDictionaryDetail").WithOpenApi();

            app.MapPut("/details/{id}", (int id, BlogDetail requestDetail) =>
            {
                if (requestDetail.BlogId == 0 || String.IsNullOrEmpty(requestDetail.BlogContent)) return Results.BadRequest("All fields are required.");

                string folderPath = "Data/DreamDictionary.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<BlogDataModel>(jsonStr);

                if (result is null) return Results.BadRequest("No data found.");

                var item = result.BlogDetail.FirstOrDefault(x => x.BlogDetailId == id);
                if (item is null) return Results.BadRequest("No data found by provide id.");

                item.BlogId = requestDetail.BlogId;
                item.BlogContent = requestDetail.BlogContent;

                var jsonStrToWrite = JsonConvert.SerializeObject(result);
                File.WriteAllText(folderPath, jsonStrToWrite);

                return Results.Ok(requestDetail);
            }).WithName("PutDreamDictionaryDetail").WithOpenApi();

            app.MapPatch("/details/{id}", (int id, BlogDetail requestDetail) =>
            {
                string folderPath = "Data/DreamDictionary.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<BlogDataModel>(jsonStr)!;

                var item = result.BlogDetail.FirstOrDefault(x => x.BlogDetailId == id);
                if (item is null) return Results.BadRequest("No data found.");

                if (requestDetail.BlogId > 0) item.BlogId = requestDetail.BlogId;
                if (!String.IsNullOrEmpty(requestDetail.BlogContent)) item.BlogContent = requestDetail.BlogContent;

                var jsonStrToWrite = JsonConvert.SerializeObject(result);
                File.WriteAllText(folderPath, jsonStrToWrite);

                return Results.Ok(requestDetail);
            }).WithName("PatchDreamDictionaryDetail").WithOpenApi();

            app.MapDelete("/details/{id}", (int id) =>
            {
                string folderPath = "Data/DreamDictionary.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<BlogDataModel>(jsonStr)!;

                var lst = result.BlogDetail.Where(x => x.BlogDetailId != id);

                result.BlogDetail = lst.ToList();

                var jsonStrToWrite = JsonConvert.SerializeObject(result);
                File.WriteAllText(folderPath, jsonStrToWrite);

                return Results.Ok("Successfully Deleted.");
            }).WithName("DeleteDreamDictionaryDetail").WithOpenApi();
        }
    }
}
