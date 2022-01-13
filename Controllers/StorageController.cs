using Microsoft.AspNetCore.Mvc;
using PISLab.Storage;

namespace PISLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        public string Index()
        {
            return "This is our new controller...";
        }
        private readonly StorageService _storageService;

        public StorageController(StorageService storageService)
        {
            _storageService = storageService;
        }

        [HttpGet]
        [Route("storageType")]
        public ActionResult<string> GetStorageType()
        {
            return Ok(_storageService.GetStorageType());
        }

        [HttpGet]
        [Route("itemsCount")]
        public ActionResult<string> GetItemsCount()
        {
            return Ok(_storageService.GetNumberOfItems());
        }
    }
}