using FlatFileGenerator.Core.Models;
using FlatFileGenerator.DataAccess.Repositories;
using FlatFileGenerator.DataGenerator.Business;
using FlatFileGenerator.FileWriter.Business;
using Microsoft.AspNetCore.Mvc;
using FlatFileGenerator.FileReader.Business;
using FlatFileGenerator.Utilities.Files;

namespace FlatFileGenerator.Web.Controllers
{
    public class AdminController(ReceiptDetailRepository repository, LogModelRepository logModelRepository, InputFileRepository inputFileRepository, ILoggerFactory loggerFactory) : Controller
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
        public async Task<IActionResult> UploadFile(IFormFile? file, int type, string fileName , string filePath)
        {
            if (file == null || file.Length is <= 0)
            {
                var errorMessage = "File is empty or too large.";
                ModelState.AddModelError("file", errorMessage);
                return new ObjectResult(errorMessage);
            }

            byte[] content;

            using (var reader = new BinaryReader(file.OpenReadStream()))
            {
                content = reader.ReadBytes((int)file.Length);
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = file.FileName;
            }

            if (type == (int)DocumentType.ErrorLog)
            {
                return await ParseLogFile(content, fileName);
            }
            try
            {
                await ParsePayload(content, type);
                return RedirectToAction("Index", "ReceiptDetail");
            }
            catch (Exception ex)
            {
                var errorMessage = "Parser error: " + ex.Message;
                ModelState.AddModelError("file", errorMessage);
                return new ObjectResult(errorMessage);
            }
        }

        private async Task ParsePayload(byte[] content, int type)
        {
            var documentType = (DocumentType)type;
            var parser = ParserFactory.GetParser(documentType, loggerFactory);
            using var payload = new MemoryStream(content);
            var receiptDetails = (await parser.ParseAsync(payload, documentType)).ToList();
            if (receiptDetails.Any())
            {
                await repository.AddRange(receiptDetails.ToList());
            }
        }

        private async Task<IActionResult> ParseLogFile(byte[] content, string fileName) 
        {
            var parser = new LogParser(loggerFactory);
            using var payload = new MemoryStream(content);
            var logModels = (await parser.ParseAsync(payload, DocumentType.ErrorLog)).ToList();
            if (logModels.Any())
            {
                foreach (var model in logModels)
                {
                    model.DocumentName = fileName;
                }
                await logModelRepository.AddRange(logModels.ToList());
            }
            // Process logModels as needed
            return RedirectToAction("Index", "LogModel");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UploadFromDirectory(int type, string fileName, string filePath)
        {
            if (string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(filePath))
            {
                var errorMessage = "File name or path missing";
                ModelState.AddModelError("file", errorMessage);
                return new ObjectResult(errorMessage);
            }

            try
            {
                var content = await FileUtil.ReadFileAsync(fileName, filePath);
                await ParsePayload(content, type);
                return RedirectToAction("Index", "ReceiptDetail");
            }
            catch (Exception e)
            {
                return new ObjectResult(e.Message);
            }
        }

        [HttpGet("[action]")]
        public IActionResult Generate()
        {
            return View();
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

            var receiptDetails = (repository.GetQueryList()).ToList();
            if (!receiptDetails.Any())
            {
                ModelState.AddModelError("fileName", "No data available to export.");
                return View("Export");
            }

            var writer = WriterFactory.GetWriter((DocumentType)type, loggerFactory);
            var success = await writer.WriteAsync(receiptDetails, fileName, filePath);

            if (success) return new ObjectResult("Export successful!");
            ModelState.AddModelError("fileName", "Failed to write file.");
            return new ObjectResult("Export failed!");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GenerateReceiptDetails(int quantity = 100)
        {
            var generator = new ReceiptDetailGenerator();
            var receiptDetails = generator.GenerateReceiptDetails(quantity);
            await repository.AddRange(receiptDetails);

            return RedirectToAction("Index", "ReceiptDetail");
        }

        private InputFile GetInputFile(string fileName, long length, DocumentType documentType)
        {
            return new InputFile
            {
                FileName = fileName,
                Size = length/1024,
                DocumentType = documentType
            };
        }

    }
}