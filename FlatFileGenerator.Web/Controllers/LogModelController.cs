using FlatFileGenerator.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FlatFileGenerator.Web.Controllers
{
    public class LogModelController(LogModelRepository repository): Controller
    {
        public IActionResult Index()
        {
            var logModels = repository.GetQueryList();
            return View(logModels);
        }
    }
}
