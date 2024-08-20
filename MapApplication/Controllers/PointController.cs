using MapApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace MapApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointController : ControllerBase
    {
        private readonly PointService _pointService;
        public PointController(PointService pointService)
        {
            _pointService = pointService;
        }

        [HttpPost("addUOW")]
        public IActionResult AddPointUOW(Point point)
        {
            var response = _pointService.Add(point);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("updateUOW/{id}")]
        public IActionResult UpdatePointUOW(long id, Point uppoint)
        {
            var response = _pointService.Update(id, uppoint);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("deleteUOW/{id}")]
        public IActionResult DeletePointUOW(int id)
        {
            var response = _pointService.Delete(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("getAllUOW")]
        public IActionResult GetAllPointUOW()
        {
            var response = _pointService.GetAll();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("getByIdUOW/{id}")]
        public IActionResult GetPointByIdUOW(int id)
        {
            var response = _pointService.GetById(id);
            return StatusCode(response.StatusCode, response);
        }
}
