using Microsoft.AspNetCore.Mvc;
using SchoolMangment.Database;

namespace SchoolMangment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreateaTablesController : ControllerBase
    {
        private readonly SqlSugarContext sqlSugarContext;
        private readonly ILogger<CreateaTablesController> _logger;

        public CreateaTablesController(ILogger<CreateaTablesController> logger, SqlSugarContext sqlSugarContext)
        {
            _logger = logger;
            this.sqlSugarContext = sqlSugarContext;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public ActionResult<string> Get()
        {
            sqlSugarContext.CreateTable();
            return Ok("true");
        }
    }
}