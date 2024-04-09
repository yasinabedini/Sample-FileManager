using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using WebApplication1.Models;

namespace WebApplication1.Pages.Document
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IndexModel(IHttpClientFactory _factory)
        {
            _httpClient = _factory.CreateClient("FileManager");
        }

        public List<Models.Document> DocList { get; set; }
        public async Task OnGet()
        {
            DocList = JsonSerializer.Deserialize<List<Models.Document>>(await _httpClient.GetStringAsync("/api/File/Index"));
        }
    }
}
