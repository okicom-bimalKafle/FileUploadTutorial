using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileUploadTutorial.Models
{
    public class FileProperty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public int Size { get; set; }
        public DateTime CreatedDateTime { get; set; }
        [NotMapped]
        public IFormFile Document { get; set; }
        
    }
}
