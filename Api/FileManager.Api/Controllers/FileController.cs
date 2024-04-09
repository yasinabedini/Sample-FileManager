using FileManager.Api.Context;
using FileManager.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FileManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : Controller
    {
        private readonly FileManagerDbContext _context;

        public FileController(FileManagerDbContext context)
        {
            _context = context;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            List<Document> files = _context.Documents.ToList();

            return Ok(JsonSerializer.Serialize(files));
        }

        [HttpGet("Download/{id}")]        
        public IActionResult Download(Guid id)
        {
            var FindDoc = _context.Documents.Find(id);

            return File(FindDoc.Data, FindDoc.ContentType, FindDoc.FileName);
        }

        [HttpPost("Upload")]
        [Produces("image/jpeg")]    
        public IActionResult Upload(IFormFile file)
        {
            Document myDoc = new Document
            {
                FileName = file.FileName,
                ContentType = file.ContentType,
                Length = (int)file.Length,
                Extention = Path.GetExtension(file.FileName),
                Data = null
            };

            using var stream = new MemoryStream();

            file.CopyTo(stream);
            stream.Position = 0;

            myDoc.Data = stream.ToArray();

            _context.Documents.Add(myDoc);
            _context.SaveChanges();

            return Ok("File Upload Succesfully ...");
        }
    }
}
