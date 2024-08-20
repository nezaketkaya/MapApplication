using MapApplication.Data.Abstract;
using System.Collections.Generic;

namespace MapApplication.Services
{
    public class EfPointService
    {
        private readonly IGenericRepository<Point> _repository;

        public EfPointService(IGenericRepository<Point> repository)
        {
            _repository = repository;
        }

        public Response<Point> Add(Point point)
        {
            if (point == null)
            {
                return Response<Point>.ErrorResponse("Invalid point data.");
            }
            return _repository.Add(point);
        }

        public Response<bool> Delete(long id)
        {
            return _repository.Delete(id);
        }

        public Response<List<Point>> GetAll()
        {
            return _repository.GetAll();
        }

        public Response<Point> GetById(long id)
        {
            return _repository.GetById(id);
        }

        public Response<Point> Update(long id, Point updatedPoint)
        {
            var pointResponse = _repository.GetById(id);

            if (!pointResponse.Succeeded)
            {
                return Response<Point>.ErrorResponse("Point not found.");
            }

            var existingPoint = pointResponse.Value;
            existingPoint.pointx = updatedPoint.pointx;
            existingPoint.pointy = updatedPoint.pointy;
            existingPoint.name = updatedPoint.name;

            return _repository.Update(id, existingPoint);
        }
    }
}
