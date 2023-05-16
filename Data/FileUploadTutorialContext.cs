using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FileUploadTutorial.Models;

namespace FileUploadTutorial.Data
{
    public class FileUploadTutorialContext : DbContext
    {
        public FileUploadTutorialContext (DbContextOptions<FileUploadTutorialContext> options)
            : base(options)
        {
        }

        public DbSet<FileUploadTutorial.Models.FileProperty> File { get; set; } = default!;
    }
}
