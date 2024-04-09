namespace FileManager.Api.Models
{
    public class Document
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string Extention { get; set; }
        public int Length { get; set; }
        public byte[] Data { get; set; }

    }
}
