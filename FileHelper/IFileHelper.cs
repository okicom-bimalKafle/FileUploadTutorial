using Microsoft.AspNetCore.Components.Forms;

namespace FileUploadTutorial.FileHelper
{
    public interface IFileHelper
    {
        Task<string> UploadFileAsync(IFormFile formFile,string folderPath);
    }
}
