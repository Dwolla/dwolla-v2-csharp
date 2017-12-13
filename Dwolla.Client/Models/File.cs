using System.IO;

namespace Dwolla.Client.Models
{
    public class File
    {
        public Stream Stream { get; set; }
        public string Filename { get; set; }
        public string ContentType { get; set; }
    }
}