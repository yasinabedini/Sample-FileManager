using Microsoft.AspNetCore.Mvc;
using MyUploader.Context;
using MyUploader.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FileManager.Api.Controllers
{
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

        [HttpGet("Download")]
        public IActionResult Download(Guid id)
        {
            var FindDoc = _context.Documents.Find(id);

            return File(FindDoc.Data, FindDoc.ContentType, FindDoc.FileName);
        }

        [HttpPost("Upload")]
        public IActionResult Upload(IFormFile uploadFile)
        {
            Document myDoc = new Document
            {
                FileName = uploadFile.FileName,
                ContentType = uploadFile.ContentType,
                Length = (int)uploadFile.Length,
                Extention = Path.GetExtension(uploadFile.FileName),
                Data = null
            };

            using var stream = new MemoryStream();

            uploadFile.CopyTo(stream);
            stream.Position = 0;

            myDoc.Data = stream.ToArray();

            _context.Documents.Add(myDoc);
            _context.SaveChanges();

            return Ok("File Upload Succesfully ...");
        }
    }
}
