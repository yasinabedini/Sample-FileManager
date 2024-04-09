using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace WebApplication1.Pages.Document
{
    public class DownLoadModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public DownLoadModel(IHttpClientFactory _factory)
        {
            _httpClient = _factory.CreateClient("FileManager");
        }

        public async Task<IActionResult> OnGet(Guid id)
        {
            var response = await _httpClient.GetAsync("/api/file/download/" + id);

            var fileStream = new FileStream($"{id}.jpg", FileMode.Create, FileAccess.Write);

            response.Content.CopyToAsync(fileStream);

            return Task.FromResult(File(fileStream, "image/jpeg")).Result;
        }

    }
}
