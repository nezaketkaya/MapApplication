using MapApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace MapApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DbPointController : ControllerBase
    {
        private readonly DbService _dbService;
        public DbPointController(DbService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost("addWithDb")]
        public IActionResult AddPointWithDb(Point point)
        {
            var response = _dbService.AddPointWithDb(point);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("updateWithDb/{id}")]
        public IActionResult UpdatePointWithDb(long id, Point uppoint)
        {
            var response = _dbService.UpdatePointWithDb(id, uppoint);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("deleteWithDb/{id}")]
        public IActionResult DeletePointWithDb(int id)
        {
            var response = _dbService.DeletePointWithDb(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("getAllWithDb")]
        public IActionResult GetAllPointsWithDb()
        {
            var response = _dbService.GetAllPointsWithDb();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("getByIdWithDb/{id}")]
        public IActionResult GetPointByIdWithDb(int id)
        {
            var response = _dbService.GetPointByIdWithDb(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
