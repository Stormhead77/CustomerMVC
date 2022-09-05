using CustomerDatalayer.Entities;
using CustomerDatalayer.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace CustomerDatalayer.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, IRepository<Customer>
    {
        public override string TableName => "Customers";

        public virtual Customer Create(Customer customer)
        {
            using (var connection = GetConnection())
            {
                var command = new SqlCommand(
                    $"INSERT INTO [{TableName}] (FirstName, LastName, PhoneNumber, Email, TotalPurchasesAmount) " +
                    "OUTPUT INSERTED.[CustomerId], INSERTED.[FirstName], INSERTED.[LastName], INSERTED.[PhoneNumber], INSERTED.[Email], INSERTED.[TotalPurchasesAmount] " +
                    "VALUES (@FirstName, @LastName, @PhoneNumber, @Email, @TotalPurchasesAmount)", connection);

                command.Parameters.AddRange(new[] {
                    new SqlParameter("@FirstName", SqlDbType.NVarChar, 50) { Value = (object?)customer.FirstName ?? DBNull.Value, IsNullable = true },
                    new SqlParameter("@LastName", SqlDbType.NVarChar, 50) { Value = customer.LastName },
                    new SqlParameter("@PhoneNumber", SqlDbType.NVarChar, 15) { Value = (object?)customer.PhoneNumber ?? DBNull.Value, IsNullable = true },
                    new SqlParameter("@Email", SqlDbType.NVarChar, 50) { Value = (object?)customer.Email ?? DBNull.Value, IsNullable = true  },
                    new SqlParameter("@TotalPurchasesAmount", SqlDbType.Money) { Value = (object?)customer.TotalPurchasesAmount ?? DBNull.Value, IsNullable = true },
                });

                var reader = command.ExecuteReader();
                reader.Read();

                return new Customer(reader);
            }
        }

        public virtual Customer Read(int customerId)
        {
            using (var connection = GetConnection())
            {
                var command = new SqlCommand($"SELECT * FROM [{TableName}] WHERE CustomerId = @CustomerId", connection);

                command.Parameters.Add(
                    new SqlParameter("@CustomerId", SqlDbType.Int)
                    {
                        Value = customerId
                    });

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Customer(reader);
                    }
                }
            }

            return null;
        }

        public virtual int Update(Customer customer)
        {
            using (var connection = GetConnection())
            {
                var command = new SqlCommand(
                $"UPDATE [{TableName}] " +
                "SET " +
                    "FirstName = @FirstName, " +
                    "LastName = @LastName, " +
                    "PhoneNumber = @PhoneNumber, " +
                    "Email = @Email, " +
                    "TotalPurchasesAmount = @TotalPurchasesAmount " +
                "WHERE CustomerId = @CustomerId", connection);

                command.Parameters.AddRange(new[] {
                    new SqlParameter("@FirstName", SqlDbType.NVarChar, 50) { Value = (object?)customer.FirstName ?? DBNull.Value, IsNullable = true },
                    new SqlParameter("@LastName", SqlDbType.NVarChar, 50) { Value = customer.LastName },
                    new SqlParameter("@PhoneNumber", SqlDbType.NVarChar, 15) { Value = (object?)customer.PhoneNumber ?? DBNull.Value, IsNullable = true },
                    new SqlParameter("@Email", SqlDbType.NVarChar, 50) { Value = (object?)customer.Email ?? DBNull.Value, IsNullable = true  },
                    new SqlParameter("@TotalPurchasesAmount", SqlDbType.Money) { Value = (object?)customer.TotalPurchasesAmount ?? DBNull.Value, IsNullable = true },
                    new SqlParameter("@CustomerId", SqlDbType.Int) { Value = customer.Id }
                });

                return command.ExecuteNonQuery();
            }
        }

        public virtual int Delete(int customerId)
        {
            using (var connection = GetConnection())
            {
                var command = new SqlCommand($"DELETE FROM [CustomerNotes] WHERE CustomerId = @CustomerId", connection);
                command.Parameters.Add(new SqlParameter("@CustomerId", SqlDbType.Int) { Value = customerId });
                command.ExecuteNonQuery();

                command.CommandText = $"DELETE FROM [Addresses] WHERE CustomerId = @CustomerId";
                command.ExecuteNonQuery();

                command.CommandText = $"DELETE FROM [{TableName}] WHERE CustomerId = @CustomerId";
                return command.ExecuteNonQuery();
            }
        }

        public void DeleteAll()
        {
            using (var connection = GetConnection())
            {
                var command = new SqlCommand("DELETE FROM [CustomerNotes]", connection);
                command.ExecuteNonQuery();

                command.CommandText = "DELETE FROM [Addresses]";
                command.ExecuteNonQuery();

                command.CommandText = $"DELETE FROM [{TableName}]";
                command.ExecuteNonQuery();
            }
        }
    }
}
