﻿using MapApplication.Interfaces;
using MapApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace MapApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointController : ControllerBase
    {
        private readonly IPointService _pointService;
        private readonly DbService _dbService;

        public PointController(IPointService pointService, DbService dbService)
        {
            _pointService = pointService;
            _dbService = dbService;
        }

        [HttpGet("{id}")]
        public IActionResult GetPointById(long id)
        {
            var response = _pointService.GetById(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("Add")]
        public IActionResult AddPoint([FromBody] Point point)
        {
            var response = _pointService.Add(point);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePoint(long id, Point updatedPoint)
        {
            var response = _pointService.Update(id, updatedPoint);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePoint(long id)
        {
            var response = _pointService.Delete(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        public IActionResult GetAllPoints()
        {
            var response = _pointService.GetAll();

            return StatusCode(response.StatusCode, response);
        }
    }
}