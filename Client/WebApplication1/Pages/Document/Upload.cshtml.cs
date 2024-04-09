

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace WebApplication1.Pages.Document
{
    public class UploadModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public UploadModel(IHttpClientFactory _factory)
        {
            _httpClient = _factory.CreateClient("FileManager");
        }


        [BindProperty]
        public IFormFile UploadFile { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            
            var requestContent = new MultipartFormDataContent();

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", UploadFile.FileName);

            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await UploadFile.CopyToAsync(stream);
            }

            // Assuming you have the image path            
            var imageContent = new ByteArrayContent(System.IO.File.ReadAllBytes(filePath));
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

            // 'file' corresponds to the name of the form field in your API
            requestContent.Add(imageContent, "file", Path.GetFileName(filePath));

            // Replace 'your_api_endpoint' with the actual endpoint where you want to send the image
            var response = await _httpClient.PostAsync("/api/file/upload", requestContent);

            return RedirectToPage("Index");
        }
    }
}
