namespace MapApplication.Interfaces
{
    public interface IPointService
    {
        List<Point> GetAll();
        Point Add(Point point);
        Point GetById(long id);
        Point Update(long id, Point updatedPoint);
        bool Delete(long id);
    }
}
