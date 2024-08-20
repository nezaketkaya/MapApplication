using Microsoft.AspNetCore.Mvc;
using MapApplication.Interfaces;
using MapApplication.Services;

namespace MapApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EfPointController : ControllerBase
    {
        private readonly EfPointService _pointService;
        public EfPointController(EfPointService pointService)
        {
            _pointService = pointService;
        }

        [HttpPost("addWithEf")]
        public IActionResult AddPointWithEf(Point point)
        {
            var response = _pointService.Add(point);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("updateWithEf/{id}")]
        public IActionResult UpdatePointWithEf(long id, Point uppoint)
        {
            var response = _pointService.Update(id, uppoint);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("deleteWithEf/{id}")]
        public IActionResult DeletePointWithEf(int id)
        {
            var response = _pointService.Delete(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("getAllWithEf")]
        public IActionResult GetAllPointsWithEf()
        {
            var response = _pointService.GetAll();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("getByIdWithEf/{id}")]
        public IActionResult GetPointByIdWithEf(int id)
        {
            var response = _pointService.GetById(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
