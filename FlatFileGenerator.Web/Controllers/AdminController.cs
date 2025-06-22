using FlatFileGenerator.Core.Models;
using FlatFileGenerator.DataAccess.Repositories;
using FlatFileGenerator.FileWriter.Business;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Text.Json.Serialization;
using FlatFileGenerator.FileReader.Business;

namespace FlatFileGenerator.Web.Controllers
{
    public class AdminController(ReceiptDetailRepository repository) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("[action]")]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UploadFile(IFormFile file, int type)
        {
            if (file.Length is <= 0 or > long.MaxValue)
            {
                ModelState.AddModelError("file", "File is empty or too large.");
                return View("Index");
            }

            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.ToString();
            if (string.IsNullOrEmpty(fileName))
            {
                ModelState.AddModelError("file", "Invalid file type. Please upload a .txt file.");
                return View("Index");
            }

            byte[] content;

            using (var reader = new BinaryReader(file.OpenReadStream()))
            {
                content = reader.ReadBytes((int)file.Length);
            }

            var documentType = (DocumentType) type;
            var parser = ParserFactory.GetParser(documentType);
            var filetype = file.ContentType.ToLowerInvariant();


            try
            {
                using var payload = new MemoryStream(content);
                var receiptDetails = (await parser.ParseAsync(payload, documentType)).ToList();
                if (receiptDetails.Any())
                {
                    await repository.AddRange(receiptDetails.ToList());
                }

                return RedirectToAction("Index", "ReceiptDetail");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("file", $"JSON deserialization error: {ex.Message}");
                return View("Index");
            }


            var result = "File uploaded";

            return new ObjectResult(result);

        }

        [HttpGet("[action]")]
        public IActionResult Export()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ExportToFile(string fileName, string filePath, int type)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                ModelState.AddModelError("fileName", "Please select a file name.");
                return View("Export");
            }

            var receiptDetails = (await repository.GetList()).ToList();
            if (!receiptDetails.Any())
            {
                ModelState.AddModelError("fileName", "No data available to export.");
                return View("Export");
            }

            var writer = WriterFactory.GetWriter((DocumentType)type);
            var success = await writer.WriteAsync(receiptDetails, fileName, filePath);

            return new ObjectResult("Export successful!");
        }
    }
}