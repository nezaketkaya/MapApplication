using MapApplication.Interfaces;

namespace MapApplication.Services
{
    public class PointService : IPointService
    {
        private static List<Point> _pointList = new List<Point>();

        public Response<Point> Add(Point point)
        {
            if (point == null)
            {
                return Response<Point>.ErrorResponse("Invalid point data.");
            }

            _pointList.Add(point);

            return Response<Point>.SuccessResponse(point, "Point added successfully.");
        }

        public Response<bool> Delete(long id)
        {
            var point = _pointList.FirstOrDefault(p => p.Id == id);
            if (point == null)
            {
                return Response<bool>.ErrorResponse("Point not found.", 404);
            }

            _pointList.Remove(point);

            return Response<bool>.SuccessResponse(true, "Point deleted successfully.");
        }

        public Response<List<Point>> GetAll()
        {
            return Response<List<Point>>.SuccessResponse(_pointList, "Points retrieved successfully.");
        }

        public Response<Point> GetById(long id)
        {
            var point = _pointList.FirstOrDefault(p => p.Id == id);

            if (point == null)
            {
                return Response<Point>.ErrorResponse("Point not found.", 404);
            }

            return Response<Point>.SuccessResponse(point, "Point retrieved successfully.");
        }

        public Response<Point> Update(long id, Point updatedPoint)
        {
            var point = _pointList.FirstOrDefault(p => p.Id == id);

            if (point == null)
            {
                return Response<Point>.ErrorResponse("Point not found.", 404);
            }

            point.PointX = updatedPoint.PointX;
            point.PointY = updatedPoint.PointY;
            point.Name = updatedPoint.Name;

            return Response<Point>.SuccessResponse(point, "Point updated successfully.");
        }
    }
}
