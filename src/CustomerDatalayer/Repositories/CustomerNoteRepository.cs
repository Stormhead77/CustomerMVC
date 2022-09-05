using CustomerDatalayer.Entities;
using CustomerDatalayer.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace CustomerDatalayer.Repositories
{
    public class CustomerNoteRepository : BaseRepository<CustomerNote>, IRepository<CustomerNote>
    {
        public override string TableName => "CustomerNotes";

        public CustomerNote Create(CustomerNote address)
        {
            using (var connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(
                    $"INSERT INTO [{TableName}] (CustomerId, NoteText) " +
                    "OUTPUT INSERTED.[CustomerId], INSERTED.[NoteText] " +
                    "VALUES (@CustomerId, @NoteText)", connection);

                command.Parameters.AddRange(new[] {
                    new SqlParameter("@CustomerId", SqlDbType.Int) { Value = address.CustomerId },
                    new SqlParameter("@NoteText", SqlDbType.NVarChar, 100) { Value = address.NoteText },
                });

                var reader = command.ExecuteReader();
                reader.Read();

                return new CustomerNote(reader);
            }
        }

        public CustomerNote Read(int addressId)
        {
            using (var connection = GetConnection())
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM [{TableName}] WHERE CustomerId = @CustomerId", connection);

                command.Parameters.Add(new SqlParameter("@CustomerId", SqlDbType.Int) { Value = addressId });

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new CustomerNote(reader);
                    }
                }
            }

            return null;
        }

        public int Update(CustomerNote address)
        {
            using (var connection = GetConnection())
            {
                SqlCommand command = new SqlCommand(
                $"UPDATE [{TableName}] " +
                "SET " +
                    "CustomerId = @CustomerId, " +
                    "NoteText = @NoteText " +
                "WHERE CustomerId = @CustomerId", connection);

                command.Parameters.AddRange(new[] {
                    new SqlParameter("@CustomerId", SqlDbType.Int) { Value = address.CustomerId },
                    new SqlParameter("@NoteText", SqlDbType.NVarChar, 100) { Value = address.NoteText },
                });

                return command.ExecuteNonQuery();
            }
        }

        public int Delete(int customerId)
        {
            using (var connection = GetConnection())
            {
                SqlCommand command = new SqlCommand($"DELETE FROM [{TableName}] WHERE CustomerId = @CustomerId", connection);

                command.Parameters.Add(new SqlParameter("@CustomerId", SqlDbType.Int) { Value = customerId });

                return command.ExecuteNonQuery();
            }
        }

        public void DeleteAll()
        {
            using (var connection = GetConnection())
            {
                var command = new SqlCommand($"DELETE FROM [{TableName}]", connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
