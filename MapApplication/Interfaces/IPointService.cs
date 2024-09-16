using MapApplication.Model;

namespace MapApplication.Interfaces
{
    public interface IPointService
    {
        Response<Point> Add(Point point);
        Response<Point> GetById(long id);
        Response<List<Point>> GetAll();
        Response<Point> Update(long id, Point updatedPoint);
        Response<bool> Delete(long id);
    }
}
