using MapApplication.Interfaces;

namespace MapApplication.Services
{
    public class PointService : IPointService
    {
        private static List<Point> _pointList = new List<Point>();
        public Point Add(Point point)
        {
            _pointList.Add(point);

            return point;
        }

        public bool Delete(long id)
        {
            var point = _pointList.FirstOrDefault(p => p.Id == id);

            if (point != null)
            {
                _pointList.Remove(point);
                return true;
            }

            return false;
        }

        public List<Point> GetAll()
        {
            return _pointList;
        }

        public Point GetById(long id)
        {
            return _pointList.FirstOrDefault(p => p.Id == id);
        }

        public Point Update(long id, Point updatedPoint)
        {
            var point = _pointList.FirstOrDefault(p => p.Id == id);

            if (point != null)
            {
                point.PointX = updatedPoint.PointX;
                point.PointY = updatedPoint.PointY;
                point.Name = updatedPoint.Name;
            }

            return point;
        }
    }
}
