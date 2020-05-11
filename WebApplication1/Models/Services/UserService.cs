using Dapper;
using Microsoft.Data.SqlClient;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Services
{
    public class UserService
    {
        private string connectionString = "Server=localhost;Database=bank;User Id=postgres;Password=456rtyrty;Pooling=false";

        public bool IsValidUser(string UserName, string Password)
        {
            return true;
        }
        public User GetUser(int id)
        {
            using (IDbConnection db = CreateConnection())
            {
                db.Open();
                var user = db.Query<User>(@"SELECT id, email, name, status, password 
                                        FROM users
                                        WHERE id = @id;", new { id }).FirstOrDefault();
                db.Close();
                return user;
            }
        }

        public void CreateUser(User user)
        {                    
            using (IDbConnection db = CreateConnection())
            {
                db.Open();
                var sqlQuery = @"INSERT INTO users 
                             (id, email, name, status, password) 
                             VALUES(@id, @email, @name, @status, @password);";
                db.Execute(sqlQuery, user);
                db.Close();
                // если мы хотим получить id добавленного пользователя
                //var sqlQuery = "INSERT INTO Users (Name, Age) VALUES(@Name, @Age); SELECT CAST(SCOPE_IDENTITY() as int)";
                //int? userId = db.Query<int>(sqlQuery, user).FirstOrDefault();
                //user.Id = userId.Value;
            }
        }

        public void UpdateUser(int id, User user)
        {
            using (IDbConnection db = CreateConnection())
            {
                db.Open();
                var sqlQuery = @"UPDATE users 
                             SET name = @name, email = @email, 
                             status = @status, password = @password
                             WHERE id = @id;";
                user.Id = id;
                db.Execute(sqlQuery, user);
                db.Close();
            }
        }

        public void DeleteUser(int id)
        {
            using (IDbConnection db = CreateConnection())
            {
                db.Open();
                var sqlQuery = "DELETE FROM users WHERE id = @id;";
                db.Execute(sqlQuery, new { id });
                db.Close();
            }
        }
        private NpgsqlConnection CreateConnection()
        {
            var connection = new NpgsqlConnection(connectionString);

            return connection;
        }
    }
}
