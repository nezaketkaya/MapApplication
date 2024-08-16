using Npgsql;

namespace MapApplication.Services
{
    public class DbService
    {
        private const string _connectionString = "Server=127.0.0.1; Port=5432; Database=MapApplication; User Id= postgres; Password=mysecretpassword;";
        private NpgsqlConnection GetOpenConnection()
        {
            var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        public Response<Point> AddPointWithDb(Point point)
        {
            if (point == null)
            {
                return Response<Point>.ErrorResponse("Invalid point data.");
            }

            using (var connection = GetOpenConnection())
            {
                var commandText = "INSERT INTO Points (Id, PointX, PointY, Name) VALUES (@id, @pointx, @pointy, @name)";
                using (var command = new NpgsqlCommand(commandText, connection))
                {
                   command.Parameters.AddWithValue("@id", point.Id);
                   command.Parameters.AddWithValue("@pointx", point.PointX);
                   command.Parameters.AddWithValue("@pointy", point.PointY);
                   command.Parameters.AddWithValue("@name", point.Name);

                   command.ExecuteNonQuery();
                }
            }

            return Response<Point>.SuccessResponse(point, "Point added successfully.");
        }

        public Response<Point> UpdatePointWithDb(long id, Point point)
        {
            if (point == null)
            {
                return Response<Point>.ErrorResponse("Invalid point data.");
            }

            var commandText = $"UPDATE Points SET PointX = {point.PointX}, PointY = {point.PointY}, Name = '{point.Name}' WHERE Id = {point.Id}";

            using (var connection = GetOpenConnection())

            using (var command = new NpgsqlCommand(commandText, connection))
            {
                var rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return Response<Point>.SuccessResponse(point, "Point updated successfully.");
                }
                else
                {
                    return Response<Point>.ErrorResponse("Point not found or not updated.");
                }
            }
        }

        public Response<Point> DeletePointWithDb(long id)
        {
            var commandText = $"DELETE FROM Points WHERE Id = {id}";

            using (var connection = GetOpenConnection())
            using (var command = new NpgsqlCommand(commandText, connection))
            {
                var rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return Response<Point>.SuccessResponse(null, "Point deleted successfully.");
                }
                else
                {
                    return Response<Point>.ErrorResponse("Point not found or not deleted.");
                }
            }
        }

        public Response<List<Point>> GetAllPointsWithDb()
        {
            var points = new List<Point>();

            var commandText = "SELECT Id, PointX, PointY, Name FROM Points";

            using (var connection = GetOpenConnection())
            using (var command = new NpgsqlCommand(commandText, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var point = new Point
                    {
                        Id = reader.GetInt32(0),
                        PointX = reader.GetDouble(1),
                        PointY = reader.GetDouble(2),
                        Name = reader.GetString(3)
                    };
                    points.Add(point);
                }
            }

            return Response<List<Point>>.SuccessResponse(points, "Points retrieved successfully.");
        }

        public Response<Point> GetPointByIdWithDb(int id)
        {
            Point point = null;

            var commandText = $"SELECT Id, PointX, PointY, Name FROM Points WHERE Id = {id}";

            using (var connection = GetOpenConnection())
            using (var command = new NpgsqlCommand(commandText, connection))
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    point = new Point
                    {
                        Id = reader.GetInt32(0),
                        PointX = reader.GetDouble(1),
                        PointY = reader.GetDouble(2),
                        Name = reader.GetString(3)
                    };
                }
            }

            if (point != null)
            {
                return Response<Point>.SuccessResponse(point, "Point retrieved successfully.");
            }
            else
            {
                return Response<Point>.ErrorResponse("Point not found.");
            }
        }


    }
}
