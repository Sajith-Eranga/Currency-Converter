using CurrenCoverterServerAPI.BL;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace CurrenCoverterServerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyConverterController : ControllerBase
    {
        
        private readonly ILogger<CurrencyConverterController> _logger;

        public CurrencyConverterController(ILogger<CurrencyConverterController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetConvertData")]
        public IActionResult Get(string value)
        {
            return Ok(new Converter().ConvertCurrencyToWords(value));
        }
    }
}