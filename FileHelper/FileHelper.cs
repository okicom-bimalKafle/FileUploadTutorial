using Microsoft.AspNetCore.Components.Forms;

namespace FileUploadTutorial.FileHelper
{
    public class FileHelper : IFileHelper
    {
        public readonly IHttpContextAccessor Context;

        public FileHelper(IHttpContextAccessor context)
        {
            Context = context;
        }

        public async Task<string> UploadFileAsync(IFormFile browserFile, string folderPath)
        {
            var fileName = $"{Guid.NewGuid().ToString()}.{browserFile.Name}";
            var folderDirectory = Path.Combine(Context.HttpContext.Request.PathBase, "wwwroot", folderPath);
            var filePath=Path.Combine(folderDirectory, fileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await browserFile.OpenReadStream().CopyToAsync(stream);
            return filePath;
        }
    }
}
