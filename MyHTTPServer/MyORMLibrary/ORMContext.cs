using System.Data;
using System.Reflection;
using Microsoft.Data.SqlClient;


public class ORMContext
{
    private readonly string _connectionString;
    private readonly string tableName = "users";

    public ORMContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public T Create<T>(T entity, string tableName) where T : class
    {
        // Пример реализации метода Create
        // Параметризованный SQL-запрос для вставки данных
        throw new NotImplementedException();
    }

    public T ReadById<T>(int id) where T : class, new()
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string queryRequest = $"SELECT * FROM {tableName} WHERE id = @id";
            SqlCommand command = new SqlCommand(queryRequest, connection);
            command.Parameters.AddWithValue("@id", id);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return MapToEntity<T>(reader);
                }
            }
        }

        return null;
    }

    public T ReadByAll<T>() where T : class, new()
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string queryRequest = $"SELECT * FROM {tableName}";
            SqlCommand command = new SqlCommand(queryRequest, connection);
            //command.Parameters.AddWithValue("@id");

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return MapToEntity<T>(reader);
                }
            }
        }

        return null;
    }

    public T ReadByName<T>(string Name) where T : class, new()
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string queryRequest = $"SELECT * FROM {tableName} WHERE name = @Name ";
            SqlCommand command = new SqlCommand(queryRequest, connection);
            command.Parameters.AddWithValue("@name", Name);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return MapToEntity<T>(reader);
                }
            }
        }

        return null;
    }
    //public void Update<T>(int id, T entity, string tableName)
    //{
    //    using (SqlConnection connection = new SqlConnection(_connectionString))
    //    {
    //        connection.Open();
    //        string sql = $"UPDATE {tableName} SET Column1 = @value1 WHERE Id = @id";
    //        SqlCommand command = new SqlCommand(sql, connection);
    //        command.Parameters.AddWithValue("@id", id);
    //        command.Parameters.AddWithValue("@value1", "значение");
    //
    //        command.ExecuteNonQuery();
    //    }
    //}

    public void Delete(int id, string tableName)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sql = $"DELETE FROM {tableName} WHERE Id = @id";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
        }
    }

    private T MapToEntity<T>(SqlDataReader reader) where T : class, new()
    {
        T entity = new T();
        Type entityType = typeof(T);
        PropertyInfo[] properties = entityType.GetProperties();

        //Получаем схему таблицы для проверки существования столбцов
        DataTable schemaTable = reader.GetSchemaTable();

        foreach (PropertyInfo property in properties)
        {
            string columnName = property.Name;
            //Проверяем наличие столбца в схеме
            DataRow[] rows = schemaTable.Select($"ColumnName = '" + columnName + "'");
            if (rows.Length > 0)
            {
                try
                {
                    int ordinal = reader.GetOrdinal(columnName);
                    object value = reader.GetValue(ordinal);
                    if (value != DBNull.Value)
                    {
                        property.SetValue(entity, Convert.ChangeType(value, property.PropertyType));
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    //Обработка ситуации, когда столбец неожиданно пропал
                    Console.WriteLine($"Column '{columnName}' not found in result set.");
                }
            }
        }

        return entity;
    }
}