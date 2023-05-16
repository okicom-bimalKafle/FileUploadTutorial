using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FileUploadTutorial.Data;
using FileUploadTutorial.Models;
using FileUploadTutorial.FileHelper;

namespace FileUploadTutorial.Controllers
{
    public class FilePropertiesController : Controller
    {
        private readonly FileUploadTutorialContext _context;
        private readonly IFileHelper _fileHelper;
        public FilePropertiesController(FileUploadTutorialContext context, IFileHelper fileHelper)
        {
            _context = context;
            _fileHelper = fileHelper;
        }

        // GET: FileProperties
        public async Task<IActionResult> Index()
        {
              return _context.File != null ? 
                          View(await _context.File.ToListAsync()) :
                          Problem("Entity set 'FileUploadTutorialContext.File'  is null.");
        }

        // GET: FileProperties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.File == null)
            {
                return NotFound();
            }

            var fileProperty = await _context.File
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fileProperty == null)
            {
                return NotFound();
            }

            return View(fileProperty);
        }

        // GET: FileProperties/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FileProperties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Path,Size,CreatedDateTime,Document")] FileProperty fileProperty)
        {
            if (ModelState.IsValid)
            {
                if (fileProperty.Document != null)
                {
                    var filePath = await _fileHelper.UploadFileAsync(fileProperty.Document, "uploads");
                    fileProperty.Path= filePath;
                }
                _context.Add(fileProperty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fileProperty);
        }

        // GET: FileProperties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.File == null)
            {
                return NotFound();
            }

            var fileProperty = await _context.File.FindAsync(id);
            if (fileProperty == null)
            {
                return NotFound();
            }
            return View(fileProperty);
        }

        // POST: FileProperties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Path,Size,CreatedDateTime")] FileProperty fileProperty)
        {
            if (id != fileProperty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fileProperty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilePropertyExists(fileProperty.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(fileProperty);
        }

        // GET: FileProperties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.File == null)
            {
                return NotFound();
            }

            var fileProperty = await _context.File
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fileProperty == null)
            {
                return NotFound();
            }

            return View(fileProperty);
        }

        // POST: FileProperties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.File == null)
            {
                return Problem("Entity set 'FileUploadTutorialContext.File'  is null.");
            }
            var fileProperty = await _context.File.FindAsync(id);
            if (fileProperty != null)
            {
                _context.File.Remove(fileProperty);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilePropertyExists(int id)
        {
          return (_context.File?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
