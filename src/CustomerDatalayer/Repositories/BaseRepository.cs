using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CustomerDatalayer.Repositories
{
    public abstract class BaseRepository<TEntity>
    {
        public abstract string TableName { get; }

        public static SqlConnection GetConnection()
        {
            var conn = new SqlConnection("Server=localhost;Database=CustomerLib_Tolstykh;Trusted_Connection=True;");
            conn.Open();

            return conn;
        }

        private static TEntity GetTEntityInstance(SqlDataReader reader)
        {
            return (TEntity)Activator.CreateInstance(typeof(TEntity), reader);
        }

        public virtual List<TEntity> GetAll()
        {
            using (var connection = GetConnection())
            {
                var command = new SqlCommand(
                    $"SELECT * " +
                    $"FROM [{TableName}]", connection);
                var reader = command.ExecuteReader();

                var res = new List<TEntity>();
                while (reader.Read())
                    res.Add(GetTEntityInstance(reader));

                return res;
            }
        }

        public List<TEntity> GetPage(int pageSize, int pageNumber, params string[] orderColumns)
        {
            using (var connection = GetConnection())
            {
                var command = new SqlCommand(
                    $"SELECT * " +
                    $"FROM [{TableName}] " +
                    $"ORDER BY {string.Join(",", orderColumns)} " +
                    $"OFFSET {pageSize * (pageNumber - 1)} ROWS " +
                    $"FETCH FIRST {pageSize} ROWS ONLY", connection);
                var reader = command.ExecuteReader();

                var customers = new List<TEntity>();
                while (reader.Read())
                    customers.Add(GetTEntityInstance(reader));

                return customers;
            }
        }

        public int GetCount()
        {
            using (var connection = GetConnection())
            {
                var command = new SqlCommand(
                    $"SELECT COUNT(*)" +
                    $"FROM [{TableName}]", connection);
                var count = (int)command.ExecuteScalar();

                return count;
            }
        }
    }
}
