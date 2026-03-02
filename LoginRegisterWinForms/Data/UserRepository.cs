using System;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace LoginRegisterWinForms.Data
{
    public class UserRepository
    {
        private readonly string _connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=UserDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public bool UsernameExists(string username)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            string sql = "SELECT COUNT(1) FROM Users WHERE Username = @Username";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Username", username);

            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }

        public void CreateUser(string username, string passwordHash)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            string sql = "INSERT INTO Users (Username, PasswordHash) VALUES (@Username, @PasswordHash)";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);

            cmd.ExecuteNonQuery();
        }

        public string? GetPasswordHash(string username)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            string sql = "SELECT PasswordHash FROM Users WHERE Username = @Username";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Username", username);

            object? result = cmd.ExecuteScalar();
            return result == null ? null : result.ToString();
        }
    }
}